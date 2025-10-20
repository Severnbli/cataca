namespace _Project.Scripts.Core.Systems.Interfaces
{
    public interface ISystemsProvider
    {
        void Init();
        void Run();
        void Destroy();
    }
}