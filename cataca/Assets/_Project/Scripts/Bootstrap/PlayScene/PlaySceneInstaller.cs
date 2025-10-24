using _Project.Scripts.Bootstrap.Shared;
using _Project.Scripts.Features.Services;
using _Project.Scripts.Features.Systems;
using _Project.Scripts.Features.Systems.Physics;

namespace _Project.Scripts.Bootstrap.PlayScene
{
    public class PlaySceneInstaller : SceneInstaller
    {
        protected override void SetupBindings()
        {
            Container.Bind<TimeService>().AsSingle();
            Container.BindInterfacesTo<MovementSystem>().AsSingle();
            Container.BindInterfacesTo<TimeServiceUpdateSystem>().AsSingle();
            Container.BindInterfacesTo<GravityApplierSystem>().AsSingle();
        }
    }
}