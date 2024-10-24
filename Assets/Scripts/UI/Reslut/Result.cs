using UnityEngine;
using UnityEngine.UI;
using Data;
using TMPro;
using UnityEngine.SceneManagement;

namespace SmithScene.Result
{
    public class Result : MonoBehaviour
    {
        public TextMeshProUGUI RankText;
        public TextMeshProUGUI weaponNameText;
        public TextMeshProUGUI qualityText;
        public TextMeshProUGUI bonusText;
        public TextMeshProUGUI lengthText;                 
        public TextMeshProUGUI weightText;                 
        public TextMeshProUGUI sharpnessText;                 
        public TextMeshProUGUI attackText;                 
        public TextMeshProUGUI Price;
        public Image SwordImage;
        public GameObject WeaponPanel;
        public GameObject LosePanel;
        Weapon _weapon = null;

        public void DisplayWeapon(Weapon weapon, float quality)
        {
            _weapon = weapon;
            LosePanel.SetActive(false);
            WeaponPanel.SetActive(true);
            gameObject.SetActive(true);
            qualityText.text = $"{quality.ToString("f1")}%";
            weaponNameText.text = weapon.weapon.Name;
            bonusText.text = _weapon.bonus.ToString();
            Price.text = _weapon.price.ToString();
            lengthText.text = _weapon.weapon.Length.ToString();
            weightText.text = _weapon.weapon.Weight.ToString();
            sharpnessText.text = _weapon.weapon.Sharpness.ToString(); 
            SwordImage.sprite = _weapon.weapon.WeaponImage;


            if (_weapon.bonus <= 5)
            {
                RankText.text = "B";

            }
            else if (_weapon.bonus <= 10)
            {
                RankText.text = "A";

            }
            else if (_weapon.bonus <= 15)
            {
                RankText.text = "S";

            }
            else if (_weapon.bonus <= 20)
            {
                RankText.text = "SS";

            }
            else if (_weapon.bonus > 20)
            {
                RankText.text = "SSS";
            }

        }

        public void DisplayLose(float quality)
        {
            qualityText.text = $"{quality.ToString("f1")}%";
            RankText.text = "D";
            WeaponPanel.SetActive(false);
            LosePanel.SetActive(true);
        }

        //ID?��ő{?��?��?��o?��[?��W?��?��?��?��

        //public void DisplayWeaponById(int weaponId)
        //{
        //    // ?��?��?��?��f?��[?��^?��?��ID?��?��?��?��擾
        //    WeaponData weaponData = GetWeaponById(weaponId);
        //    if (weaponData != null)
        //    {
        //        weaponText.text += $"?��?��?��A?��?��?��e?��B: {weaponData.Rarity},?��?��?��?��: {weaponData.Length}," +
        //$"?��d?��?��: {weaponData.Weight},?��s?��?��: {weaponData.Sharpness}\n";
        //        SwordImage.sprite = weaponData.WeaponImage; // ?��?��?��̉摜


        //    }
        //    else
        //    {
        //        // ?��?��?���?��?��Ȃ�?��?��?��?��?���??��̏�?��?��
        //        weaponText.text = "Weapon not found!";
        //    }
        //}

        //private WeaponData GetWeaponById(int weaponId)
        //{
        //    // WeaponDatabase?��?��?��?��ID?��Ɋ�Â�?��ĕ�?��?��f?��[?��^?��?��?��擾
        //    foreach (var weapon in weaponDatabase.GetWeaponList())
        //    {
        //        if (weapon.Id == weaponId)
        //        {
        //            return weapon;
        //        }
        //    }
        //    return null; 
        //}
        public void SendShop()
        {

        }

        public void BottomClose()
        {
            if(_weapon != null)
            {
                Storage.AddWeapon(_weapon);

            }

            SceneManager.LoadScene("MenuScene");
        }
    }

}


