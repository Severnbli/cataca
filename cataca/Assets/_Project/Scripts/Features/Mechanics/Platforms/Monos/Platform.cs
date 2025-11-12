using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        [SerializeField] private GameObject _statesContainer;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private PlatformType[] _platformTypes;
        
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public Collider2D Collider => _collider;
        public PlatformType[] PlatformTypes => _platformTypes;

        public List<Transform> States
        {
            get
            {
                var states = _statesContainer != null 
                    ? _statesContainer.GetComponentsInChildren<Transform>()
                        .Where(x => x != _statesContainer.transform)
                        .ToList()
                    : new List<Transform>();
                return states;
            }
        }
        
#if UNITY_EDITOR
        [PropertySpace(10)]
        [Button]
        private void Validate()
        {
            var checks = new Dictionary<Func<bool>, string>();

            checks.TryAdd(() => _statesContainer == null, "StatesContainer not set");
            checks.TryAdd(() => _spriteRenderer == null, "SpriteRenderer not set");
            checks.TryAdd(() => _collider == null, "Collider not set");
            checks.TryAdd(() => _platformTypes.Length != _platformTypes.Distinct().Count(), "Platform types duplicated");
            
            EditorUtils.Validate(checks, nameof(Platform));
        }
#endif
    }
}