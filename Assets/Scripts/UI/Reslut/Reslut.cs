using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Data;
using Data.Database;
using TMPro;

namespace SmithScene.Result
{
    public class Reslut : MonoBehaviour
    {
        public TextMeshProUGUI weaponText;                
        public WeaponDatabase weaponDatabase;  
        public TextMeshProUGUI bonusText;
        public TextMeshProUGUI RankText;
        public TextMeshProUGUI Price;
        [SerializeField] GameObject ResultObject;
        public Image SwordImage;


        //Start is called before the first frame update
        //void Start()
        //{

        //}

        public void DisplayWeapon(Weapon weapon)
        {
            bonusText.text += $"ボーナス: {weapon.bonus}\n";
            Price.text += $"価格: {weapon.price}\n";
            weaponText.text += $"レアリティ: {weapon.weapon.Rarity},長さ: { weapon.weapon.Length}," +
                $"重さ: {weapon.weapon.Weight},鋭さ: {weapon.weapon.Sharpness}\n";
            SwordImage.sprite = weapon.weapon.WeaponImage;//剣の画像

            // テキストを画面に表示


            if (weapon.bonus == 0)
            {

            }
            else if (weapon.bonus <= 5)
            {

            }
            else if (weapon.bonus <= 10)
            {

            }
            else if (weapon.bonus <= 15)
            {

            }
            else if (weapon.bonus <= 20)
            {
                RankText.text = "SS";
            }

        }
        //IDで捜すバージョン

        //public void DisplayWeaponById(int weaponId)
        //{
        //    // 武器データをIDから取得
        //    WeaponData weaponData = GetWeaponById(weaponId);
        //    if (weaponData != null)
        //    {
        //        weaponText.text += $"レアリティ: {weaponData.Rarity},長さ: {weaponData.Length}," +
        //$"重さ: {weaponData.Weight},鋭さ: {weaponData.Sharpness}\n";
        //        SwordImage.sprite = weaponData.WeaponImage; // 剣の画像


        //    }
        //    else
        //    {
        //        // 見つからなかった場合の処理
        //        weaponText.text = "Weapon not found!";
        //    }
        //}

        //private WeaponData GetWeaponById(int weaponId)
        //{
        //    // WeaponDatabaseからIDに基づいて武器データを取得
        //    foreach (var weapon in weaponDatabase.GetWeaponList())
        //    {
        //        if (weapon.Id == weaponId)
        //        {
        //            return weapon;
        //        }
        //    }
        //    return null; 
        //}


        public void BottomClose()
        {
            ResultObject.SetActive(false);
        }
    }

}


