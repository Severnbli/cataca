using _Project.Scripts.Bootstrap._Shared;
using _Project.Scripts.Features._Shared.Systems;
using _Project.Scripts.Features.UI.Buttons.Systems;

namespace _Project.Scripts.Bootstrap.MenuScene
{
    public class MenuSceneInstaller : SceneInstaller
    {
        protected override void BindSystems()
        {
            Container.BindInterfacesTo<CloseAppSignalButtonListenerSystem>().AsSingle();
            Container.BindInterfacesTo<CloseAppRequestHandlerSystem>().AsSingle();
        }
    }
}