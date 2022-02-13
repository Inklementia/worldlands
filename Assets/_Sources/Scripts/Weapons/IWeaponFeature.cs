namespace _Sources.Scripts.Weapons
{
    public interface IWeaponFeature
    {
        void Accept(IVisitor visitor);
    }
}
