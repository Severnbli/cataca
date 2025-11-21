using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Levels.Monos
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private GameObject _platformsParent;
        
        public GameObject PlatformsParent => _platformsParent;
    }
}