using _Sources.Scripts.Battle;
using UnityEngine;

namespace _Sources.Scripts.Data
{
    public class ES3DataManager : SingletonClass<ES3DataManager>
    {
        private const string HealthConstant = "Cash";
        private const string EnergyConstant = "LaunchCount";
        private const string EquipedWeaponConstant = "EquipedWeapon";
        private const string SecondaryWeaponConstant = "SecondaryWeapon";
        private const string CurrentLevelConstant = "VolumeValue";
        private const string KilledEnemiesConstant = "KilledEnemies";

        private const string VolumeConstant = "VolumeValue";
        private const string VolumeBgConstant = "VolumeBGValue";

        public float Health { get; private set; }
        
        public float Energy { get; private set; }
        
        public GameObject EquipedWeapon { get; private set; }
        public GameObject SecondaryWeapon { get; private set; }
        public int CurrentLevel { get; set; }
        public int KilledEnemiesCount { get; set; }
        public float VolumeValue { get; set; }
        public float VolumeBgValue { get; set; }

        public override void Awake()
        {
            base.Awake();
            LoadAllData();
        }
        
        public void LoadAllData()
        {
            LoadPlayerHealth();
            LoadPlayerEnergy();
            
            LoadLevelNumber();
            
            LoadVolumeValue();
            LoadVolumeBGValue();
            
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
        

        public GameObject LoadEquipedWeapon()
        {
            if (ES3.KeyExists(EquipedWeaponConstant))
            {
                return EquipedWeapon = ES3.Load<GameObject>(EquipedWeaponConstant);
            }

            return null;
        }
        public GameObject LoadSecondaryWeapon()
        {
            if (ES3.KeyExists(SecondaryWeaponConstant))
            {
                return SecondaryWeapon = ES3.Load<GameObject>(SecondaryWeaponConstant);
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
        
        public void SaveEquipedWeapon(GameObject currentWeapon) => ES3.Save(EquipedWeaponConstant, EquipedWeapon = currentWeapon);
        public void SaveSecondaryWeapon(GameObject secondaryWeapon) => ES3.Save(SecondaryWeaponConstant, SecondaryWeapon = secondaryWeapon);
        
        public void SaveKilledEnemiesCount(int enemiesCount) => ES3.Save(KilledEnemiesConstant, KilledEnemiesCount = enemiesCount);

        
        public void SaveLevelNumber(int levelNumber) => ES3.Save(CurrentLevelConstant, CurrentLevel = levelNumber);
        public void SaveVolumeValue(float volumeValue) => ES3.Save(VolumeConstant, VolumeValue = volumeValue);
        public void SaveVolumeBGValue(float volumeValue) => ES3.Save(VolumeBgConstant, VolumeBgValue = volumeValue);

  

    }
}