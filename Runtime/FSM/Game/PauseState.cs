using UnityEngine;

namespace StrangerGameTools.FSM.Game
{
    public class PauseState : GameState
    {
        public override void Enter(params object[] args)
        {
            base.Enter(args);
            Time.timeScale = 0f;
        }

        public override void Exit()
        {
            base.Exit();
            Time.timeScale = 1f;
        }
    }
}
