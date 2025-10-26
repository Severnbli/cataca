using Zenject;

namespace _Project.Scripts.Shared.ScriptableObjects
{
    public abstract class ScriptableObjectAutoInstaller<T> : ScriptableObjectInstaller 
        where T : ScriptableObjectAutoInstaller<T>
    {
        public sealed override void InstallBindings()
        {
            RegisterBindings();
            Container.Bind<T>().FromInstance((T) this).AsSingle();
        }
        
        protected virtual void RegisterBindings() {}
    }
}