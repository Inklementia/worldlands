using System.Collections.Generic;
using _Sources.Scripts.Player.PlayerFiniteStateMachine;
using UnityEngine;

namespace _Sources.Scripts.Weapons.Weapon_Features
{
    public class MultishotWeapon : MonoBehaviour, IWeaponFeature
    {
        [SerializeField] private WeaponTypeDataSO multishotWeaponData;
        [SerializeField] private ShootingWeapon _weapon;
    
        private PlayerEntity _playerEntity;

        public List<Vector2> FirePoints { get; private set; }

        private float _angle;
        private bool _fullRound;

        public WeaponTypeDataSO MultishotWeaponData { get => multishotWeaponData; private set => multishotWeaponData = value; }

        private void Awake()
        {
            FirePoints = new List<Vector2>();
            _weapon = GetComponent<ShootingWeapon>();

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
                _angle = _weapon.RotatableWeapon.InitialRotateAngle;
            }
            else
            {
                _angle = 90;
            }

            var halfAngle = Mathf.Ceil(multishotWeaponData.FireAngle / 2);
            var segments = multishotWeaponData.NumberOfProjectiles;

            if (multishotWeaponData.RemoveAdditionalSegmentAtEnd)
            {
                segments = multishotWeaponData.NumberOfProjectiles - 1;
            }

            var startAngle = _angle - halfAngle;
            var endAngle = _angle + halfAngle;
       
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
                _angle = _weapon.RotatableWeapon.InitialRotateAngle;
            }
            else
            {
                _angle = 90;
            }

            //var segments = numberOfBullets + 1;

            var startAngle = _angle - halfAngle;
            var endAngle = _angle + halfAngle;

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
            _playerEntity = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.PlayerFiniteStateMachine.PlayerEntity>();
            AssignFirePoints();

            visitor.Visit(this); 
        }
    }
}
