using UnityEngine;

namespace StrangerGameTools.FSM.Game
{
    public class PauseState : IState
    {
        public void Enter(params object[] args)
        {
            Time.timeScale = 0f;
        }

        public void Exit()
        {
            Time.timeScale = 1f;
        }

        public void HandleInput()
        {

        }

        public void Update(float deltaTime)
        {

        }
    }
}
