using UnityEngine;

namespace StrangerGameTools
{
    /// <summary>
    /// A collidable object that can detect collisions with other objects.
    /// </summary>
    public class Collidable : Contactable<Collision>
    {
        protected override void SetupCollider()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = false;
        }

        void OnCollisionEnter(Collision collision)
        {
            if (CanContact(collision.gameObject))
            {
                OnContactEnter?.Invoke(collision);
                OnEnter?.Invoke();
            }
        }

        void OnCollisionExit(Collision collision)
        {
            if (CanContact(collision.gameObject))
            {
                OnContactExit?.Invoke(collision);
                OnExit?.Invoke();
            }
        }
    }
}
