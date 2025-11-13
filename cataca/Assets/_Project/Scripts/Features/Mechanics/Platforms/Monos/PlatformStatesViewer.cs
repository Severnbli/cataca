using UnityEngine;

#if UNITY_EDITOR
using Sirenix.OdinInspector;
using System.Collections.Generic;
using _Project.Scripts.Features.Mechanics.Platforms.Enums;
using _Project.Scripts._Shared.Utils;
using System.Linq;
#endif

namespace _Project.Scripts.Features.Mechanics.Platforms.Monos
{
    public class PlatformStatesViewer : MonoBehaviour
    {
        [SerializeField] private Platform _platform;

#if UNITY_EDITOR
        [SerializeField] private bool _draw = false;
        [SerializeField] private bool _animate = false;
        [SerializeField] private float _animationDelay = 1f;
        [SerializeField] private Color _gizmosColor = Color.green;
        [SerializeField] private Color _handlesColor = Color.cyan;
        
        private float _lastSwitchTime;
        private int _currentInnerCombo;

        private void OnDrawGizmos()
        {
            if (!_draw || _platform == null || _platform.Collider == null) return;

            var positionStates = _platform.PositionStates;
            var rotationStates = _platform.RotateStates;
            var scaleStates = _platform.ScaleStates;
            var platformTypes = new HashSet<PlatformType>(_platform.PlatformTypes);

            if (!positionStates.Any() || !rotationStates.Any() || !scaleStates.Any()) return;

            var posCount = platformTypes.Contains(PlatformType.Position) ? positionStates.Count : 1;
            var rotCount = platformTypes.Contains(PlatformType.Rotate) ? rotationStates.Count : 1;
            var scaleCount = platformTypes.Contains(PlatformType.Scale) ? scaleStates.Count : 1;

            var innerCount = rotCount * scaleCount;

            var time = UnityEngine.Time.realtimeSinceStartup;

            if (!_animate)
            {
                for (var i = 0; i < posCount; i++)
                {
                    var posIdx = platformTypes.Contains(PlatformType.Position) ? i : 0;
                    var pos = positionStates[posIdx].position;

                    for (var j = 0; j < rotCount; j++)
                    {
                        var rotIdx = platformTypes.Contains(PlatformType.Rotate) ? j : 0;
                        var rot = rotationStates[rotIdx].rotation;

                        for (var k = 0; k < scaleCount; k++)
                        {
                            var scaleIdx = platformTypes.Contains(PlatformType.Scale) ? k : 0;
                            var scale = scaleStates[scaleIdx].localScale;

                            DrawState(pos, rot, scale, _gizmosColor);
                            var label = $"{i} | {j} | {k}";
                            EditorUtils.DrawHandle(pos, label, _handlesColor);
                        }
                    }
                }
            }
            else
            {
                if (time - _lastSwitchTime >= _animationDelay)
                {
                    _currentInnerCombo = (_currentInnerCombo + 1) % innerCount;
                    _lastSwitchTime = time;
                }

                var j = _currentInnerCombo / scaleCount;
                var k = _currentInnerCombo % scaleCount;

                var rotIdx = platformTypes.Contains(PlatformType.Rotate) ? j : 0;
                var scaleIdx = platformTypes.Contains(PlatformType.Scale) ? k : 0;

                var rot = rotationStates[rotIdx].rotation;
                var scale = scaleStates[scaleIdx].localScale;

                for (var i = 0; i < posCount; i++)
                {
                    var posIdx = platformTypes.Contains(PlatformType.Position) ? i : 0;
                    var pos = positionStates[posIdx].position;

                    DrawState(pos, rot, scale, _gizmosColor);
                    var label = $"{i} | {j} | {k}";
                    EditorUtils.DrawHandle(pos, label, _handlesColor);
                }
            }
        }

        private void DrawState(Vector3 pos, Quaternion rot, Vector3 scale, Color color)
        {
            Gizmos.matrix = Matrix4x4.TRS(pos, rot, scale);
            Collider2DUtils.DrawCollider(_platform.Collider, color);
            Gizmos.matrix = Matrix4x4.identity;
        }

        [PropertySpace(10)]
        [Button]
        private void ResetAnimation()
        {
            _lastSwitchTime = 0f;
            _currentInnerCombo = 0;
        }
#endif
    }
}