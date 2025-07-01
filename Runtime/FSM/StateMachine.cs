using System.Collections.Generic;

namespace StrangerGameTools.FSM
{
    public class StateMachine
    {
        public IState CurrentState { get { return _currentState; } }

        readonly Dictionary<string, IState> _stateDictionary = new Dictionary<string, IState>();
        IState _currentState = new EmptyState();

        public void AddState(string name, IState state)
        {
            _stateDictionary.Add(name, state);
        }

        public void RemoveState(string name)
        {
            _stateDictionary.Remove(name);
        }

        public void ClearStates()
        {
            _stateDictionary.Clear();
        }

        public void ChangeState(string name, params object[] args)
        {
            _currentState.Exit();
            IState nextState = _stateDictionary[name];
            nextState.Enter(args);
            _currentState = nextState;
        }

        public void Update(float deltaTime)
        {
            _currentState.Update(deltaTime);
        }

        public void HandleInput()
        {
            _currentState.HandleInput();
        }
    }
}
