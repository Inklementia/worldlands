using System;
using System.Collections.Generic;
using System.Linq;
using _Sources.Scripts.Enemies.State_Mashine;
using _Sources.Scripts.Player;
using _Sources.Scripts.Player.PlayerFiniteStateMachine;
using UnityEngine;

namespace _Sources.Scripts.Weapons.Weapon_Features
{
    public class RotatableWeapon : MonoBehaviour, IWeaponFeature
    {
        [SerializeField] private bool rotateOnAttack;
        [SerializeField] private WeaponTypeDataSO rotatableWeaponData;
        [SerializeField] private float initialAngle = 90;


        //[SerializeField] private DungeonManager dm;
        
        public AimHelper Aimhelper { get; private set; }
        public float RotateAngle { get; private set; }
        public float InitialRotateAngle { get; private set; }

        public PlayerEntity PlayerEntity { get; private set; }
        public WeaponTypeDataSO RotatableWeaponData { get => rotatableWeaponData; private set => rotatableWeaponData = value; }


        private void Awake()
        {
            InitialRotateAngle = initialAngle;
            Aimhelper = GetComponent<AimHelper>();
        }
        
        private void FixedUpdate()
        {
            //Debug.Log(InitialRotateAngle);
            if (PlayerEntity != null)
            {
                if (PlayerEntity.Core.EnemyDetectionSenses.EnemyInFieldOfView())
                {
                    
                    RotateWeapon(Aimhelper.GetDirection());
                }
                else
                {
                    if (rotateOnAttack && PlayerEntity.InputHandler.IsAttackButtonPressedDown && PlayerEntity.InputHandler.CkeckIfJoystickPressed())
                    {
                        RotateWeapon(PlayerEntity.InputHandler.MovementPos);
                    }
                    if (PlayerEntity.InputHandler.CkeckIfJoystickPressed() && !rotateOnAttack)
                    {
                        RotateWeapon(PlayerEntity.InputHandler.MovementPos);
                    }
                }
               
            }
        }

        private void RotateWeapon(Vector2 where)
        {
          
            RotateAngle = Mathf.Atan2(where.x, where.y) * Mathf.Rad2Deg;
            InitialRotateAngle = Mathf.Atan2(where.x, where.y) * Mathf.Rad2Deg;
            if (RotateAngle < 0)
            {
                RotateAngle = -RotateAngle;
                gameObject.transform.rotation = Quaternion.Euler(0, 180, -(RotateAngle - initialAngle));
                
            }
            else
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, -(RotateAngle - initialAngle));
               
            }
        }
        
        
        
        public void Accept(IVisitor visitor)
        {
            PlayerEntity = GameObject.FindWithTag("Player").GetComponent<PlayerEntity>();
            Aimhelper.AssignPlayerToAimHelper(PlayerEntity);
            //_enemiesList = dm.SpawnedEnemies;
            if (PlayerEntity.Core.EnemyDetectionSenses.EnemyInFieldOfView())
            {
                Aimhelper.DetectEnemies();
            }
        
       
            visitor.Visit(this);

            //_player.WeaponHandler.Weaponry.CurrentWeapon.Accept(rotatable)
        }

       
        public void UnsetPlayer()
        { 
            PlayerEntity = null;
        }
        
 
 
       
    }
}
