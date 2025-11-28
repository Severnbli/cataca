using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Dangers.Monos
{
    public class Danger : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider;
        
        public Collider2D Collider => _collider;
    }
}