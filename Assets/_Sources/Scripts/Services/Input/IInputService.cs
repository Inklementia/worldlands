using UnityEngine;

namespace _Sources.Scripts.Services.Input
{
    public interface IInputService
    {
        Vector2 Axis { get; }
        bool IsSwitchWeaponButtonPressed();
        bool IsAttackButtonUp();
        bool IsAttackButtonDown();
        bool IsPickUpButtonUp();
 
    }
}
