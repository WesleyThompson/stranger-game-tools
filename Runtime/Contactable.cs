using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace StrangerGameTools
{
    /// <summary>
    /// This is a base class for objects that can be contacted by other objects.
    /// It provides a way to handle contact events with a specific type of object.
    /// It requires a Collider component to be attached to the GameObject.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [RequireComponent(typeof(Collider))]
    public abstract class Contactable<T> : MonoBehaviour
    {
        /// <summary>
        /// This delegate is used to handle contact events with the specified type T.
        /// </summary>
        /// <param name="other"></param>
        public delegate void ContactEvent(T other);

        /// <summary>
        /// This event is triggered when another object enters the contact area of this object.
        /// It provides the other object as a parameter of type T.
        /// </summary>
        public ContactEvent OnContactEnter;

        /// <summary>
        /// This event is triggered when another object exits the contact area of this object.
        /// It provides the other object as a parameter of type T.
        /// </summary>
        public ContactEvent OnContactExit;

        /// <summary>
        /// Tags that will cause the contact to activate, leave empty to contact on anything
        /// </summary>
        [Tooltip("Tags that will cause the contact to activate, leave empty to contact on anything")]
        public List<string> ContactTags = new List<string>();

        /// <summary>
        /// This event is triggered when another object enters the contact area of this object.
        /// </summary>
        [SerializeField]
        public UnityEvent OnEnter;

        /// <summary>
        /// This event is triggered when another object exits the contact area of this object.
        /// </summary>
        [SerializeField]
        public UnityEvent OnExit;

        /// <summary>
        /// The collider that this contactable object uses to detect contacts.
        /// </summary>
        protected Collider _collider;

        /// <summary>
        /// Indicates whether this contactable can be contacted more than once.
        /// If true, it can be contacted multiple times; if false, it can only be contacted once.
        /// </summary>
        [SerializeField, Tooltip("If true, can be contacted multiple times; if false, can only be contacted once.")]
        protected bool _isSingleContact;

        /// <summary>
        /// Indicates whether this contactable object has already been contacted.
        /// This is used to prevent multiple contacts if _isSingleContact is true.
        /// </summary>
        protected bool _hasContacted;

        protected virtual void Reset() => SetupCollider();
        protected virtual void Awake()
        {
            SetupCollider();
            _hasContacted = false;
        }

        /// <summary>
        /// Configure the collider for this contactable object.
        /// This is called in Reset and Awake, so it can be used to set up the collider
        /// </summary>
        protected abstract void SetupCollider();

        /// <summary>
        /// Checks if the tag of the other object is valid for contact.
        /// If ContactTags is empty, it will return true for any tag.
        /// </summary>
        /// <param name="other">Gameobject that this is contacting</param>
        /// <returns></returns>
        protected bool CheckIsTagValid(GameObject other)
        {
            return !ContactTags.Any() || ContactTags.Contains(other.tag);
        }

        /// <summary>
        /// Checks if this contactable object can be contacted based on its single contact setting.
        /// </summary>
        /// <returns></returns>
        protected bool CheckPassesSingleContact()
        {
            if (_isSingleContact)
            {
                if (_hasContacted)
                {
                    return false;
                }
                _hasContacted = true;
            }
            return true;
        }

        /// <summary>
        /// Checks if the other object can be contacted based on its tag and single contact settings.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        protected bool CanContact(GameObject other)
        {
            if (CheckIsTagValid(other) && CheckPassesSingleContact())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
