using System;
using UnityEngine;

namespace StrangerGameTools.Interactions
{
    /// <summary>
    /// A collidable object that can detect collisions with other objects.
    /// </summary>
    public class Collidable : Contactable<Collision>
    {
        public event Action<Collidable> OnCollisionEnterEvent;
        public event Action<Collidable> OnCollisionExitEvent;

        protected override void SetupCollider()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = false;
        }

        protected override GameObject GetGameObject(Collision contact) => contact.gameObject;

        void OnCollisionEnter(Collision collision) =>
            HandleEnter(collision, _ => OnCollisionEnterEvent?.Invoke(this));

        void OnCollisionExit(Collision collision) =>
            HandleExit(collision, _ => OnCollisionExitEvent?.Invoke(this));
    }
}
