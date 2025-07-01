using System;
using StrangerGameTools.FSM;

namespace StrangerGameTools
{
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

        static readonly StateMachine _stateMachine = new StateMachine();

        public GameStateManager()
        {
            _stateMachine.AddState(Constants.STATE_MAIN_MENU, new FSM.Game.MainMenuState());
            _stateMachine.AddState(Constants.STATE_GAME, new FSM.Game.GameState());
            _stateMachine.AddState(Constants.STATE_PAUSE, new FSM.Game.PauseState());
            _stateMachine.AddState(Constants.STATE_CREDITS, new FSM.Game.CreditsState());
        }

        /// <summary>
        /// Initializes the GameStateManager.
        /// Pass any necessary initialization parameters here.
        /// </summary>
        public void Initialize()
        {
            _stateMachine.ChangeState(Constants.STATE_MAIN_MENU);
        }

        public static void ChangeState(string stateName, params object[] args)
        {
            _stateMachine.ChangeState(stateName, args);
        }

        public static void Update(float deltaTime)
        {
            _stateMachine.Update(deltaTime);
        }

        public static void HandleInput()
        {
            _stateMachine.HandleInput();
        }
    }
}
