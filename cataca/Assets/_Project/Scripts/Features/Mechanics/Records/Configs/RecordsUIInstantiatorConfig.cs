using _Project.Scripts._Shared.ScriptableObjects;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Records.Configs
{
    public class RecordsUIInstantiatorConfig : ScriptableObjectAutoInstaller<RecordsUIInstantiatorConfig>
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Sprite[] _icons;
        
        public GameObject Prefab => _prefab;
        public Sprite[] Icons => _icons;
    }
}