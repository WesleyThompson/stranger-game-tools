using UnityEngine;

namespace StrangerGameTools.FSM.Game
{
    public class MainMenuState : GameState
    {
        public override void Enter(params object[] args)
        {
            base.Enter(args);
            Cursor.lockState = CursorLockMode.None; //TODO could also use CursorLockMode.Confined, should load based on settings
            Cursor.visible = true;
        }
    }
}
