using UnityEngine;

namespace StrangerGameTools
{
    /// <summary>
    /// A triggerable object that can detect when other objects enter or exit its trigger area.
    /// </summary>
    public class Triggerable : Contactable<Collider>
    {
        protected override void SetupCollider()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }


        void OnTriggerEnter(Collider other)
        {
            if (CanContact(other.gameObject))
            {
                OnContactEnter?.Invoke(other);
                OnEnter?.Invoke();
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (CanContact(other.gameObject))
            {
                OnContactExit?.Invoke(other);
                OnExit?.Invoke();
            }
        }
    }
}
