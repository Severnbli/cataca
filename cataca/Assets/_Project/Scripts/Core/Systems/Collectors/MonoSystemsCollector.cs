using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Core.Systems.Interfaces;
using Leopotam.EcsLite;
using Sirenix.Utilities;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Systems.Collectors
{
    public class MonoSystemsCollector : MonoBehaviour, ISystemsCollector
    {
        private readonly HashSet<IEcsSystems> _systems = new();

        [Inject]
        public void Construct(IEnumerable<IEcsSystems> providers)
        {
            _systems.AddRange(providers);
        }
        
        private void Start()
        {
            Init();
        }

        private void Update()
        {
            Run();
        }

        private void OnDestroy()
        {
            Destroy();
        }

        public void Init()
        {
            foreach (var systems in _systems) systems?.Init();
        }

        public void Run()
        {
            foreach (var systems in _systems) systems?.Run();
        }

        public void Destroy()
        {
            foreach (var system in _systems.Where(s => s != null))
            {
                system.Destroy();
            }
        }
    }
}