using UnityEngine;
using Data;
using System.Collections.Generic;

namespace Database
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

