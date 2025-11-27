using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Levels.Monos
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private GameObject _platformsParent;
        [SerializeField] private GameObject _recordsParent;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Collider2D _levelCompleter;
        
        public GameObject PlatformsParent => _platformsParent;
        public GameObject RecordsParent => _recordsParent;
        public Transform SpawnPoint => _spawnPoint;
        public Collider2D LevelCompleter => _levelCompleter;
    }
}