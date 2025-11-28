using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Core.StateMachine.Interfaces
{
    public interface IState
    {
        UniTask OnEnter();
        UniTask OnExit();
    }
}