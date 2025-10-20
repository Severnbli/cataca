namespace _Project.Scripts.Core.Systems.Interfaces
{
    public interface ISystemsCollector
    {
        public void Init();
        public void Run();
        public void Destroy();
    }
}