using System;
using StrangerGameTools.FSM;
using StrangerGameTools.FSM.Game;
using StrangerGameTools.Settings;
using UnityEngine;

namespace StrangerGameTools
{
    /// <summary>
    /// Manages the game states in the application.
    /// This class is a singleton and should be accessed through the Instance property.
    /// </summary>
    public class GameStateManager : Singleton<GameStateManager>
    {
        public static event Action OnEnterGame;
        public static event Action OnExitGame;
        public static event Action OnEnterMainMenu;
        public static event Action OnExitMainMenu;
        public static event Action OnEnterPause;
        public static event Action OnExitPause;
        public static event Action OnEnterCredits;
        public static event Action OnExitCredits;
        public static event Action OnEnterLoading;
        public static event Action OnExitLoading;

        static readonly StateMachine _stateMachine = new StateMachine();
        protected GameModeSettings _gameModeSettings;

        public const string GAMESTATEMANAGER_LOG_TAG = "[GameStateManager] ";

        /// <summary>
        /// Initializes the GameStateManager.
        /// Pass any necessary initialization parameters here.
        /// </summary>
        public void Initialize(GameModeSettings gameModeSettings)
        {
            _gameModeSettings = gameModeSettings;

            _stateMachine.AddState(Constants.STATE_MAIN_MENU, CreateGameState(Constants.STATE_MAIN_MENU, _gameModeSettings));
            _stateMachine.AddState(Constants.STATE_GAME, CreateGameState(Constants.STATE_GAME, _gameModeSettings));
            _stateMachine.AddState(Constants.STATE_PAUSE, CreateGameState(Constants.STATE_PAUSE, _gameModeSettings));
            _stateMachine.AddState(Constants.STATE_CREDITS, CreateGameState(Constants.STATE_CREDITS, _gameModeSettings));
            _stateMachine.AddState(Constants.STATE_LOADING, CreateGameState(Constants.STATE_LOADING, _gameModeSettings));
        }

        public static void StartMainMenu()
        {
            Debug.Log(GAMESTATEMANAGER_LOG_TAG + "Main Menu State Started");
            _stateMachine.ChangeState(Constants.STATE_MAIN_MENU);
        }

        public static void StartGame()
        {
            Debug.Log(GAMESTATEMANAGER_LOG_TAG + "Game State Started");
            _stateMachine.ChangeState(Constants.STATE_GAME);
        }

        public static void StartPause()
        {
            Debug.Log(GAMESTATEMANAGER_LOG_TAG + "Pause State Started");
            _stateMachine.ChangeState(Constants.STATE_PAUSE);
        }

        public static void StartCredits()
        {
            Debug.Log(GAMESTATEMANAGER_LOG_TAG + "Credits State Started");
            _stateMachine.ChangeState(Constants.STATE_CREDITS);
        }

        public static void StartLoading()
        {
            Debug.Log(GAMESTATEMANAGER_LOG_TAG + "Loading State Started");
            _stateMachine.ChangeState(Constants.STATE_LOADING);
        }

        protected string GetCurrentState()
        {
            return _stateMachine.CurrentState.GetType().Name ?? "No current state";
        }

        protected static void ChangeState(string stateName, params object[] args)
        {
            _stateMachine.ChangeState(stateName, args);
        }

        protected static void Update(float deltaTime)
        {
            _stateMachine.Update(deltaTime);
        }

        protected static void HandleInput()
        {
            _stateMachine.HandleInput();
        }

        /// <summary>
        /// Creates a new game state based on the provided state name and settings.
        /// </summary>
        /// <param name="stateName"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        protected GameState CreateGameState(string stateName, GameModeSettings settings)
        {
            GameState gameState;
            switch (stateName)
            {
                case Constants.STATE_MAIN_MENU:
                    gameState = new MainMenuState();
                    gameState.OnEnterState += () => OnEnterMainMenu?.Invoke();
                    gameState.OnExitState += () => OnExitMainMenu?.Invoke();
                    break;
                case Constants.STATE_GAME:
                    gameState = new GameplayState();
                    gameState.OnEnterState += () => OnEnterGame?.Invoke();
                    gameState.OnExitState += () => OnExitGame?.Invoke();
                    break;
                case Constants.STATE_PAUSE:
                    gameState = new PauseState();
                    gameState.OnEnterState += () => OnEnterPause?.Invoke();
                    gameState.OnExitState += () => OnExitPause?.Invoke();
                    break;
                case Constants.STATE_CREDITS:
                    gameState = new CreditsState();
                    gameState.OnEnterState += () => OnEnterCredits?.Invoke();
                    gameState.OnExitState += () => OnExitCredits?.Invoke();
                    break;
                case Constants.STATE_LOADING:
                    gameState = new LoadingState();
                    gameState.OnEnterState += () => OnEnterLoading?.Invoke();
                    gameState.OnExitState += () => OnExitLoading?.Invoke();
                    break;
                default:
                    throw new ArgumentException($"Unknown game state: {stateName}");
            }

            return gameState;
        }
    }
}
