using UnityEngine;
using UnityEngine.UI;
using CommonUI;
using Data;
using FancyScrollView;
using TMPro;

namespace ShopScene
{
    public class WeaponCell : FancyGridViewCell<Weapon, Context>
    {
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] Image icon;
        [SerializeField] TextMeshProUGUI rarityText;
        [SerializeField] TextMeshProUGUI priceText;
        [SerializeField] Button button;
        
        public override void Initialize()
        {
            button.onClick.AddListener(() => Context.OnCellClicked?.Invoke(Index));
        }

        public override void UpdateContent(Weapon itemData)
        {
            nameText.text = itemData.weapon.Name;
            icon.sprite = itemData.weapon.WeaponImage;
            rarityText.text = $"Rare:{itemData.weapon.Rarity}";
            priceText.text = $"{itemData.price}G";

        }

        public override void UpdatePosition(float position)
        {
            // position は 0.0 ~ 1.0 の値です
            // position に基づいてスクロールの外観を自由に制御できます
            base.UpdatePosition(position);
        }
    }

}
