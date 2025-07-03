using System;

namespace StrangerGameTools.FSM.Game
{
    /// <summary>
    /// Base class for game states in the state machine.
    /// </summary>
    public abstract class GameState : IState
    {
        public delegate void GameStateEvent();
        public GameStateEvent OnEnterState;
        public GameStateEvent OnExitState;

        public virtual void Enter(params object[] args)
        {
            OnEnterState?.Invoke();
        }

        public virtual void Exit()
        {
            OnExitState?.Invoke();
        }

        public virtual void HandleInput()
        {

        }

        public virtual void Update(float deltaTime)
        {

        }
    }
}
