using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StrangerGameTools.Interactions
{
    /// <summary>
    /// Represents an interactable object in the game.
    /// </summary>
    public class Interactable : MonoBehaviour
    {
        /// <summary>
        ///  A list of triggerable objects that can activate this interactable.
        /// </summary>
        [SerializeField]
        List<Triggerable> _triggerables;
        /// <summary>
        ///  Whether this interactable can only be used once.
        /// </summary>
        [SerializeField]
        protected bool _isSingleUse;
        /// <summary>
        ///   An event that is triggered when the interactable is activated.
        /// </summary>
        public UnityEvent OnInteract;

        private bool _hasInteracted = false;
        private HashSet<Triggerable> _activeTriggerables;

        private void Awake()
        {
            _activeTriggerables = new HashSet<Triggerable>();
        }

        private void Start()
        {
            foreach (Triggerable triggerable in _triggerables)
            {
                triggerable.OnTriggerEnterEvent += AddTriggerable;
                triggerable.OnTriggerExitEvent += RemoveTriggerable;
            }
        }

        /// <summary>
        ///  Interacts with the interactable object.
        /// </summary>
        public virtual void Interact()
        {
            if (_isSingleUse && _hasInteracted)
            {
                return;
            }

            if (_activeTriggerables.Count > 0)
            {
                OnInteract?.Invoke();
                _hasInteracted = true;
            }
        }

        private void AddTriggerable(Triggerable triggerable)
        {
            _activeTriggerables.Add(triggerable);
        }

        private void RemoveTriggerable(Triggerable triggerable)
        {
            _activeTriggerables.Remove(triggerable);
        }
    }
}
