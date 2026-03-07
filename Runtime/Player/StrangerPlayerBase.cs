using UnityEngine;

namespace StrangerGameTools.Player
{
    /// <summary>
    /// Base <see cref="IStrangerPlayer"/> implementation that provides default serialized fields
    /// and auto-properties that can be overridden.
    /// </summary>
    [DisallowMultipleComponent]
    public abstract class StrangerPlayerBase : MonoBehaviour, IStrangerPlayer
    {
        [Header("Base Player Settings")]
        [SerializeField]
        private bool _canLook = true;

        [SerializeField]
        private bool _canMove = true;

        [SerializeField]
        private bool _canJump = true;

        [SerializeField]
        private bool _isControllable = true;

        [SerializeField]
        private float _moveSpeed = 5f;

        public bool CanLook { get => _canLook; set => _canLook = value; }
        public bool CanMove { get => _canMove; set => _canMove = value; }
        public bool CanJump { get => _canJump; set => _canJump = value; }
        public bool IsControllable { get => _isControllable; set => _isControllable = value; }
        public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }

        /// <summary>
        /// Called when the player should be initialized.
        /// </summary>
        public virtual void Setup() { }
    }
}
