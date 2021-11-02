using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultishotWeapon : MonoBehaviour, IWeaponFeature
{
    [SerializeField] private WeaponTypeDataSO multishotWeaponData;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Player _player;

    private List<Vector2> _firePoints = new List<Vector2>();
    private float RotateAngle;
    private bool _fullRound;

    public WeaponTypeDataSO MultishotWeaponData { get => multishotWeaponData; private set => multishotWeaponData = value; }

    private void Awake()
    {
        _weapon = GetComponent<Weapon>();

        //if (!IsRotatable)
        //{
        //    AssignFirePoints();
        //}
    }

    private void AssignFirePoints()
    {
        _firePoints.Clear();
        if (_weapon.IsRotatable)
        {
            RotateAngle = _weapon.RotatableWeapon.InitialRotateAngle;
        }
        else
        {
            RotateAngle = 90;
        }

        var halfAngle = Mathf.Ceil(multishotWeaponData.FireAngle / 2);
        var segments = multishotWeaponData.NumberOfProjectiles;
        if (multishotWeaponData.RemoveAdditionalSegmentAtEnd)
        {
            segments = multishotWeaponData.NumberOfProjectiles - 1;
        }

        var startAngle = RotateAngle - halfAngle;
        var endAngle = RotateAngle + halfAngle;

        float angle = startAngle;
        float arcLength = endAngle - startAngle;
        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle);
            float y = Mathf.Cos(Mathf.Deg2Rad * angle);

            _firePoints.Add(new Vector2(x, y));

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
            RotateAngle = _weapon.RotatableWeapon.InitialRotateAngle;
        }
        else
        {
            RotateAngle = 90;
        }

        //var segments = numberOfBullets + 1;

        var startAngle = RotateAngle - halfAngle;
        var endAngle = RotateAngle + halfAngle;
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
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        AssignFirePoints();

        visitor.Visit(this); 
    }
}
