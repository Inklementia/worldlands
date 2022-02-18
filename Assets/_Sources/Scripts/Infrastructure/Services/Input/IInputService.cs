using UnityEngine;

namespace _Sources.Scripts.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        bool IsSwitchWeaponButtonPressed();
        bool IsAttackButtonUp();
        bool IsAttackButtonDown();
        bool IsAttackButtonPressed();
        bool IsPickUpButtonUp();
 
    }
}
