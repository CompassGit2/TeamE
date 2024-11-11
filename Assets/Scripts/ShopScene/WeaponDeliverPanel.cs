using System;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopScene
{   
    public class WeaponDeliverPanel : MonoBehaviour
    {
        public Action DeliverWeapon;
        [SerializeField] Image weaponImage;
        [SerializeField] TextMeshProUGUI titleText;
        [SerializeField] TextMeshProUGUI descriptionText;
        [SerializeField] TextMeshProUGUI priceText;
        [SerializeField] TextMeshProUGUI rarityText;
        [SerializeField] TextMeshProUGUI lengthText;
        [SerializeField] TextMeshProUGUI weightText;
        [SerializeField] TextMeshProUGUI sharpnessText;
        [SerializeField] TextMeshProUGUI bonusText;
        [SerializeField] Button deliverButton;

        public void Enable(Weapon weapon)
        {
            weaponImage.sprite = weapon.weapon.WeaponImage;
            titleText.text = $"{weapon.weapon.Name}を納品する？";
            descriptionText.text = weapon.weapon.Description;
            priceText.text = $"{weapon.price + Storage.AcceptedOrder.orderData.Reward}G";
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

        public void OnClickDeliverButton()
        {
            DeliverWeapon();
        }
    }
}
