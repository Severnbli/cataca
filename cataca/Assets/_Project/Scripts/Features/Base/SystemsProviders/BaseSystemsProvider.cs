using _Project.Scripts.Features.Base.Interfaces;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Base.SystemsProviders
{
    public abstract class BaseSystemsProvider : ISystemsProvider
    {
        protected EcsSystems Systems;

        protected BaseSystemsProvider(EcsWorld world)
        {
            Systems = new EcsSystems(world);
        }

        public virtual void Init()
        {
            Systems.Init();
        }

        public virtual void Run()
        {
            Systems?.Run();
        }

        public virtual void Destroy()
        {
            if (Systems == null) return;
            
            Systems?.Destroy();
            Systems = null;
        }
    }
}