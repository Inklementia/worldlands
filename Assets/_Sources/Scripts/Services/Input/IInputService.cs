using UnityEngine;

namespace _Sources.Scripts.Services.Input
{
    public interface IInputService
    {
        Vector2 Axis { get; }
        bool IsSwitchWeaponButtonPressed();
        bool IsActionButtonUp();
        bool IsActionButtonDown();
    }
}
