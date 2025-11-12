using _Project.Scripts._Shared.Utils;

#if UNITY_EDITOR
using System.Linq;
using UnityEngine;
#endif

namespace _Project.Scripts.Features.Mechanics.Platforms.Monos
{
    public class PlatformStatesViewer : MonoBehaviour
    {
        [SerializeField] private Platform _platform;

#if UNITY_EDITOR
        [SerializeField] private Color _gizmosColor = Color.green;
        [SerializeField] private Color _handlesColor = Color.cyan;
        
        private void OnDrawGizmos()
        {
            if (_platform == null || _platform.States == null || _platform.Collider == null) return;

            var nonNullStates = _platform.States.Where(x => x != null).ToList();
            
            for (var i = 0; i < nonNullStates.Count; i++)
            {
                Collider2DUtils.DrawColliderAt(_platform.Collider, nonNullStates[i].transform, _gizmosColor);
                EditorUtils.DrawHandle(nonNullStates[i].transform.position, i.ToString(), _handlesColor);
            }
        }
#endif
    }
}