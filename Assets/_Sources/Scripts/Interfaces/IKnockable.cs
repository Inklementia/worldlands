using UnityEngine;

namespace _Sources.Scripts.Interfaces
{
    public interface IKnockable
    {
        void KnockBack(Vector2 angle, float strength, int direction);
    
    }
}
