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

    }

}

