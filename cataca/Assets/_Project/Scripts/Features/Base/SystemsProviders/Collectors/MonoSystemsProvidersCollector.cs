using System.Collections.Generic;
using _Project.Scripts.Features.Base.Interfaces;
using Sirenix.Utilities;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Features.Base.SystemsProviders.Collectors
{
    public class MonoSystemsProvidersCollector : MonoBehaviour, ISystemsProvidersCollector
    {
        private readonly HashSet<ISystemsProvider> _providers = new();

        [Inject]
        public void Construct(IEnumerable<ISystemsProvider> providers)
        {
            _providers.AddRange(providers);
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
            foreach (var p in _providers) p.Init();
        }

        public void Run()
        {
            foreach (var p in _providers) p.Run();
        }

        public void Destroy()
        {
            foreach (var p in _providers) p.Destroy();
        }
    }
}