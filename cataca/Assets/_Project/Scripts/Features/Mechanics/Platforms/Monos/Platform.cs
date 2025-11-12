using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Platforms.Monos
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private GameObject _statesContainer;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Collider2D _collider;
        
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public Collider2D Collider => _collider;

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