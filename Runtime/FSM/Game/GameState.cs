using System;
using StrangerGameTools.Settings;
using UnityEngine;

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

        protected GameStateSettings gameStateSettings;

        public GameState(GameStateSettings gameStateSettings)
        {
            this.gameStateSettings = gameStateSettings;
        }

        public virtual void Enter(params object[] args)
        {
            OnEnterState?.Invoke();
            Cursor.lockState = gameStateSettings.CursorSettings.LockMode;
            Cursor.visible = gameStateSettings.CursorSettings.Visible;
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
