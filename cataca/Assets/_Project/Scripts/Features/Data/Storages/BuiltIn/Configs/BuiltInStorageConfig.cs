using System.Collections.Generic;
using System.Linq;
using _Project.Scripts._Shared.ScriptableObjects;
using _Project.Scripts._Shared.Utils;
using _Project.Scripts.Features.Data.Entities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Features.Data.Storages.BuiltIn.Configs
{
    public class BuiltInStorageConfig : ScriptableObjectAutoInstaller<BuiltInStorageConfig>
    {
        [SerializeField] private string _levelToLoad;
        [SerializeField] private string _levels;
        [SerializeField] private string _records;
        [SerializeField] private string _settings;
        
        public string LevelToLoad => _levelToLoad;
        public string Levels => _levels;
        public string Records => _records;
        public string Settings => _settings;

#if UNITY_EDITOR
        [PropertySpace(10)]
        [PropertyOrder(0)]
        [Button]
        private void Validate()
        {
            var checks = EditorUtils.GetChecksContainer();
            
            checks.TryAdd(() => string.IsNullOrEmpty(_levelToLoad), "LevelToLoad key is not assigned");
            checks.TryAdd(() => string.IsNullOrEmpty(_levels), "Levels key is not assigned");
            checks.TryAdd(() => string.IsNullOrEmpty(_records), "Records key is not assigned");
            checks.TryAdd(() => string.IsNullOrEmpty(_settings), "Settings key is not assigned");

            var storages = new List<string> { _levelToLoad, _levels, _records, _settings };
            checks.TryAdd(() => storages.Count != storages.Distinct().Count(), "Repeated storage values detected");
            
            EditorUtils.Validate(checks, nameof(BuiltInStorageConfig));
        }
        
        [PropertySpace(10)]
        [PropertyOrder(1)]
        [Button]
        private void ResetLevelToLoad()
        {
            PlayerPrefs.DeleteKey(_levelToLoad);
        }

        [PropertySpace(10)]
        [PropertyOrder(2)]
        [SerializeField]
        private LevelDto _level;
        
        [PropertySpace(10)]
        [PropertyOrder(2)]
        [Button]
        private void AddLevelToLoad()
        {
            StorageUtils.SaveLevelToLoad(this, _level);
        }
        
        [PropertySpace(10)]
        [PropertyOrder(3)]
        [Button]
        private void ResetLevels()
        {
            PlayerPrefs.DeleteKey(_levels);
        }
        
        [PropertySpace(10)]
        [PropertyOrder(4)]
        [Button]
        private void ResetRecords()
        {
            PlayerPrefs.DeleteKey(_records);
        }

        [PropertySpace] 
        [PropertyOrder(5)]
        [SerializeField] 
        private RecordDto recordDto;
        
        [PropertySpace(10)]
        [PropertyOrder(5)]
        [Button]
        private void AddRecord()
        {
            var list = StorageUtils.LoadRecords(this);
            list.Add(recordDto);
            StorageUtils.SaveRecords(this, list);
        }
        
        [PropertySpace(10)]
        [PropertyOrder(6)]
        [Button]
        private void ResetSettings()
        {
            PlayerPrefs.DeleteKey(_settings);
        }
        
        [PropertySpace(10)]
        [PropertyOrder(10)]
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