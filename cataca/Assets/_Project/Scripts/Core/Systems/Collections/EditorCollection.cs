using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Core.Systems.Interfaces;
using Leopotam.EcsLite;
using Leopotam.EcsLite.UnityEditor;

namespace _Project.Scripts.Core.Systems.Collections
{
    public class EditorCollection : EcsSystems
    {
        public EditorCollection(EcsWorld defaultWorld, IEnumerable<IEditorSystem> systems) : base(defaultWorld)
        {
            AddEditorSystems(systems);
        }

        private void AddEditorSystems(IEnumerable<IEditorSystem> systems)
        {
            GetAllSystems().AddRange(systems);
            GetAllSystems().Add(new EcsSystemsDebugSystem());
        }
    }
}