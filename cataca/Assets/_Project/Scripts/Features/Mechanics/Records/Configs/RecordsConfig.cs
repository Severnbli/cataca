using System.Collections.Generic;
using System.Linq;
using System.Text;
using _Project.Scripts._Shared.ScriptableObjects;
using _Project.Scripts._Shared.Utils;
using _Project.Scripts.Features.Data.Storages.BuiltIn.Configs;
using _Project.Scripts.Features.Mechanics.Records.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Records.Configs
{
    public class RecordsConfig : ScriptableObjectAutoInstaller<RecordsConfig>
    {
        [SerializeField] 
        [OnValueChanged("ValidateRecords")]
        private List<RecordComponent> _records;

        [SerializeField] private bool _loopedPlayback = true;
        
        public List<RecordComponent> Records => _records;
        public bool LoopedPlayback => _loopedPlayback;

#if UNITY_EDITOR
        [ShowInInspector]
        [ReadOnly]
        [TextArea(3, 100)]  
        private string _output;
        
        [SerializeField]
        [OnValueChanged("CheckIsRecordsStorageLinkNotNullOrEmpty")]
        private BuiltInStorageConfig _builtInStorageConfig;

        [PropertySpace]
        [Button]
        private void CheckIsRecordsStorageLinkNotNullOrEmpty()
        {
            if (_builtInStorageConfig is null) return;
            if (string.IsNullOrEmpty(_builtInStorageConfig.Records))
            {
                _output = "Warning! Records storage link is null or empty!";
                return;
            }
            
            _output = "Records storage link is fine.";
        }
        
        [PropertySpace]
        [Button]
        private void ValidateRecords()
        {
            if (_builtInStorageConfig == null) return;

            var idsFromStorage = StorageUtils.LoadRecords(_builtInStorageConfig).ConvertAll(x => x.Id);
            var notAudioClipIds = new HashSet<int>();
            var repeatedIds = new HashSet<int>();

            foreach (var record in _records)
            {
                if (idsFromStorage.Contains(record.Record.Id)) idsFromStorage.Remove(record.Record.Id);
                if (record.AudioClip == null) notAudioClipIds.Add(record.Record.Id);
                if (_records.Count(x => x.Record.Id == record.Record.Id) > 1)
                {
                    repeatedIds.Add(record.Record.Id);
                }
            }

            var outputBuilder = new StringBuilder();

            if (idsFromStorage.Any())
            {
                outputBuilder.Append($"Records with ids {string.Join(", ", idsFromStorage)} in storage but not submitted!" +
                                     $"\nYou can reset storage at BuiltInStorageConfig.asset.\n\n");
            }

            if (notAudioClipIds.Any())
            {
                outputBuilder.Append($"Records with ids {string.Join(", ", notAudioClipIds)} have no AudioClips!\n\n");
            }
            
            if (repeatedIds.Any())
            {
                outputBuilder.Append($"Records with repeated ids {string.Join(", ", repeatedIds)}!\n\n");
            }

            if (outputBuilder.Length == 0)
            {
                _output = "Records is good.";
                return;
            }

            _output = "Warning! Find some inaccuracies:\n\n";
            _output += outputBuilder.ToString();
        }
#endif
    }
}