using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Core.StateMachine.Interfaces
{
    public interface IStateMachine
    {
        UniTaskVoid LoadState<T>() where T : IState;
    }
}