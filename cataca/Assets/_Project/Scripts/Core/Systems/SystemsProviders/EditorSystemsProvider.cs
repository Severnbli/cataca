#if UNITY_EDITOR

using Leopotam.EcsLite;
using Leopotam.EcsLite.UnityEditor;

namespace _Project.Scripts.Core.Systems.SystemsProviders
{
    public class EditorSystemsProvider : BaseSystemsProvider
    {
        public EditorSystemsProvider(EcsWorld world) : base(world)
        {
        }

        public override void Init()
        {
            Systems
                .Add(new EcsWorldDebugSystem())
                .Init();
        }
    }
}

#endif