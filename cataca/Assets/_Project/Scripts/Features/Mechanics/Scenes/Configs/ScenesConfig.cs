using System.Linq;
using _Project.Scripts._Shared.ScriptableObjects;
using _Project.Scripts._Shared.Utils;
using _Project.Scripts.Features.Mechanics.Scenes.Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;

namespace _Project.Scripts.Features.Mechanics.Scenes.Configs
{
    public class ScenesConfig : SerializedScriptableObjectAutoInstaller<ScenesConfig>
    {
        [SerializeField] private SerializedDictionary<Scene, string> _scenes = new()
        {
            { Scene.Menu, "Menu" },
            { Scene.Game, "Game" }
        };
        
        public SerializedDictionary<Scene, string> Scenes => _scenes;

#if UNITY_EDITOR
        [Button]
        [PropertySpace(10)]
        private void Validate()
        {
            var checks = EditorUtils.GetChecksContainer();
            
            checks.TryAdd(() => !_scenes.Keys.Contains(Scene.Menu), "Menu scene key is not provided");
            checks.TryAdd(() => !_scenes.Keys.Contains(Scene.Game), "Game scene key is not provided");
            checks.TryAdd(() => _scenes.Values.Any(string.IsNullOrEmpty), "Some of scene values are not provided");
            
            EditorUtils.Validate(checks, nameof(ScenesConfig));
        }
#endif
    }
}