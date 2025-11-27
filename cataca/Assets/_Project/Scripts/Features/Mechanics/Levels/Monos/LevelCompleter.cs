using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Levels.Monos
{
    public class LevelCompleter : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider;
        
        public Collider2D Collider => _collider;
    }
}