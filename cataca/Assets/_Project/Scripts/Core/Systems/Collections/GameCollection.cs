using System.Collections.Generic;
using _Project.Scripts.Core.Systems.Interfaces;
using AB_Utility.FromSceneToEntityConverter;
using Leopotam.EcsLite;

#if UNITY_EDITOR
using Leopotam.EcsLite.UnityEditor;
#endif

namespace _Project.Scripts.Core.Systems.Collections
{
    public sealed class GameCollection : BaseCollection<IEcsGameSystem>
    {
        public GameCollection(EcsWorld defaultWorld, IEnumerable<IEcsGameSystem> systems) : base(defaultWorld, systems)
        {
#if UNITY_EDITOR
            Add(new EcsSystemsDebugSystem());
#endif
            this.ConvertScene();
        }
    }
}