using UnityEngine;
using Zenject;

namespace _Project.Scripts.Features.Systems.Physics.Configs
{
    public abstract class ForceConfig<T> : ScriptableObjectInstaller where T : ForceConfig<T>
    {
        public bool isEnabled;
        public float value;
        public Vector3 direction;

        public override void InstallBindings()
        {
            Container.Bind<T>().FromInstance((T)this).AsSingle();
        }
    }
}