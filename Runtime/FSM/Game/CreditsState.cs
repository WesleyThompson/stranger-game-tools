using StrangerGameTools.Settings;
using UnityEngine;

namespace StrangerGameTools.FSM.Game
{
    public class CreditsState : GameState
    {
        public CreditsState(GameStateSettings gameStateSettings) : base(gameStateSettings)
        {

        }

        public override void HandleInput()
        {
            base.HandleInput();
            // Handle input for the credits state, such as returning to the main menu
        }
    }
}
