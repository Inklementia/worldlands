using _Sources.Scripts.Battle;
using _Sources.Scripts.Helpers;
using _Sources.Scripts.Player;
using _Sources.Scripts.Weapons;
using UnityEngine;

namespace _Sources.Scripts.Data
{
    public class ES3DataManager : MonoBehaviour
    {
        public static ES3DataManager Instance;
        private const string HealthConstant = "Health";
        private const string EnergyConstant = "Energy";
        private const string PlayerConstant = "Player";
        private const string EquipedWeaponConstant = "EquipedWeapon";
        private const string SecondaryWeaponConstant = "SecondaryWeapon";
        private const string CurrentLevelConstant = "CurrentLevel";
        private const string KilledEnemiesConstant = "KilledEnemies";

        private const string VolumeConstant = "VolumeValue";
        private const string VolumeBgConstant = "VolumeBGValue";

        public float Health;
        public float Energy;

        public string EquipedWeaponName;
        public string SecondaryWeaponName;
        public int CurrentLevel;
        public int KilledEnemiesCount;
        public float VolumeValue;
        public float VolumeBgValue { get; set; }

        public void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            LoadAllData();
            
           
        }
        
        public void LoadAllData()
        {
            LoadPlayerHealth();
            LoadPlayerEnergy();

            LoadLevelNumber();

            LoadEquipedWeapon();
            LoadSecondaryWeapon();
            
            LoadKilledEnemiesValue();
         
        }

        
        public float LoadPlayerHealth()
        {
            if (ES3.KeyExists(HealthConstant))
            {
                return Health = ES3.Load<float>(HealthConstant);
            }

            return 0;
        }
        
        public float LoadPlayerEnergy()
        {
            if (ES3.KeyExists(EnergyConstant))
            {
                return Energy = ES3.Load<float>(EnergyConstant);
            }

            return 0;
        }

        public float LoadVolumeBGValue()
        {
            if (ES3.KeyExists(VolumeBgConstant))
            {
                return VolumeBgValue = ES3.Load<float>(VolumeBgConstant);
            }

            return 0;
        }
        public float LoadVolumeValue()
        {
            if (ES3.KeyExists(VolumeConstant))
            {
                return VolumeValue = ES3.Load<float>(VolumeConstant);
            }

            return 0;
        }
        public int LoadLevelNumber()
        {
            if (ES3.KeyExists(CurrentLevelConstant))
            {
                return CurrentLevel = ES3.Load<int>(CurrentLevelConstant);
            }

            return 1;
        }
        

        public string LoadEquipedWeapon()
        {
            if (ES3.KeyExists(EquipedWeaponConstant))
            {
                return EquipedWeaponName = ES3.Load<string>(EquipedWeaponConstant);
            }
        return null;

        }
        

        public string LoadSecondaryWeapon()
        {
            if (ES3.KeyExists(SecondaryWeaponConstant))
            {
               
                 return SecondaryWeaponName = ES3.Load<string>(SecondaryWeaponConstant);

            }
            return null;
  
        }
        public int LoadKilledEnemiesValue()
        {
            if (ES3.KeyExists(KilledEnemiesConstant))
            {
                return KilledEnemiesCount = ES3.Load<int>(KilledEnemiesConstant);
            }

            return 0;
        }
        public void SavePlayerHealthState(float healthAmount) => ES3.Save(HealthConstant, Health = healthAmount);
        public void SavePlayerEnergyState(float energyAmount) => ES3.Save(EnergyConstant, Energy = energyAmount);
        
        public void SaveEquipedWeapon(string currentWeapon) => ES3.Save(EquipedWeaponConstant, EquipedWeaponName = currentWeapon);
        public void SaveSecondaryWeapon(string secondaryWeapon) => ES3.Save(SecondaryWeaponConstant, SecondaryWeaponName = secondaryWeapon);
        
        public void SaveKilledEnemiesCount(int enemiesCount) => ES3.Save(KilledEnemiesConstant, KilledEnemiesCount = enemiesCount);

        
        public void SaveLevelNumber(int levelNumber) => ES3.Save(CurrentLevelConstant, CurrentLevel = levelNumber);
        public void SaveVolumeValue(float volumeValue) => ES3.Save(VolumeConstant, VolumeValue = volumeValue);
        public void SaveVolumeBGValue(float volumeValue) => ES3.Save(VolumeBgConstant, VolumeBgValue = volumeValue);

  
        public void DeleteLevelNumber()
        {
            if (ES3.KeyExists(CurrentLevelConstant))
            {
                ES3.DeleteKey(CurrentLevelConstant);
            }
        }
        public void DeleteHealth()
        {
            if (ES3.KeyExists(HealthConstant))
            {
                ES3.DeleteKey(HealthConstant);
            }
        }
        public void DeleteEnergy()
        {
            if (ES3.KeyExists(EnergyConstant))
            {
                ES3.DeleteKey(EnergyConstant);
            }
        }
        
    }
}