using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Records.Monos
{
    public class RecordObject : SerializedMonoBehaviour
    {
        [SerializeField] private int _recordId;
        [SerializeField] private Collider2D _collider;
        
        public int RecordId => _recordId;
        public Collider2D Collider => _collider;
    }
}