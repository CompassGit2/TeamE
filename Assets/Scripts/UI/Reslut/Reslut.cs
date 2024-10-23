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
            bonusText.text += $"�{�[�i�X: {weapon.bonus}\n";
            Price.text += $"���i: {weapon.price}\n";
            weaponText.text += $"���A���e�B: {weapon.weapon.Rarity},����: { weapon.weapon.Length}," +
                $"�d��: {weapon.weapon.Weight},�s��: {weapon.weapon.Sharpness}\n";
            SwordImage.sprite = weapon.weapon.WeaponImage;//���̉摜

            // �e�L�X�g����ʂɕ\��


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
        //ID�ő{���o�[�W����

        //public void DisplayWeaponById(int weaponId)
        //{
        //    // ����f�[�^��ID����擾
        //    WeaponData weaponData = GetWeaponById(weaponId);
        //    if (weaponData != null)
        //    {
        //        weaponText.text += $"���A���e�B: {weaponData.Rarity},����: {weaponData.Length}," +
        //$"�d��: {weaponData.Weight},�s��: {weaponData.Sharpness}\n";
        //        SwordImage.sprite = weaponData.WeaponImage; // ���̉摜


        //    }
        //    else
        //    {
        //        // ������Ȃ������ꍇ�̏���
        //        weaponText.text = "Weapon not found!";
        //    }
        //}

        //private WeaponData GetWeaponById(int weaponId)
        //{
        //    // WeaponDatabase����ID�Ɋ�Â��ĕ���f�[�^���擾
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


