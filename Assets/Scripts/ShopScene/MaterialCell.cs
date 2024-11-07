using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using FancyScrollView;
using CommonUI;

namespace ShopScene
{
    public class MaterialCell : FancyGridViewCell<MaterialStack, Context>
    {
        [SerializeField] TextMeshProUGUI itemName;
        [SerializeField] Image itemImage;
        [SerializeField] TextMeshProUGUI itemPrice;
        [SerializeField] TextMeshProUGUI stock;
        [SerializeField] Button button;

        public override void Initialize()
        {
            button.onClick.AddListener(() => Context.OnCellClicked?.Invoke(Index));
        }

        public override void UpdateContent(MaterialStack itemData)
        {
            itemName.text = itemData.material.Name;
            itemImage.sprite = itemData.material.MaterialImage;
            itemPrice.text = $"{itemData.material.Price}G";
            stock.text = $"在庫:{itemData.amount}";
        }

        public override void UpdatePosition(float position)
        {
            // position は 0.0 ~ 1.0 の値です
            // position に基づいてスクロールの外観を自由に制御できます
            base.UpdatePosition(position);
        }
    }
}
