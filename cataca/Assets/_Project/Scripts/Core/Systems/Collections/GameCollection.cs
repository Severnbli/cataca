using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Core.Systems.Interfaces;
using Leopotam.EcsLite;
using Leopotam.EcsLite.UnityEditor;

namespace _Project.Scripts.Core.Systems.Collections
{
    public class GameCollection : EcsSystems
    {
        public GameCollection(EcsWorld defaultWorld, IEnumerable<IGameSystem> systems) : base(defaultWorld)
        {
            AddGameSystems(systems);
        }

        private void AddGameSystems(IEnumerable<IGameSystem> systems)
        {
            GetAllSystems().AddRange(systems);
            
#if UNITY_EDITOR
            GetAllSystems().Add(new EcsSystemsDebugSystem());
#endif
        }
    }
}