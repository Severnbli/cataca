using _Project.Scripts.Core.Systems.Collections;
using Leopotam.EcsLite;
using Zenject;

namespace _Project.Scripts.Bootstrap.Shared
{
    public abstract class SceneInstaller : MonoInstaller
    {
        public sealed override void InstallBindings()
        {
            Container.BindInstance(new EcsWorld()).AsSingle();
            
            SetupBindings();
            SetupCollections();
        }

        protected abstract void SetupBindings();
        
        private void SetupCollections()
        {
#if UNITY_EDITOR
            Container.Bind<IEcsSystems>().To<EditorCollection>().AsSingle();
#endif
            
            Container.Bind<IEcsSystems>().To<GameCollection>().AsSingle();
        }
    }
}