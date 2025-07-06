using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StrangerGameTools.Input
{
    /// <summary>
    /// A basic class for handling player inputs.
    /// This class can be extended to implement custom input handling.
    /// </summary>
    [DisallowMultipleComponent, RequireComponent(typeof(PlayerInput))]
    public class BasicInputs : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 Move;
        public Vector2 Look;
        public bool Pause;
        public bool Jump;
        public bool Interact;
        public bool Sprint;
        public bool Debug;

        [Header("Mouse Cursor Settings")]
        public bool cursorInputForLook = true;

        public event Action OnInteractEvent;
        public event Action OnDebugEvent;
        public event Action OnPauseEvent;

#if ENABLE_INPUT_SYSTEM
        protected void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        protected void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        protected void OnJump(InputValue value)
        {
            JumpInput(value.isPressed);
        }

        protected void OnInteract(InputValue value)
        {
            InteractInput(value.isPressed);
        }

        protected void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }

        protected void OnDebug(InputValue value)
        {
            DebugInput(value.isPressed);
        }

        protected void OnPause(InputValue value)
        {
            PauseInput(value.isPressed);
        }

#endif

        private void MoveInput(Vector2 newMoveDirection)
        {
            Move = newMoveDirection;
        }

        private void LookInput(Vector2 newLookDirection)
        {
            Look = newLookDirection;
        }

        private void JumpInput(bool newJumpState)
        {
            Jump = newJumpState;
        }

        private void InteractInput(bool newInteractState)
        {
            Interact = newInteractState;
        }

        private void SprintInput(bool newSprintState)
        {
            Sprint = newSprintState;
        }

        private void DebugInput(bool newDebugState)
        {
            Debug = newDebugState;
        }

        private void PauseInput(bool newPauseState)
        {
            Pause = newPauseState;
        }
    }
}
