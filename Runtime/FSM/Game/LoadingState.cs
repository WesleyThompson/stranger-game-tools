using StrangerGameTools.Settings;
using UnityEngine;

namespace StrangerGameTools.FSM.Game
{
    /// <summary>
    /// Represents the loading state of the game.
    /// </summary>
    /// <remarks>
    /// This state is typically used to display a loading screen while resources are being loaded.
    /// </remarks>
    public class LoadingState : GameState
    {
        public LoadingState(GameStateSettings gameStateSettings) : base(gameStateSettings)
        {

        }
    }
}
