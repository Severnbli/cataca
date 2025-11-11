using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Features.Data.Entities;
using _Project.Scripts.Features.Data.Storages.BuiltIn.Configs;
using _Project.Scripts.Features.Mechanics.Records.Components;
using UnityEngine;

namespace _Project.Scripts._Shared.Utils
{
    public static class RecordsUtils
    {
        public static bool TryGetAudioClipByRecord(List<RecordComponent> records, Record record,
            out AudioClip audioClip)
        {
            audioClip = null;
            
            var recordComponentsWithSuchIds = records
                .Where(x => x.Record.Id == record.Id)
                .ToList();
            
            if (recordComponentsWithSuchIds.Count == 0) return false;
            
            var recordComponent = recordComponentsWithSuchIds.First();
            audioClip = recordComponent.AudioClip;
            return audioClip != null;
        }

        public static List<RecordComponent> GetRecordComponentsByRecords(List<RecordComponent> recordComponents,
            List<Record> records)
        {
            var ids = records.Select(x => x.Id).ToList();
            
            var result = recordComponents
                .Where(x => ids.Contains(x.Record.Id))
                .ToList();
            
            return result;
        }

        public static List<RecordComponent> GetRecordComponentsByMemory(List<RecordComponent> recordComponents,
            BuiltInStorageConfig config)
        {
            var records = StorageUtils.LoadRecords(config);
            
            return GetRecordComponentsByRecords(recordComponents, records);
        }
    }
}