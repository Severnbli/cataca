using _Project.Scripts.Bootstrap._Shared;
using _Project.Scripts.Features.Mechanics.Input.Services;
using _Project.Scripts.Features.Mechanics.Input.Systems;
using _Project.Scripts.Features.Mechanics.Physics._Shared.Systems;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Checks.Systems;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Systems;
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
            Container.BindInterfacesTo<TimeServiceUpdateSystem>().AsSingle();
            Container.BindInterfacesTo<InputServiceUpdateSystem>().AsSingle();

            Container.BindInterfacesTo<InputBasedWalkSystem>().AsSingle();
            Container.BindInterfacesTo<InputBasedJumpSystem>().AsSingle();
            Container.BindInterfacesTo<InputBasedDashSystem>().AsSingle();
            
            Container.BindInterfacesTo<JumpBlockGroundCheckUpdateSystem>().AsSingle();

            Container.BindInterfacesTo<WalkDampingUpdateSystem>().AsSingle();
            Container.BindInterfacesTo<JumpDampingUpdateSystem>().AsSingle();
            Container.BindInterfacesTo<DashDampingUpdateSystem>().AsSingle();
            Container.BindInterfacesTo<RigidbodyVelocityApplySystem>().AsSingle();

            Container.BindInterfacesTo<GroundCheckTimeBlockUpdateSystem>().AsSingle();
            Container.BindInterfacesTo<GroundCheckStatusUpdateSystem>().AsSingle();
            Container.BindInterfacesTo<JumpResetCurrentCountOnGroundCheckSystem>().AsSingle();
            Container.BindInterfacesTo<DashResetCurrentCountOnGroundCheckSystem>().AsSingle();
            
            Container.BindInterfacesTo<PlayerMovementConfigSetupSystem>().AsSingle();
        }
    }
}