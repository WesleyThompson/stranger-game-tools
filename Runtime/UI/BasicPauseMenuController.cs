using StrangerGameTools.FSM.Game;
using StrangerGameTools.Management;
using UnityEngine;

//TODO Create base class for game menus that work with the GameStateManager
namespace StrangerGameTools.UI
{
    public class BasicPauseMenuController : MonoBehaviour
    {
        [SerializeField]
        protected GameObject _pauseMenuRootElement;

        void Awake()
        {
            GameStateManager.OnEnterPause += OnEnterPause;
            GameStateManager.OnExitPause += OnExitPause;
        }

        void OnDestroy()
        {
            GameStateManager.OnEnterPause -= OnEnterPause;
            GameStateManager.OnExitPause -= OnExitPause;
        }

        protected void OnEnterPause()
        {
            _pauseMenuRootElement.SetActive(true);
        }

        protected void OnExitPause()
        {
            _pauseMenuRootElement.SetActive(false);
        }

        public void ResumeGame()
        {
            GameStateManager.StartGame(); //Should there be some function for resuming separate from start game?
        }
    }
}
