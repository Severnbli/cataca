using System.Collections.Generic;
using _Project.Scripts.Core.Systems.Interfaces;
using Leopotam.EcsLite;
using Leopotam.EcsLite.UnityEditor;

namespace _Project.Scripts.Core.Systems.Collections
{
    public sealed class GameCollection : BaseCollection<IEcsGameSystem>
    {
        public GameCollection(EcsWorld defaultWorld, IEnumerable<IEcsGameSystem> systems) : base(defaultWorld, systems)
        {
#if UNITY_EDITOR
            Add(new EcsSystemsDebugSystem());
#endif
        }
    }
}