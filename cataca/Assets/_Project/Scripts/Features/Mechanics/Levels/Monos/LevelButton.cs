using _Project.Scripts.Features.Data.Entities;
using _Project.Scripts.Features.UI.Buttons.Monos;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Levels.Monos
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private ButtonWrapper _button;
        [SerializeField] private TextMeshProUGUI _name;
        
        public ButtonWrapper Button => _button;
        public TextMeshProUGUI Name => _name;
        public LevelDto LevelDto { get; set; }
    }
}