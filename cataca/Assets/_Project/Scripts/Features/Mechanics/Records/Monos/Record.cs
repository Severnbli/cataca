using _Project.Scripts.Features.UI.Buttons.Monos;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Features.Mechanics.Records.Monos
{
    public class Record : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private ButtonWrapper _button;
        
        public Image Icon => _icon;
        public ButtonWrapper Button => _button;
    }
}