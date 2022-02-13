using _Sources.Scripts.Weapons.Weapon_Features;

namespace _Sources.Scripts.Weapons
{
    public interface IVisitor
    {
        void Visit(IWeaponFeature weapon);

    }
}
