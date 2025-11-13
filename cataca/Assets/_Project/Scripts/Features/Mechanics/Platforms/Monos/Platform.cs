using System.Collections.Generic;
using System.Linq;
using System.Text;
using _Project.Scripts._Shared.Extensions;
using _Project.Scripts.Features.Mechanics.Platforms.Enums;
using UnityEngine;

#if UNITY_EDITOR
using System;
using _Project.Scripts._Shared.Utils;
using Sirenix.OdinInspector;
#endif

namespace _Project.Scripts.Features.Mechanics.Platforms.Monos
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private GameObject _positionStatesContainer;
        [SerializeField] private GameObject _rotateStatesContainer;
        [SerializeField] private GameObject _scaleStatesContainer;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private PlatformType[] _platformTypes;
        [SerializeField] private Transform _object;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public Collider2D Collider => _collider;
        public PlatformType[] PlatformTypes => _platformTypes;
        public Transform Object => _object;

        public List<Transform> PositionStates => _positionStatesContainer.GetChildComponents<Transform>();
        public List<Transform> RotateStates => _rotateStatesContainer.GetChildComponents<Transform>();
        public List<Transform> ScaleStates => _scaleStatesContainer.GetChildComponents<Transform>();

#if UNITY_EDITOR
        [PropertySpace(10)]
        [Button]
        private void Validate()
        {
            var checks = new Dictionary<Func<bool>, string>();

            checks.TryAdd(() => _positionStatesContainer == null, "PositionStatesContainer not set");
            checks.TryAdd(() => PositionStates.Count == 0, "No positional state is specified");
            checks.TryAdd(() => _rotateStatesContainer == null, "RotateStatesContainer not set");
            checks.TryAdd(() => RotateStates.Count == 0, "No rotational state is specified");
            checks.TryAdd(() => _scaleStatesContainer == null, "ScaleStatesContainer not set");
            checks.TryAdd(() => ScaleStates.Count == 0, "No scale state is specified");
            checks.TryAdd(() => _spriteRenderer == null, "SpriteRenderer not set");
            checks.TryAdd(() => _collider == null, "Collider not set");
            checks.TryAdd(() => _platformTypes.Length != _platformTypes.Distinct().Count(), "Platform types duplicated");
            checks.TryAdd(() => _object == null, "Object not set");
            
            EditorUtils.Validate(checks, nameof(Platform));
        }
#endif
    }
}