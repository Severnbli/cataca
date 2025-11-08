using _Project.Scripts.Core.StateMachine.Interfaces;
using _Project.Scripts.Core.Systems.Interfaces;
using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;
using NUnit.Framework;
using Zenject;

namespace _Project.Scripts.Core.StateMachine.Base
{
    public class StateMachine : IStateMachine
    {
        public StateMachine(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        private DiContainer _diContainer;
        private IState _currentState;
        
        public async UniTaskVoid LoadState<T>() where T : IState
        {
            if (_currentState is not null) await _currentState.OnExit();
            _currentState = _diContainer.Resolve<T>();
            if (_currentState is not null) await _currentState.OnEnter();
        }
    }
}