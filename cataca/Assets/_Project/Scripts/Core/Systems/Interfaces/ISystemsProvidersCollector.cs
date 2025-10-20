namespace _Project.Scripts.Core.Systems.Interfaces
{
    public interface ISystemsProvidersCollector
    {
        public void Init();
        public void Run();
        public void Destroy();
    }
}