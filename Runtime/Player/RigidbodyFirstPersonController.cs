using System;
using StrangerGameTools.Input;
using StrangerGameTools.Interactions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StrangerGameTools.Player
{
    [RequireComponent(typeof(Collidable))]
    [DisallowMultipleComponent, RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public class RigidbodyFirstPersonController : MonoBehaviour
    {
        [SerializeField]
        private Transform _cameraTarget;
        [Header("Movement Settings")]
        [SerializeField]
        private bool _isControllable = true;
        [SerializeField]
        private float _moveSpeed = 5f;
        [SerializeField]
        private bool _canJump = true;
        [SerializeField, Range(0f, 5000f)]
        private float _jumpForce = 10f; //Maybe this should be tied to mass as a ratio?

        [Header("Look Settings")]
        [SerializeField]
        private float _lookSensitivity = 2f;
        [SerializeField]
        private float _verticalLookLimit = 90f;
        [SerializeField]
        private bool _grounded;
        [SerializeField, Tooltip("What layers the character uses as ground")]
        private LayerMask _groundLayers = 1 << 0; //Default layer

        public const float GROUNDED_CHECK_RADIUS = 0.5f;
        private BasicInputs _inputs;
        private Collider _collider;
        private Collidable _collidable;
        private Vector3 _groundedSpherePosition;
        private bool _isJumping = false;
        private PlayerInput _playerInput;
        private Rigidbody _rigidbody;
        private float _horizonalLookAngle = 0f;
        private float _verticalLookAngle = 0f;

        private void OnDrawGizmos()
        {
            //Draw grounded check sphere
            if (_collider != null && _groundedSpherePosition != null)
            {
                Gizmos.color = _grounded ? Constants.CLEAR_GREEN : Constants.CLEAR_RED;
                Gizmos.DrawSphere(_groundedSpherePosition, GROUNDED_CHECK_RADIUS);
            }
        }

        private void Start()
        {
            //Initialize required components
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _collidable = GetComponent<Collidable>();
            _inputs = FindAnyObjectByType<BasicInputs>();
            _playerInput = FindAnyObjectByType<PlayerInput>();
        }

        private void FixedUpdate()
        {
            GroundedCheck();
            if (!_isControllable)
            {
                return;
            }

            if (_canJump && _inputs.Jump)
            {
                Jump();
                _inputs.Jump = false;
            }
            Move();
        }

        void LateUpdate()
        {
            if(!_isControllable)
            {
                return;
            }

            Look();
        }

        private void Move()
        {
            Vector3 moveDirection = new Vector3(_inputs.Move.x, 0.0f, _inputs.Move.y).normalized;
            if (_inputs.Move != Vector2.zero)
            {
                //If there is input, calculate the movement direction based on the camera's forward and right vectors
                moveDirection = transform.right * _inputs.Move.x + transform.forward * _inputs.Move.y;
            }
            _rigidbody.linearVelocity = new Vector3(moveDirection.x * _moveSpeed, _rigidbody.linearVelocity.y, moveDirection.z * _moveSpeed);
        }

        private void Look()
        {
            float deltaTimeMultiplier =IsCurrentDeviceMouse ? 1.0f : Time.fixedDeltaTime;

            _horizonalLookAngle = _inputs.Look.x * _lookSensitivity * deltaTimeMultiplier;
            _verticalLookAngle += _inputs.Look.y * _lookSensitivity * deltaTimeMultiplier;

            //Rotate the capsule for horizontal look, rotate camera for vertical
            transform.Rotate(Vector3.up * _horizonalLookAngle);

            _verticalLookAngle = ClampAngle(_verticalLookAngle, -_verticalLookLimit, _verticalLookLimit);
            _cameraTarget.transform.localRotation = Quaternion.Euler(Vector3.left * _verticalLookAngle);
        }

        private void Jump()
        {
            if (_grounded && !_isJumping)
            {
                _isJumping = true;
                _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            }
        }

        //TODO make this a utility function for input
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

        /// <summary>
        /// Checks if the player is grounded by casting a sphere at the bottom of the collider.
        /// Depends on the player's collider being on a separate layer from the ground layers.
        /// </summary>
        private void GroundedCheck()
        {
            _groundedSpherePosition = new Vector3(transform.position.x, transform.position.y - (_collider.bounds.size.y / 2f), transform.position.z);
            _grounded = Physics.CheckSphere(_groundedSpherePosition, GROUNDED_CHECK_RADIUS, _groundLayers, QueryTriggerInteraction.Ignore);
            if (_grounded && _isJumping && _rigidbody.linearVelocity.y < 0f)
            {
                //TODO add an event for when the player lands
                _isJumping = false;
            }
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -Constants.MAX_DEGREES) lfAngle += Constants.MAX_DEGREES;
            if (lfAngle > Constants.MAX_DEGREES) lfAngle -= Constants.MAX_DEGREES;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}
