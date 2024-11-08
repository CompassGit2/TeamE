using System;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopScene
{   
    public class WeaponSellPanel : MonoBehaviour
    {
        public Action SellWeapon;
        [SerializeField] Image weaponImage;
        [SerializeField] TextMeshProUGUI titleText;
        [SerializeField] TextMeshProUGUI descriptionText;
        [SerializeField] TextMeshProUGUI priceText;
        [SerializeField] TextMeshProUGUI rarityText;
        [SerializeField] TextMeshProUGUI lengthText;
        [SerializeField] TextMeshProUGUI weightText;
        [SerializeField] TextMeshProUGUI sharpnessText;
        [SerializeField] TextMeshProUGUI bonusText;
        [SerializeField] Button sellButton;

        public void Enable(Weapon weapon)
        {
            weaponImage.sprite = weapon.weapon.WeaponImage;
            titleText.text = $"{weapon.weapon.Name}を売る？";
            descriptionText.text = weapon.weapon.Description;
            priceText.text = $"{weapon.price}G";
            rarityText.text = $"Rare{weapon.weapon.Rarity}";
            lengthText.text = $"長さ:{weapon.weapon.Length}";
            weightText.text = $"重さ:{weapon.weapon.Weight}";
            sharpnessText.text = $"鋭さ:{weapon.weapon.Sharpness}";
            bonusText.text = $"ボーナス:+{weapon.bonus}  +{weapon.price - weapon.weapon.BasePrice}G";
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void OnClickSellButton()
        {
            SellWeapon();
        }
    }
}
