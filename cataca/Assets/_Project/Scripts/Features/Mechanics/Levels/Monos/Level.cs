using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Levels.Monos
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private GameObject _platformsParent;
        [SerializeField] private GameObject _recordsParent;
        [SerializeField] private GameObject _dangersParent;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private LevelCompleter _levelCompleter;
        
        public GameObject PlatformsParent => _platformsParent;
        public GameObject RecordsParent => _recordsParent;
        public GameObject DangersParent => _dangersParent;
        public Transform SpawnPoint => _spawnPoint;
        public LevelCompleter LevelCompleter => _levelCompleter;
    }
}