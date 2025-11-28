using _Project.Scripts.Bootstrap._Shared;
using _Project.Scripts.Features._Shared.Systems;
using _Project.Scripts.Features.Audio.Services;
using _Project.Scripts.Features.Audio.Systems;
using _Project.Scripts.Features.Mechanics.Anims.Systems;
using _Project.Scripts.Features.Mechanics.Levels.Systems;
using _Project.Scripts.Features.Mechanics.Records.Services;
using _Project.Scripts.Features.Mechanics.Records.Systems;
using _Project.Scripts.Features.Mechanics.Scenes.Systems;
using _Project.Scripts.Features.UI.Buttons.Systems;
using _Project.Scripts.Features.UI.Popups.Systems;

namespace _Project.Scripts.Bootstrap.MenuScene
{
    public class MenuSceneInstaller : SceneInstaller
    {
        protected override void BindServices()
        {
            Container.Bind<RecordsPlayService>().AsSingle();
            Container.Bind<AudioService>().AsSingle();
        }

        protected override void BindSystems()
        {
            Container.BindInterfacesTo<ButtonUpdateSystem>().AsSingle();
            
            Container.BindInterfacesTo<AudioSettingsLoaderSystem>().AsSingle();
            Container.BindInterfacesTo<AudioSlidersLoaderSystem>().AsSingle();
            Container.BindInterfacesTo<AudioServiceLoaderSystem>().AsSingle();
            Container.BindInterfacesTo<AudioThemeVolumeSetupSystem>().AsSingle();
            
            Container.BindInterfacesTo<RecordsUIInstantiatorSystem>().AsSingle();
            Container.BindInterfacesTo<SpawnLevelsButtonsFromStorageSystem>().AsSingle();
            
            Container.BindInterfacesTo<RecordPlayPauseRequestDeleter>().AsSingle();
            
            Container.BindInterfacesTo<CloseAppSignalButtonListenerSystem>().AsSingle();
            Container.BindInterfacesTo<CloseAppRequestHandlerSystem>().AsSingle();
            
            Container.BindInterfacesTo<RecordPlayPauseRequestOnButtonPressedSenderSystem>().AsSingle();
            
            Container.BindInterfacesTo<OpenSignalButtonListenerSystem>().AsSingle();
            Container.BindInterfacesTo<CloseSignalButtonListenerSystem>().AsSingle();
            
            Container.BindInterfacesTo<PopupOpenListenerSystem>().AsSingle();
            Container.BindInterfacesTo<PopupCloseListenerSystem>().AsSingle();
            Container.BindInterfacesTo<PopupPlayCloseAnimRequestHandlerSystem>().AsSingle();
            Container.BindInterfacesTo<PopupPlayOpenAnimRequestHandlerSystem>().AsSingle();
            
            Container.BindInterfacesTo<PlayPauseRecordOnRequestSystem>().AsSingle();
            Container.BindInterfacesTo<PlayableRecordUpdateSystem>().AsSingle();
            Container.BindInterfacesTo<RecordsPlayServiceUpdatePlayingStateSystem>().AsSingle();
            
            Container.BindInterfacesTo<AudioSettingsWriterSystem>().AsSingle();
            Container.BindInterfacesTo<AudioServiceUpdateOnSlidersChangeValueSystem>().AsSingle();
            Container.BindInterfacesTo<AudioSettingsUpdateSystem>().AsSingle();

            Container.BindInterfacesTo<InstantiateLevelButtonOnRequestSystem>().AsSingle();
            Container.BindInterfacesTo<LoadLevelButtonOnRequestSystem>().AsSingle();
            Container.BindInterfacesTo<LevelButtonUpdateSystem>().AsSingle();
            Container.BindInterfacesTo<ProcessLevelButtonPressSystem>().AsSingle();
            
            Container.BindInterfacesTo<ProcessSceneLoaderButtonPressedSystem>().AsSingle();
            Container.BindInterfacesTo<LoadSceneOnRequestSystem>().AsSingle();
            
            Container.BindInterfacesTo<InstantiateLevelButtonRequestDeleterSystem>().AsSingle();
            Container.BindInterfacesTo<LoadLevelButtonRequestDeleterSystem>().AsSingle();
            Container.BindInterfacesTo<LoadSceneRequestDeleterSystem>().AsSingle();
            
            Container.BindInterfacesTo<TweenKillOnDisposeSystem>().AsSingle();
        }
    }
}