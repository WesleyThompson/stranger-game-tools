using StrangerGameTools.Management;
using UnityEngine;

namespace StrangerGameTools.UI
{
    [DisallowMultipleComponent]
    public abstract class MenuControllerBase : MonoBehaviour
    {
        [SerializeField]
        protected GameObject _menuRootElement;

        protected virtual void Awake()
        {
            SubscribeToEvents();
        }

        protected virtual void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        protected virtual void SubscribeToEvents() { }
        protected virtual void UnsubscribeFromEvents() { }

        protected void ShowMenu() => _menuRootElement.SetActive(true);
        protected void HideMenu() => _menuRootElement.SetActive(false);

        public virtual void EndGame()
        {
            GameStateManager.EndGame();
        }
    }
}
