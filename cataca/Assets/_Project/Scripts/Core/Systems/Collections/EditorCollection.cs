using System.Collections.Generic;
using _Project.Scripts.Core.Systems.Interfaces;
using Leopotam.EcsLite;

#if UNITY_EDITOR
using Leopotam.EcsLite.UnityEditor;
#endif

namespace _Project.Scripts.Core.Systems.Collections
{
    public sealed class EditorCollection : BaseCollection<IEcsEditorSystem>
    {
        public EditorCollection(EcsWorld defaultWorld, IEnumerable<IEcsEditorSystem> systems) : base(defaultWorld, systems)
        {
#if UNITY_EDITOR
            Add(new EcsWorldDebugSystem());
            Add(new EcsSystemsDebugSystem());
#endif
        }
    }
}