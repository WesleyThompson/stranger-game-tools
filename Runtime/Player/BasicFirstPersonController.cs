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
		private GameObject _cinemachineCameraTarget;
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
		private float _terminalVelocity = 53.0f;

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


    }
}
