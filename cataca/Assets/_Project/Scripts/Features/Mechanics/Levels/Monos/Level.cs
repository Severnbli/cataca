using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Levels.Monos
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private GameObject _platformsParent;
        [SerializeField] private Transform _spawnPoint;
        
        public GameObject PlatformsParent => _platformsParent;
        public Transform SpawnPoint => _spawnPoint;
    }
}