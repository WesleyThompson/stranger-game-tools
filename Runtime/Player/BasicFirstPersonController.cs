using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StrangerGameTools.Player
{
    /// <summary>
    /// A basic first-person controller that can be extended for custom player movement.
    /// </summary>
#if ENABLE_INPUT_SYSTEM
    [RequireComponent(typeof(PlayerInput))]
#endif
    [RequireComponent(typeof(CharacterController)), DisallowMultipleComponent]
    public class BasicFirstPersonController : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField]
        private bool _isControllable = true;
        [SerializeField, Tooltip("Move speed of the character in m/s")]
        private float _moveSpeed = 4.0f;
        [SerializeField, Tooltip("Sprint speed of the character in m/s")]
        private float _sprintSpeed = 6.0f;
        [SerializeField, Tooltip("Rotation speed of the character")]
        private float _rotationSpeed = 1.0f;
        [SerializeField, Tooltip("Acceleration and deceleration")]
        private float _speedChangeRate = 10.0f;

        [Space(10)]
        [SerializeField, Tooltip("The height the player can jump")]
        private float _jumpHeight = 1.2f;
        [SerializeField, Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        private float _gravity = -15.0f;

        [Space(10)]
        [SerializeField]
        private bool _jumpEnabled = false;

        [SerializeField, Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        private float _jumpTimeout = 0.1f;
        [SerializeField, Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        private float _fallTimeout = 0.15f;

        [Header("Player Grounded")]
        [SerializeField, Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        private bool _grounded = true;
        [SerializeField, Tooltip("Useful for rough ground")]
        private float _groundedOffset = -0.14f;
        [SerializeField, Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        private float _groundedRadius = 0.5f;
        [SerializeField, Tooltip("What layers the character uses as ground")]
        private LayerMask _groundLayers;

        [Header("Cinemachine")]
        [SerializeField, Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        private GameObject _cameraTarget;
        [SerializeField, Tooltip("How far in degrees can you move the camera up")]
        private float _topClamp = 90.0f;
        [Tooltip("How far in degrees can you move the camera down")]
        [SerializeField]
        private float _bottomClamp = -90.0f;

        [Header("Audio")]
        [SerializeField]
        private float _walkingSoundCooldownTime;
        [SerializeField]
        private float _runningSoundCooldownTime;

        private float _stepTime = 0f;

        // cinemachine
        private float _cinemachineTargetPitch;

        // player
        private float _speed;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private readonly float _terminalVelocity = 53.0f;

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;


#if ENABLE_INPUT_SYSTEM
        private PlayerInput _playerInput;
#endif
        private CharacterController _controller;
        private BasicInputs _input;
        private GameObject _mainCamera;

        private const float _threshold = 0.01f;

        private bool IsCurrentDeviceMouse
        {
            get
            {
#if ENABLE_INPUT_SYSTEM
                return _playerInput.currentControlScheme == Constants.CONTROL_SCHEME_KEYBOARDMOUSE;
#else
                return false;
#endif
            }
        }

        private void OnDrawGizmosSelected()
		{
			Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
			Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

			if (_grounded) Gizmos.color = transparentGreen;
			else Gizmos.color = transparentRed;

			// when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
			Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - _groundedOffset, transform.position.z), _groundedRadius);
		}

        private void Awake()
        {
            if (_mainCamera == null)
            {
                _mainCamera = Camera.main.gameObject;
            }
        }

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<BasicInputs>();
#if ENABLE_INPUT_SYSTEM
            _playerInput = GetComponent<PlayerInput>();
#else
            Debug.LogError("BasicFirstPersonController requires the new Input System package to be enabled. Please enable it in the project settings.");
#endif
            _jumpTimeoutDelta = _jumpTimeout;
            _fallTimeoutDelta = _fallTimeout;
        }

        private void Update()
        {
            if (_isControllable)
            {
                GroundedCheck();
                JumpAndGravity();
                Move();
            }
        }

        private void LateUpdate()
        {
            if (_isControllable)
            {
                CameraRotation();
            }
        }

        public void SetIsControllable(bool isControllable)
        {
            _isControllable = isControllable;
        }

        private void GroundedCheck()
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y + _groundedOffset, transform.position.z);
            _grounded = Physics.CheckSphere(spherePosition, _groundedRadius, _groundLayers, QueryTriggerInteraction.Ignore);
        }

        private void CameraRotation()
        {
            if (_input.look.sqrMagnitude >= _threshold)
            {
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

                _cinemachineTargetPitch += _input.look.y * _rotationSpeed * deltaTimeMultiplier;
                _rotationVelocity = _input.look.x * _rotationSpeed * deltaTimeMultiplier;

                // clamp our pitch rotation
                _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, _bottomClamp, _topClamp);

                // Update Cinemachine camera target pitch
                _cameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

                // rotate the player left and right
                transform.Rotate(Vector3.up * _rotationVelocity);
            }
        }

        private void Move()
        {
			float targetSpeed = _input.sprint ? _sprintSpeed : _moveSpeed;

			if (_input.move == Vector2.zero) targetSpeed = 0.0f;

			// a reference to the players current horizontal velocity
			float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

			float speedOffset = 0.1f;
			float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

			// accelerate or decelerate to target speed
			if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				// creates curved result rather than a linear one giving a more organic speed change
				// note T in Lerp is clamped, so we don't need to clamp our speed
				_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * _speedChangeRate);

				// round speed to 3 decimal places
				_speed = Mathf.Round(_speed * 1000f) / 1000f;
			}
			else
			{
				_speed = targetSpeed;
			}

			// normalise input direction
			Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

			// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is a move input rotate player when the player is moving
			if (_input.move != Vector2.zero)
			{
				// move
				inputDirection = transform.right * _input.move.x + transform.forward * _input.move.y;

				//Audio
				if (_grounded)
				{
					if (_input.sprint)
					{
						if (Time.time - _runningSoundCooldownTime >= _stepTime)
						{
							FootstepSound();
						}
					}
					else
					{
						if (Time.time - _walkingSoundCooldownTime >= _stepTime)
						{
							FootstepSound();
						}
					}
				}
			}

			// move the player
			_controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

        }

        private void JumpAndGravity()
        {
            if (_grounded)
			{
				// reset the fall timeout timer
				_fallTimeoutDelta = _fallTimeout;

				// stop our velocity dropping infinitely when grounded
				if (_verticalVelocity < 0.0f)
				{
					_verticalVelocity = -2f;
				}

				if (_jumpEnabled)
				{
					// Jump
					if (_input.jump && _jumpTimeoutDelta <= 0.0f)
					{
						// the square root of H * -2 * G = how much velocity needed to reach desired height
						_verticalVelocity = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
					}

					// jump timeout
					if (_jumpTimeoutDelta >= 0.0f)
					{
						_jumpTimeoutDelta -= Time.deltaTime;
					}
				}
			}
			else
			{
				// reset the jump timeout timer
				_jumpTimeoutDelta = _jumpTimeout;

				// fall timeout
				if (_fallTimeoutDelta >= 0.0f)
				{
					_fallTimeoutDelta -= Time.deltaTime;
				}

				// if we are not grounded, do not jump
				_input.jump = false;
			}

			// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
			if (_verticalVelocity < _terminalVelocity)
			{
				_verticalVelocity += _gravity * Time.deltaTime;
			}
        }

        private void FootstepSound()
        {
            _stepTime = Time.time;
            // Play footstep sound here
            // Example: AudioManager.PlayFootstepSound();
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -Constants.MAX_DEGREES) lfAngle += Constants.MAX_DEGREES;
            if (lfAngle > Constants.MAX_DEGREES) lfAngle -= Constants.MAX_DEGREES;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}
