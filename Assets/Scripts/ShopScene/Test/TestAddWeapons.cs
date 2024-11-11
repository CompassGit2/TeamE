using System.Collections;
using System.Collections.Generic;
using Data;
using Data.Database;
using UnityEngine;

public class TestAddWeapons : MonoBehaviour
{
    [SerializeField] WeaponDatabase weaponDatabase;
    public void OnClickButton()
    {
        Weapon weapon = new Weapon(weaponDatabase.weaponList[0],0,0,200);
        Storage.AddWeapon(weapon);
        weapon = new Weapon(weaponDatabase.weaponList[0],0,0,400);
        Storage.AddWeapon(weapon);
        weapon = new Weapon(weaponDatabase.weaponList[1],0,0,300);
        Storage.AddWeapon(weapon);
        Debug.Log("武器を倉庫に追加しました");
    }
}
