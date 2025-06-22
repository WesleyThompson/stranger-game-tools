using System;
using UnityEngine;

namespace StrangerGameTools.Interactions
{
    /// <summary>
    /// A triggerable object that can detect when other objects enter or exit its trigger area.
    /// </summary>
    public class Triggerable : Contactable<Collider>
    {
        public event Action<Triggerable> OnTriggerEnterEvent;
        public event Action<Triggerable> OnTriggerExitEvent;

        protected override void SetupCollider()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }

        protected override GameObject GetGameObject(Collider contact) => contact.gameObject;

        void OnTriggerEnter(Collider other) =>
            HandleEnter(other, _ => OnTriggerEnterEvent?.Invoke(this));

        void OnTriggerExit(Collider other) =>
            HandleExit(other, _ => OnTriggerExitEvent?.Invoke(this));
    }
}
