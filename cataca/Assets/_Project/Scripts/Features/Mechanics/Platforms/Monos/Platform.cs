using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Features.Mechanics.Platforms.Enums;
using UnityEngine;

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
    }
}