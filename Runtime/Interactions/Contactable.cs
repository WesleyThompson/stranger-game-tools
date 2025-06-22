using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace StrangerGameTools.Interactions
{
    [RequireComponent(typeof(Collider)), DisallowMultipleComponent]
    public abstract class Contactable<T> : MonoBehaviour
    {
        public delegate void ContactEvent(T other);
        public ContactEvent OnContactEnter;
        public ContactEvent OnContactExit;

        [Tooltip("Tags that will cause the contact to activate, leave empty to contact on anything")]
        public List<string> ContactTags = new();

        [SerializeField]
        public UnityEvent OnEnter;
        [SerializeField]
        public UnityEvent OnExit;

        protected Collider _collider;

        [SerializeField, Tooltip("If true, can be contacted multiple times; if false, can only be contacted once.")]
        protected bool _isSingleContact;

        protected bool _hasContacted;

        protected virtual void Reset() => SetupCollider();

        protected virtual void Awake()
        {
            SetupCollider();
            _hasContacted = false;
        }

        protected abstract void SetupCollider();

        protected bool CanContact(GameObject other) =>
            CheckIsTagValid(other) && CheckPassesSingleContact();

        protected bool CheckIsTagValid(GameObject other) =>
            !ContactTags.Any() || ContactTags.Contains(other.tag);

        protected bool CheckPassesSingleContact()
        {
            if (_isSingleContact && _hasContacted)
                return false;

            _hasContacted = true;
            return true;
        }

        /// <summary>
        /// Called by child classes to process an enter event.
        /// </summary>
        protected void HandleEnter(T contact, System.Action<Contactable<T>> externalEvent = null)
        {
            if (CanContact(GetGameObject(contact)))
            {
                OnContactEnter?.Invoke(contact);
                OnEnter?.Invoke();
                externalEvent?.Invoke(this);
            }
        }

        /// <summary>
        /// Called by child classes to process an exit event.
        /// </summary>
        protected void HandleExit(T contact, System.Action<Contactable<T>> externalEvent = null)
        {
            if (CanContact(GetGameObject(contact)))
            {
                OnContactExit?.Invoke(contact);
                OnExit?.Invoke();
                externalEvent?.Invoke(this);
            }
        }

        /// <summary>
        /// Must be implemented to extract the GameObject from the contact type.
        /// </summary>
        protected abstract GameObject GetGameObject(T contact);
    }
}
