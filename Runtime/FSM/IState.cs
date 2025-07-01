namespace StrangerGameTools.FSM
{
    public interface IState
    {
        void Update(float deltaTime);
        void HandleInput();
        void Enter(params object[] args);
        void Exit();
    }
}
