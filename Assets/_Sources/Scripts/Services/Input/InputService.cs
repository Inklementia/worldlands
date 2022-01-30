using UnityEngine;

namespace _Sources.Scripts.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const string SwitchWeapon = "SwitchWeapon";
        protected const string Action = "Action";

        
        public bool IsSwitchWeaponButtonPressed() =>
            SimpleInput.GetButtonUp(SwitchWeapon);
        
        public bool IsActionButtonUp() =>
            SimpleInput.GetButtonUp(Action);
        
        public bool IsActionButtonDown() =>
            SimpleInput.GetButtonDown(Action);
        
        public abstract Vector2 Axis { get; }

        protected static Vector2 SimpleInputAxis() =>
            new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));

    }
}