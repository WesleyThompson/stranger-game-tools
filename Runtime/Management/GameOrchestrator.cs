using StrangerGameTools.Settings;
using UnityEngine;

namespace StrangerGameTools.Management
{
    /// <summary>
    /// Manages game wide settings and orchestrates the game states.
    /// </summary>
    [DisallowMultipleComponent]
    public class GameOrchestrator : MonoBehaviour
    {
        [SerializeField]
        private GameModeSettings _gameModeSettings;
        public static GameStateManager GameStateManager;
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            SetupGameStateManager();
        }

        void Start()
        {
            GameStateManager.StartMainMenu();
        }

        private void SetupGameStateManager()
        {
            GameStateManager = GameStateManager.Instance;
            GameStateManager.Initialize(_gameModeSettings);
        }

        [ContextMenu("Start Main Menu")]
        public void StartMainMenu()
        {
            GameStateManager.StartMainMenu();
        }

        [ContextMenu("Start Game")]
        public void StartGame()
        {
            GameStateManager.StartGame();
        }

        [ContextMenu("Start Pause")]
        public void StartPause()
        {
            GameStateManager.StartPause();
        }
    }
}
