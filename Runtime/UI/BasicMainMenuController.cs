using StrangerGameTools.Management;
using UnityEngine;

namespace StrangerGameTools.UI
{
    [DisallowMultipleComponent]
    public class BasicMainMenuController : MonoBehaviour
    {
        [SerializeField]
        protected GameObject _mainMenuRootElement;

        void Awake()
        {
            GameStateManager.OnEnterMainMenu += OnEnterMainMenu;
            GameStateManager.OnExitMainMenu += OnExitMainMenu;
        }

        void OnDestroy()
        {
            GameStateManager.OnEnterMainMenu -= OnEnterMainMenu;
            GameStateManager.OnExitMainMenu -= OnExitMainMenu;
        }

        void OnEnterMainMenu()
        {
            _mainMenuRootElement.SetActive(true);
        }

        void OnExitMainMenu()
        {
            _mainMenuRootElement.SetActive(false);
        }

        public void StartGame()
        {
            GameStateManager.StartGame();
        }
    }
}
