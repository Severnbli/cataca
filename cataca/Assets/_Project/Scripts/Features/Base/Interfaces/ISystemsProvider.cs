namespace _Project.Scripts.Features.Base.Interfaces
{
    public interface ISystemsProvider
    {
        void Init();
        void Run();
        void Destroy();
    }
}