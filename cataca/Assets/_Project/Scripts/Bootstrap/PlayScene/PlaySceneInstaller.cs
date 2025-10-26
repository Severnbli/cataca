using _Project.Scripts.Bootstrap.Shared;
using _Project.Scripts.Features.Input.Services;
using _Project.Scripts.Features.Input.Systems;
using _Project.Scripts.Features.Physics.Characters.Movement.Systems;
using _Project.Scripts.Features.Physics.Systems;
using _Project.Scripts.Features.Player.Systems;
using _Project.Scripts.Features.Time.Services;
using _Project.Scripts.Features.Time.Systems;

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

            Container.BindInterfacesTo<WalkDampingUpdateSystem>().AsSingle();
            Container.BindInterfacesTo<JumpDampingUpdateSystem>().AsSingle();
            Container.BindInterfacesTo<DashDampingUpdateSystem>().AsSingle();
            Container.BindInterfacesTo<RigidbodyVelocityApplySystem>().AsSingle();

            Container.BindInterfacesTo<PlayerConfigSetupSystem>().AsSingle();
        }
    }
}