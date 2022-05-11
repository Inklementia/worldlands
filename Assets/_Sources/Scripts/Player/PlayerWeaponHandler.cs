using System;
using _Sources.Scripts.Data;
using _Sources.Scripts.Input;
using _Sources.Scripts.Object_Pooler;
using _Sources.Scripts.Player.PlayerFiniteStateMachine;
using _Sources.Scripts.Structs;
using _Sources.Scripts.Weapons;
using DG.Tweening;
using UnityEngine;

namespace _Sources.Scripts.Player
{
    public class PlayerWeaponHandler : MonoBehaviour
    {
        public PlayerInputHandler InputHandler { get; private set; }
        public PlayerWeaponry Weaponry { get; private set; }
        public PlayerEntity Player { get; private set; }

        [SerializeField] private int weaponsNumberToCarry = 2;
        [SerializeField] private Tag weaponTag;
        [SerializeField] private GameObject firstWeaponPrefab;
        private EncounteredWeapon _encounteredWeapon;
        public GameObject RefillEnergyText;

        private void Awake()
        {
            InputHandler = GetComponent<PlayerInputHandler>();
            Weaponry = GetComponentInChildren<PlayerWeaponry>();
            Player = GetComponent<PlayerEntity>();


        }
        // Start is called before the first frame update
        void Start()
        {
            /*
            if (ES3DataManager.Instance.CurrentLevel == 1 )
            {
                GameObject weapon = Instantiate(firstWeaponPrefab, transform.position, Quaternion.identity);
                weapon.name = firstWeaponPrefab.name;
                //Weaponry.EquipWeapon(weapon.GetComponent<ShootingWeapon>());
            }
            else
            {
                LoadWeaponsIfExist();
            }
*/
           
        }

        public void LoadWeaponsIfExist()
        {
          
            //InputHandler.EnablePickUpButton();
            if (ES3DataManager.Instance.EquipedWeaponName != null)
            {
                UnityEngine.Object pPrefab = Resources.Load("Weapons/"+ES3DataManager.Instance.EquipedWeaponName); // note: not .prefab!
              
                GameObject weapon1 = (GameObject)GameObject.Instantiate(pPrefab, Vector3.zero, Quaternion.identity);
                weapon1.name = ES3DataManager.Instance.EquipedWeaponName;
                Weaponry.EquipWeapon(weapon1.GetComponent<ShootingWeapon>());
            }
            if (ES3DataManager.Instance.SecondaryWeaponName != null)
            {
                UnityEngine.Object pPrefab = Resources.Load("Weapons/"+ES3DataManager.Instance.SecondaryWeaponName); // note: not .prefab!
                GameObject weapon2 = (GameObject)GameObject.Instantiate(pPrefab, Vector3.zero, Quaternion.identity);
                weapon2.name = ES3DataManager.Instance.SecondaryWeaponName;
                Weaponry.EquipWeapon(weapon2.GetComponent<ShootingWeapon>());
            }

        }
        // Update is called once per frame
        private void Update()
        {
            //_encounteredWeapon = Player.Core.CollisionSenses.EncounteredWeapon;

            //do events

            ManageSwitchingAndEquipingWeapons();
            ManageAttacking();


        }

        private void LateUpdate()
        {
            bool once = true;
            if (once)
            {
                
            }
        }

        private void ManageSwitchingAndEquipingWeapons()
        {
            // if more than 1 weapons
            if (Weaponry.CanSwitchWeapons)
            {
                InputHandler.EnableWeaponSwitch();
            }
            else
            {
                InputHandler.DisableWeaponSwitch();
            }
            // switching 
            if (InputHandler.IsSwitchWeaponButtonPressed)
            {
                Weaponry.SwitchWeapon();
            }

    
            // if player is near weapon
            if (_encounteredWeapon.Weapon != null )
            {
         
                // activate Action button
                InputHandler.EnablePickUpButton();
         
                // if Action button is pressed and Player can take 1 more weapon
                if (InputHandler.IsPickUpButtonPressed && Weaponry.CarriedWeapons.Count < weaponsNumberToCarry)
                {
                    Weaponry.EquipWeapon(_encounteredWeapon.Weapon);
                    //InputHandler.EnableWeaponSwitchButton();
                    _encounteredWeapon.Weapon = null;
                }
                else if (InputHandler.IsPickUpButtonPressed && Weaponry.CarriedWeapons.Count >= weaponsNumberToCarry)
                {
                    Weaponry.DropCurrentWeapon(_encounteredWeapon.Position);
                    Weaponry.EquipWeapon(_encounteredWeapon.Weapon);
                }
                //otherwise drop current and pick - up new
            }
            else
            {
                //StartCoroutine(WaitAndChangeActionModeOnAttack());
                InputHandler.EnableAttackButton();
          
            }
        }
        private void ManageAttacking()
        {
            // if player has weapon
            if (Weaponry.CurrentWeapon != null)
            {
                InputHandler.ShowEnergyIndicator(true);
                InputHandler.SetEnergyCostIndicator( Weaponry.CurrentWeapon.BaseWeaponData.EnergyCostPerAttack);
                if (Player.Core.EnergySystem.CurrentStat > Weaponry.CurrentWeapon.BaseWeaponData.EnergyCostPerAttack)
                {
                    RefillEnergyText.SetActive(false);
                    if(InputHandler.IsAttackButtonPressedUp && !Weaponry.CurrentWeapon.ShouldBeCharged)
                    {
                        Weaponry.CurrentWeapon.Attack();
                        Player.Core.EnergySystem.DecreaseStat(Weaponry.CurrentWeapon.BaseWeaponData.EnergyCostPerAttack);


                    }
                    else if (InputHandler.IsAttackButtonPressed && Weaponry.CurrentWeapon.ShouldBeCharged)
                    {
                        // charging
                        Weaponry.CurrentWeapon.ChargeableWeapon.Charge();
                    }
                    else if(InputHandler.IsAttackButtonPressedUp && (Weaponry.CurrentWeapon.ShouldBeCharged && Weaponry.CurrentWeapon.Charged))
                    {
                        Weaponry.CurrentWeapon.Attack();
                        Player.Core.EnergySystem.DecreaseStat(Weaponry.CurrentWeapon.BaseWeaponData.EnergyCostPerAttack);
                    }
                }
                else
                {
                    RefillEnergyText.SetActive(true);
                }
            
            }

        }
        //


        private void OnTriggerEnter2D(Collider2D collision)
        {
      
            if (collision.HasTag(weaponTag))
            {
         
              
                _encounteredWeapon.Position = collision.transform;
                _encounteredWeapon.Weapon = collision.GetComponent<ShootingWeapon>();
                //Debug.Log("Encountered weapon: "+ _encounteredWeapon.Weapon.BaseWeaponData.Name);
          
            }
        }
        /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        bool once = true;
        if (collision.HasTag(WeaponTag) && once)
        {
            _encounteredWeapon.Position = collision.transform;
            _encounteredWeapon.Weapon = collision.GetComponent<ShootingWeapon>();
            once = false;
        }
    }*/
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.HasTag(weaponTag))
            {
                
                _encounteredWeapon.Weapon = null;
            }
        }
    }
}
