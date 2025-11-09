using _Project.Scripts.Bootstrap._Shared;
using _Project.Scripts.Features._Shared.Systems;
using _Project.Scripts.Features.Mechanics.Records.Systems;
using _Project.Scripts.Features.UI.Buttons.Systems;
using _Project.Scripts.Features.UI.Popups.Systems;
using UnityEngine;

namespace _Project.Scripts.Bootstrap.MenuScene
{
    public class MenuSceneInstaller : SceneInstaller
    {
        protected override void BindSystems()
        {
            Container.BindInterfacesTo<RecordPlayPauseRequestDeleter>().AsSingle();
            
            Container.BindInterfacesTo<CloseAppSignalButtonListenerSystem>().AsSingle();
            Container.BindInterfacesTo<CloseAppRequestHandlerSystem>().AsSingle();
            
            Container.BindInterfacesTo<OpenSignalButtonListenerSystem>().AsSingle();
            Container.BindInterfacesTo<CloseSignalButtonListenerSystem>().AsSingle();
            
            Container.BindInterfacesTo<PopupOpenListenerSystem>().AsSingle();
            Container.BindInterfacesTo<PopupCloseListenerSystem>().AsSingle();
            Container.BindInterfacesTo<PopupPlayCloseAnimRequestHandlerSystem>().AsSingle();
            Container.BindInterfacesTo<PopupPlayOpenAnimRequestHandlerSystem>().AsSingle();
        }
    }
}