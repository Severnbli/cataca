using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Features.Mechanics.Records.Monos
{
    public class Record : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        
        public Image Icon => _icon;
    }
}