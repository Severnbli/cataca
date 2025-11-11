using _Project.Scripts._Shared.ScriptableObjects;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Records.Configs
{
    public class RecordsInstantiatorConfig : ScriptableObjectAutoInstaller<RecordsInstantiatorConfig>
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Sprite[] _icons;
    }
}