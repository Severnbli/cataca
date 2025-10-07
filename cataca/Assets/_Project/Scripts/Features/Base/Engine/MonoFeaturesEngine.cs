using System.Collections.Generic;
using App.Scripts.Features.FeatureBase.Contracts;
using UnityEngine;
using Zenject;

namespace App.Scripts.Features.FeatureBase.Engine
{
    public class MonoFeaturesEngine : MonoBehaviour, IFeaturesEngine
    {
        private readonly List<IDisposableFeature> _disposableFeatures = new();
        private readonly List<IFixedUpdatableFeature> _fixedUpdatableFeatures = new();
        private readonly List<ILaunchableFeature> _launchableFeatures = new();
        private readonly List<IOnDrawGizmosFeature> _onDrawGizmosFeatures = new();
        private readonly List<IUpdatableFeature> _updatableFeatures = new();

        public void Start()
        {
            Launch();
        }

        public void OnDestroy()
        {
            Dispose();
        }

        public void OnDrawGizmos()
        {
            foreach (var t in _onDrawGizmosFeatures) t.OnDrawGizmos();
        }

        public void Launch()
        {
            foreach (var t in _launchableFeatures) t.Launch();
        }

        public void Update()
        {
            foreach (var t in _updatableFeatures) t.Update(Time.deltaTime);
        }

        public void FixedUpdate()
        {
            foreach (var t in _fixedUpdatableFeatures) t.FixedUpdate(Time.fixedDeltaTime);
        }

        public void Dispose()
        {
            foreach (var t in _disposableFeatures) t.Dispose();
        }

        [Inject]
        public void Construct(IEnumerable<ILaunchableFeature> launchables,
            IEnumerable<IUpdatableFeature> updatables,
            IEnumerable<IFixedUpdatableFeature> fixedUpdatables,
            IEnumerable<IDisposableFeature> disposables,
            IEnumerable<IOnDrawGizmosFeature> onDrawGizmos
        )
        {
            _launchableFeatures.AddRange(launchables);
            _updatableFeatures.AddRange(updatables);
            _fixedUpdatableFeatures.AddRange(fixedUpdatables);
            _disposableFeatures.AddRange(disposables);
            _onDrawGizmosFeatures.AddRange(onDrawGizmos);
        }
    }
}