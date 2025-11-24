using _Project.Scripts._Shared.ScriptableObjects;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Levels.Configs
{
    public class LevelsUIInstantiatorConfig : ScriptableObjectAutoInstaller<LevelsUIInstantiatorConfig>
    {
        [SerializeField] private GameObject _prefab;
        
        public GameObject Prefab => _prefab;
    }
}