using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Records.Monos
{
    public class RecordObject : MonoBehaviour
    {
        [SerializeField] private int _recordId;
        [SerializeField] private Collider2D _collider;
        
        public int RecordId => _recordId;
        public Collider2D Collider => _collider;
    }
}