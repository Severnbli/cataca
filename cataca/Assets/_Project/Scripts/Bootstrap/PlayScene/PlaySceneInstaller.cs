using _Project.Scripts.Bootstrap.Shared;
using _Project.Scripts.Features.Time.Services;
using _Project.Scripts.Features.Time.Systems;

namespace _Project.Scripts.Bootstrap.PlayScene
{
    public class PlaySceneInstaller : SceneInstaller
    {

        protected override void BindServices()
        {
            Container.Bind<TimeService>().AsSingle();
        }

        protected override void BindSystems()
        {
            Container.BindInterfacesTo<TimeServiceUpdateSystem>().AsSingle();
        }
    }
}