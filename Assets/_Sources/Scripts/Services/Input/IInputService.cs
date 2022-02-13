using _Sources.Scripts.Infrastructure.Services;
using UnityEngine;

namespace _Sources.Scripts.Services.Input
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
