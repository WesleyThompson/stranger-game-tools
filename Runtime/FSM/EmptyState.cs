namespace StrangerGameTools.FSM
{
    /// <summary>
    /// An empty state that can be used as a placeholder in state machines.
    /// </summary>
    /// <remarks>
    /// This state does not perform any actions and is useful for initializing state machines or as a default state.
    /// </remarks>
    public class EmptyState : IState
    {
        public void Enter(params object[] args) { }
        public void Exit() { }
        public void HandleInput() { }
        public void Update(float deltaTime) { }
    }
}
