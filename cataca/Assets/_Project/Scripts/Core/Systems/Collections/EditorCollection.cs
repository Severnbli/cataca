using System.Collections.Generic;
using _Project.Scripts.Core.Systems.Interfaces;
using Leopotam.EcsLite;
using Leopotam.EcsLite.UnityEditor;

namespace _Project.Scripts.Core.Systems.Collections
{
    public sealed class EditorCollection : BaseCollection<IEcsEditorSystem>
    {
        public EditorCollection(EcsWorld defaultWorld, IEnumerable<IEcsEditorSystem> systems) : base(defaultWorld, systems)
        {
            Add(new EcsSystemsDebugSystem());
        }
    }
}