using System.Collections.Generic;
using Leopotam.EcsLite;

namespace _Project.Scripts.Core.Systems.Collections
{
    public class BaseCollection<T> : EcsSystems where T : IEcsSystem
    {
        public BaseCollection(EcsWorld defaultWorld, IEnumerable<T> systems) : base(defaultWorld)
        {
            SetupSystems(systems);
        }

        private void SetupSystems(IEnumerable<T> systems)
        {
            foreach (var s in systems) Add(s);
        }
    }
}