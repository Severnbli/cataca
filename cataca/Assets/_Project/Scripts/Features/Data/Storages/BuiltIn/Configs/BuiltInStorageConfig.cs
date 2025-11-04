using _Project.Scripts._Shared.ScriptableObjects;
using _Project.Scripts._Shared.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Features.Data.Storages.BuiltIn.Configs
{
    public class BuiltInStorageConfig : ScriptableObjectAutoInstaller<BuiltInStorageConfig>
    {
        [SerializeField] private string _levels;
        [SerializeField] private string _records;
        
        public string Levels => _levels;
        public string Records => _records;

#if UNITY_EDITOR
        [PropertySpace(10)]
        [Button]
        private void ResetLevels()
        {
            PlayerPrefs.DeleteKey(_levels);
        }
        
        [PropertySpace(10)]
        [Button]
        private void ResetRecords()
        {
            PlayerPrefs.DeleteKey(_records);
        }
        
        [PropertySpace(10)]
        [Button]
        private void ResetAll()
        {
            if (EditorUtils.Confirm("Warning! It will reset ALL keys even if they are not specified."))
            {
                PlayerPrefs.DeleteAll();
            }
        }
#endif
    }
}