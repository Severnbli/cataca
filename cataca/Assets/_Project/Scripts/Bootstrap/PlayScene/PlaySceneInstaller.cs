using _Project.Scripts.Bootstrap.Shared;
using _Project.Scripts.Features.Time.Services;
using _Project.Scripts.Features.Time.Systems;

namespace _Project.Scripts.Bootstrap.PlayScene
{
    public class PlaySceneInstaller : SceneInstaller
    {
        protected override void SetupBindings()
        {
            Container.Bind<TimeService>().AsSingle();
            Container.BindInterfacesTo<TimeServiceUpdateSystem>().AsSingle();
        }
    }
}