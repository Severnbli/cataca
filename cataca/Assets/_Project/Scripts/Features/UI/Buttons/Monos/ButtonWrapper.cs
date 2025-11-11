using UnityEngine.UI;

namespace _Project.Scripts.Features.UI.Buttons.Monos
{
    public class ButtonWrapper : Button
    {
        private bool _wasPressedLastFrame;
        
        public bool Pressed { get; private set; }
        public bool Holding { get; private set; }
        public bool Released { get; private set; }
        
        private void Update() {
            var pressed = IsPressed();

            Pressed = !_wasPressedLastFrame && pressed;
            Released = _wasPressedLastFrame && !pressed;
            Holding = pressed;

            _wasPressedLastFrame = pressed;
        }
    }
}