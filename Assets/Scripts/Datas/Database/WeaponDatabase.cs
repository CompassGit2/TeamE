using UnityEngine;
using System.Collections.Generic;

namespace Data.Database
{
    [CreateAssetMenu(menuName = "Database/WeaponDatabase")]
    public class WeaponDatabase : ScriptableObject
    {
        public List<WeaponData> weaponList = new List<WeaponData>();

        public List<WeaponData> GetWeaponList()
        {
            return weaponList;
        }

        public WeaponData GetWeaponData(string weaponName)
        {
            foreach (WeaponData weapon in weaponList)
            {
                if (weapon.Name == weaponName)
                {
                    return weapon;
                }
            }
            return new WeaponData();
        }

    }

}

