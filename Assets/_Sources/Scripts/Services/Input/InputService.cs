using UnityEngine;

namespace _Sources.Scripts.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const string SwitchWeapon = "SwitchWeapon";
        protected const string Attack = "Attack";
        protected const string PickUp = "PickUp";

        
        public bool IsSwitchWeaponButtonPressed() =>
            SimpleInput.GetButtonUp(SwitchWeapon);
        
        public bool IsAttackButtonUp() =>
            SimpleInput.GetButtonUp(Attack);
        
        public bool IsAttackButtonDown() =>
            SimpleInput.GetButtonDown(Attack);
        public bool IsPickUpButtonUp() =>
            SimpleInput.GetButtonUp(PickUp);

        public abstract Vector2 Axis { get; }

        protected static Vector2 SimpleInputAxis() =>
            new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));

    }
}