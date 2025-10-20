using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Base.SystemsProviders
{
    public class GameSystemsProvider : BaseSystemsProvider
    {
        public GameSystemsProvider(EcsWorld world) : base(world)
        {
        }

        public override void Init()
        {
            Systems.Init();
        }
    }
}