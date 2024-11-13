using CommonUI;
using Data;
using FancyScrollView;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopScene
{
    public class OrderCell : FancyGridViewCell<Order, Context>
    {
        [SerializeField] TextMeshProUGUI orderTypeText;
        [SerializeField] Image orderIcon;
        [SerializeField] TextMeshProUGUI descriptionText;
        [SerializeField] TextMeshProUGUI requirementTypeText;
        [SerializeField] TextMeshProUGUI requirementDetailText;
        [SerializeField] Button button;

        public override void Initialize()
        {
            button.onClick.AddListener(() => Context.OnCellClicked?.Invoke(Index));
        }

        public override void UpdateContent(Order order)
        {
            switch(order.orderData.OrderType)
            {
                case OrderType.Normal:
                    orderTypeText.text = "通常依頼"; 
                    break;
                case OrderType.Special:
                    orderTypeText.text = "特別依頼";
                    break;
            }
            orderIcon.sprite = order.orderData.OrderImage;
            descriptionText.text = order.orderData.Description;
            
            switch(order.orderData.RequirementType)
            {
                case Requirements.ByData:
                    requirementTypeText.text = "納品条件:武器種";
                    requirementDetailText.text = $"武器名:{order.orderData.weaponData.Name}";
                    break;
                case Requirements.SpecSpecifications:
                    requirementTypeText.text = "納品条件:スペック";
                    requirementDetailText.text = "";
                    if(order.orderData.specRequirements.checkLength)
                    requirementDetailText.text += $"長さ:{order.orderData.specRequirements.requiredLength}\n";
                    if(order.orderData.specRequirements.checkWeight)
                    requirementDetailText.text += $"重さ:{order.orderData.specRequirements.requiredWeight}\n";
                    if(order.orderData.specRequirements.checkSharpness)
                    requirementDetailText.text += $"鋭さ:{order.orderData.specRequirements.requiredSharpness}\n";
                    break;
                case Requirements.Rarity:
                    requirementTypeText.text = "納品条件:レアリティ";
                    requirementDetailText.text = $"Rare{order.orderData.RequiredRarity}以上";
                    break;
            }
        }

        public override void UpdatePosition(float position)
        {
            // position は 0.0 ~ 1.0 の値です
            // position に基づいてスクロールの外観を自由に制御できます
            base.UpdatePosition(position);
        }
    }
}
