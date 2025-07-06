using StrangerGameTools.Settings;
using UnityEngine;

namespace StrangerGameTools.FSM.Game
{

    public class GameplayState : GameState
    {
        public GameplayState(GameStateSettings gameStateSettings) : base(gameStateSettings)
        {

        }

        public override void Enter(params object[] args)
        {
            base.Enter(args);
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }
    }
}
