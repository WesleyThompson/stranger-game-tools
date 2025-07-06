using StrangerGameTools.Input;
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
        [SerializeField]
        private BasicInputs _basicInputs;

        [Header("Debug Settings")]
        [SerializeField]
        private bool _startInGameState = false;
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            SetupGameStateManager();
        }

        void Start()
        {
            if (_startInGameState)
            {
                GameStateManager.StartGame();
            }
            else
            {
                GameStateManager.StartMainMenu();
            }
        }

        void Update()
        {
            GameStateManager.Update(Time.deltaTime);
            GameStateManager.HandleInput();
        }

        private void SetupGameStateManager()
        {
            GameStateManager.Instance.Initialize(_gameModeSettings, _basicInputs);
        }

        public void StartGame()
        {
            GameStateManager.StartGame();
        }
    }
}
