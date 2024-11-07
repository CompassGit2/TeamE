using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopScene
{
    public class BuyPanel : MonoBehaviour
    {
        public Action<int> BuyMaterial;
        [SerializeField] Image materialImage;
        [SerializeField] TextMeshProUGUI titleText;
        [SerializeField] TextMeshProUGUI descriptionText;
        [SerializeField] TextMeshProUGUI priceText;
        [SerializeField] TextMeshProUGUI stockText;
        [SerializeField] TextMeshProUGUI warehouseAmountText;

        int buyAmount = 1;
        int price;
        int stock;
        int maxNum;

        [SerializeField] GameObject buyAmountPanel;
        [SerializeField] Slider slider;
        [SerializeField] TextMeshProUGUI sliderText;
        [SerializeField] TextMeshProUGUI maxText;
        [SerializeField] GameObject noMoneyPanel;
        [SerializeField] Button buyButton;

        public void Enable(MaterialStack materialStack)
        {
            materialImage.sprite = materialStack.material.MaterialImage;
            titleText.text = $"{materialStack.material.Name}を買う？";
            descriptionText.text = materialStack.material.Description;

            price = materialStack.material.Price;
            priceText.text = $"{price*buyAmount}G";
            
            stock = materialStack.amount;
            stockText.text = $"在庫数:{buyAmount}/{stock}";

            warehouseAmountText.text = $"所持数:{CheckWarehouseAmount(materialStack.material)}";

            if(Storage.Gold >= price)
            {
                maxNum = Mathf.Min(stock, Storage.Gold/price);
                slider.maxValue = maxNum;
                maxText.text = $"{maxNum}";
                
                slider.value = buyAmount;
                sliderText.text = $"購入数:{buyAmount}";
                buyAmountPanel.SetActive(true);
                noMoneyPanel.SetActive(false);
                buyButton.interactable = true;
            }
            else
            {
                buyAmountPanel.SetActive(false);
                noMoneyPanel.SetActive(true);
                buyButton.interactable = false;
            }
            
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void OnClickIncreaseButton()
        {
            if(buyAmount < maxNum)
            {
                buyAmount ++;
                slider.value = buyAmount;
                sliderText.text = $"購入数:{buyAmount}";
                priceText.text = $"{price*buyAmount}G";
                stockText.text = $"在庫数:{buyAmount}/{stock}";
            }
        }

        public void OnClickDecreaseButton()
        {
            if(buyAmount > 1)
            {
                buyAmount --;
                slider.value = buyAmount;
                sliderText.text = $"購入数:{buyAmount}";
                priceText.text = $"{price*buyAmount}G";
                stockText.text = $"在庫数:{buyAmount}/{stock}";
            }
        }

        public void OnSliderValueChanged()
        {
            buyAmount = (int)slider.value;
            sliderText.text = $"購入数:{buyAmount}";
            priceText.text = $"{price*buyAmount}G";
            stockText.text = $"在庫数:{buyAmount}/{stock}";
        }


        public void OnClickBuyButton()
        {
            BuyMaterial(buyAmount);
        }

        int CheckWarehouseAmount(MaterialData materialData)
        {
            int have;
            MaterialStack stack = Storage.Materials.Find(i => i.material == materialData);

            if(stack == null)
            {
                have = 0;
            }
            else
            {
                have = stack.amount;
            }

            return have;
        }

    }

}
