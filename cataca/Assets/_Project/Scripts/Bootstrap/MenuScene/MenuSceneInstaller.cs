using _Project.Scripts.Bootstrap._Shared;
using _Project.Scripts.Features.UI.Buttons.Systems;

namespace _Project.Scripts.Bootstrap.MenuScene
{
    public class MenuSceneInstaller : SceneInstaller
    {
        protected override void BindSystems()
        {
            Container.BindInterfacesTo<CloseAppByButtonSystem>().AsSingle();
        }
    }
}