using System.Collections.Generic;
using System.Linq;
using _Project.Scripts._Shared.ScriptableObjects;
using _Project.Scripts._Shared.Utils;
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
        [PropertySpace(10)]
        [Button]
        private void Validate()
        {
            var checks = EditorUtils.GetChecksContainer();
            var ids = new HashSet<int>(_levels.Count);
            var repeatedIds = new HashSet<int>();
            
            foreach (var level in _levels)
            {
                if (!ids.Add(level.Level.Id))
                {
                    repeatedIds.Add(level.Level.Id);
                }
                
                checks.TryAdd(() => level.Prefab == null, $"Level with id = {level.Level.Id} has no prefab");
            }
            
            checks.TryAdd(() => repeatedIds.Any(), $"Ids [{string.Join(", ", repeatedIds)}] are repeated");
            
            EditorUtils.Validate(checks, nameof(LevelsConfig));
        }
#endif
    }
}