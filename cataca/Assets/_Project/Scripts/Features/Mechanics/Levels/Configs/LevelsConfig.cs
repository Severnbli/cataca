using System.Collections.Generic;
using System.Linq;
using _Project.Scripts._Shared.ScriptableObjects;
using _Project.Scripts._Shared.Utils;
using _Project.Scripts.Features.Data.Storages.BuiltIn.Configs;
using _Project.Scripts.Features.Mechanics.Levels.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Levels.Configs
{
    public class LevelsConfig : ScriptableObjectAutoInstaller<LevelsConfig>
    {
        [SerializeField] private List<LevelComponent> _levels;
        
        public List<LevelComponent> Levels => _levels;
      
#if UNITY_EDITOR
        [SerializeField] private BuiltInStorageConfig _storageConfig;
        
        [PropertySpace(10)]
        [Button]
        private void Validate()
        {
            var checks = EditorUtils.GetChecksContainer();
            var ids = new HashSet<int>(_levels.Count);
            var repeatedIds = new HashSet<int>();
            
            foreach (var level in _levels)
            {
                if (!ids.Add(level.LevelDto.Id))
                {
                    repeatedIds.Add(level.LevelDto.Id);
                }
                
                checks.TryAdd(() => level.Prefab == null, $"LevelDto with id = {level.LevelDto.Id} has no prefab");
            }
            
            checks.TryAdd(() => repeatedIds.Any(), $"Ids [{string.Join(", ", repeatedIds)}] are repeated");

            if (_storageConfig != null)
            {
                if (StorageUtils.TryLoadLevelToLoad(_storageConfig, out var levelToLoad))
                {
                    checks.TryAdd(() => !ids.Contains(levelToLoad.Id),
                        $"LevelToLoad with id {levelToLoad.Id} is in memory but not assigned");
                }

                var levels = StorageUtils.LoadLevels(_storageConfig);

                foreach (var level in levels)
                {
                    checks.TryAdd(() => !ids.Contains(level.Id),
                        "LevelDto with id: " + level.Id + " is in memory but not assigned");
                }
            }

            EditorUtils.Validate(checks, nameof(LevelsConfig));
        }
#endif
    }
}