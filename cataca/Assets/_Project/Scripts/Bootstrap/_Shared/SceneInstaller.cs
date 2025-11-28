using _Project.Scripts.Core.Systems.Collections;
using Leopotam.EcsLite;
using Zenject;

namespace _Project.Scripts.Bootstrap._Shared
{
    public abstract class SceneInstaller : MonoInstaller
    {
        public sealed override void InstallBindings()
        {
            Container.BindInstance(new EcsWorld()).AsSingle();
            Container.BindInstance(destroyCancellationToken).AsSingle();
            SetupBindings();
            SetupCollections();
        }

        private void SetupBindings()
        {
            BindServices();
            BindSystems();
        }
        
        private void SetupCollections()
        {
#if UNITY_EDITOR
            Container.Bind<IEcsSystems>().To<EditorCollection>().AsSingle();
#endif
            
            Container.Bind<IEcsSystems>().To<GameCollection>().AsSingle();
        }
        
        protected virtual void BindServices() {}
        protected virtual void BindSystems() {}
    }
}