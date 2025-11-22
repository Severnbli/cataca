using _Project.Scripts.Bootstrap._Shared;
using _Project.Scripts.Features.Audio.Systems;
using _Project.Scripts.Features.Mechanics.Anims.Systems;
using _Project.Scripts.Features.Mechanics.Input.Services;
using _Project.Scripts.Features.Mechanics.Input.Systems;
using _Project.Scripts.Features.Mechanics.Levels.Systems;
using _Project.Scripts.Features.Mechanics.Physics._Shared.Systems;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Checks.Systems;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Systems;
using _Project.Scripts.Features.Mechanics.Platforms.Requests;
using _Project.Scripts.Features.Mechanics.Platforms.Systems;
using _Project.Scripts.Features.Mechanics.Player.Systems;
using _Project.Scripts.Features.Mechanics.Time.Services;
using _Project.Scripts.Features.Mechanics.Time.Systems;

namespace _Project.Scripts.Bootstrap.PlayScene
{
    public class PlaySceneInstaller : SceneInstaller
    {

        protected override void BindServices()
        {
            Container.Bind<TimeService>().AsSingle();
            Container.Bind<InputService>().AsSingle();
        }

        protected override void BindSystems()
        {
            Container.BindInterfacesTo<PlayerMovementConfigSetupSystem>().AsSingle();
            Container.BindInterfacesTo<AudioSettingsLoaderSystem>().AsSingle();
            Container.BindInterfacesTo<LoadLevelsFromConfigSystem>().AsSingle();
            
            Container.BindInterfacesTo<TweenQueueAppendRequestDeleterSystem>().AsSingle();
            Container.BindInterfacesTo<LoadPlatformRequestDeleterSystem>().AsSingle();
            Container.BindInterfacesTo<LoadLevelRequestDeleterSystem>().AsSingle();
            
            Container.BindInterfacesTo<PositionPlatformRequestDeleterSystem>().AsSingle();
            Container.BindInterfacesTo<RotatePlatformRequestDeleterSystem>().AsSingle();
            Container.BindInterfacesTo<ScalePlatformRequestDeleterSystem>().AsSingle();
            
            Container.BindInterfacesTo<TimeServiceUpdateSystem>().AsSingle();
            Container.BindInterfacesTo<InputServiceUpdateSystem>().AsSingle();
            
            Container.BindInterfacesTo<JumpPerformRequestDeleterSystem>().AsSingle();
            Container.BindInterfacesTo<DashPerformRequestDeleterSystem>().AsSingle();
            Container.BindInterfacesTo<PlayJumpAnimationRequestDeleterSystem>().AsSingle();
            Container.BindInterfacesTo<PlayDashAnimationRequestDeleterSystem>().AsSingle();
            
            Container.BindInterfacesTo<JumpRequestSenderOnInputSystem>().AsSingle();
            Container.BindInterfacesTo<DashRequestSenderOnInputSystem>().AsSingle();
            Container.BindInterfacesTo<PositionPlatformRequestSenderOnInputSystem>().AsSingle();
            Container.BindInterfacesTo<RotatePlatformRequestSenderOnInputSystem>().AsSingle();
            Container.BindInterfacesTo<ScalePlatformRequestSenderOnInputSystem>().AsSingle();

            Container.BindInterfacesTo<WalkPerformOnListenInputSystem>().AsSingle();
            Container.BindInterfacesTo<JumpPerformOnRequestSystem>().AsSingle();
            Container.BindInterfacesTo<DashPerformOnRequestSystem>().AsSingle();
            
            Container.BindInterfacesTo<JumpBlockGroundCheckUpdateSystem>().AsSingle();

            Container.BindInterfacesTo<WalkDampingUpdateSystem>().AsSingle();
            Container.BindInterfacesTo<DashDampingUpdateSystem>().AsSingle();
            Container.BindInterfacesTo<RigidbodyVelocityApplySystem>().AsSingle();
            
            Container.BindInterfacesTo<PlayerDashAnimationTriggerSystem>().AsSingle();
            Container.BindInterfacesTo<PlayerFallAnimationUpdateSystem>().AsSingle();
            Container.BindInterfacesTo<PlayerJumpAnimationTriggerSystem>().AsSingle();
            Container.BindInterfacesTo<PlayerWalkAnimationUpdateSystem>().AsSingle();
            
            Container.BindInterfacesTo<FlipObjectOnWalkSystem>().AsSingle();

            Container.BindInterfacesTo<GroundCheckTimeBlockUpdateSystem>().AsSingle();
            Container.BindInterfacesTo<GroundCheckStatusUpdateSystem>().AsSingle();
            Container.BindInterfacesTo<JumpResetCurrentCountOnGroundCheckSystem>().AsSingle();
            Container.BindInterfacesTo<DashResetCurrentCountOnGroundCheckSystem>().AsSingle();
            
            Container.BindInterfacesTo<LoadLevelOnRequestSystem>().AsSingle();
            
            Container.BindInterfacesTo<PositionPlatformOnRequestSystem>().AsSingle();
            Container.BindInterfacesTo<RotatePlatformOnRequestSystem>().AsSingle();
            Container.BindInterfacesTo<ScalePlatformOnRequestSystem>().AsSingle();
            Container.BindInterfacesTo<PlatformTypeUpdateSystem>().AsSingle();
            Container.BindInterfacesTo<PlatformGlowAddSystem>().AsSingle();
            Container.BindInterfacesTo<PlatformGlowUpdatePulseSystem>().AsSingle();
            Container.BindInterfacesTo<PlatformGlowUpdateTransitionSystem>().AsSingle();
            Container.BindInterfacesTo<LoadPlatformOnRequestSystem>().AsSingle();
            Container.BindInterfacesTo<PlatformControlledByInputUpdateSystem>().AsSingle();

            Container.BindInterfacesTo<TweenQueueAppendOnRequestSystem>().AsSingle();
            Container.BindInterfacesTo<TweenQueueUpdateSystem>().AsSingle();
            
            Container.BindInterfacesTo<TweenKillOnDisposeSystem>().AsSingle();
        }
    }
}