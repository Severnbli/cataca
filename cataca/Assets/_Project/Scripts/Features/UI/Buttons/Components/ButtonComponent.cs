using System;
using _Project.Scripts.Features.UI.Buttons.Monos;

namespace _Project.Scripts.Features.UI.Buttons.Components
{
    [Serializable]
    public struct ButtonComponent
    {
        public ButtonWrapper Button;
        
        [NonSerialized] public bool WasPressed;
        [NonSerialized] public bool Pressed;
        [NonSerialized] public bool Holding;
        [NonSerialized] public bool Released;
    }
}