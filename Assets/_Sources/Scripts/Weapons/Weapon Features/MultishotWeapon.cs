using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultishotWeapon : MonoBehaviour, IWeaponFeature
{
    [SerializeField] private WeaponTypeDataSO multishotWeaponData;
    [SerializeField] private Weapon _weapon;
    
    private Player _player;

    public List<Vector2> FirePoints { get; private set; }

    private float Angle;
    private bool _fullRound;

    public WeaponTypeDataSO MultishotWeaponData { get => multishotWeaponData; private set => multishotWeaponData = value; }

    private void Awake()
    {
        FirePoints = new List<Vector2>();
        _weapon = GetComponent<Weapon>();

        //if (!IsRotatable)
        //{
        //    AssignFirePoints();
        //}
    }

    public void AssignFirePoints()
    {
        //Debug.Log("Assigned" + RotateAngle);
        FirePoints.Clear();
        if (_weapon.IsRotatable)
        {
            Angle = _weapon.RotatableWeapon.InitialRotateAngle;
        }
        else
        {
            Angle = 90;
        }

        var halfAngle = Mathf.Ceil(multishotWeaponData.FireAngle / 2);
        var segments = multishotWeaponData.NumberOfProjectiles;

        if (multishotWeaponData.RemoveAdditionalSegmentAtEnd)
        {
            segments = multishotWeaponData.NumberOfProjectiles - 1;
        }

        var startAngle = Angle - halfAngle;
        var endAngle = Angle + halfAngle;
       
        float angle = startAngle;
        float arcLength = endAngle - startAngle;
        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle);
            float y = Mathf.Cos(Mathf.Deg2Rad * angle);

            FirePoints.Add(new Vector2(x, y));

            angle += (arcLength / segments);
        }
    }

    private void OnDrawGizmos()
    {

        // 30
        // 15
        var halfAngle = Mathf.Ceil(multishotWeaponData.FireAngle / 2);
        var segments = multishotWeaponData.NumberOfProjectiles;
        if (multishotWeaponData.RemoveAdditionalSegmentAtEnd)
        {
            segments = multishotWeaponData.NumberOfProjectiles - 1;
        }


        if (_weapon.IsRotatable)
        {
            Angle = _weapon.RotatableWeapon.InitialRotateAngle;
        }
        else
        {
            Angle = 90;
        }

        //var segments = numberOfBullets + 1;

        var startAngle = Angle - halfAngle;
        var endAngle = Angle + halfAngle;

        List<Vector2> arcPoints = new List<Vector2>();

        float angle = startAngle;
        float arcLength = endAngle - startAngle;
        for (int i = 0; i <= segments; i++)
        {

            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * 2;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * 2;

            arcPoints.Add(new Vector2(x, y));

            Gizmos.color = Color.white;
            Gizmos.DrawLine(_weapon.AttackPosition.position, new Vector2(_weapon.AttackPosition.position.x + x, _weapon.AttackPosition.position.y + y));

            angle += (arcLength / segments);
        }
    }
    public void Accept(IVisitor visitor)
    {
        _player = gameObject.FindWithTag(MultishotWeaponData.PlayerTag).GetComponent<Player>();
        AssignFirePoints();

        visitor.Visit(this); 
    }
}
