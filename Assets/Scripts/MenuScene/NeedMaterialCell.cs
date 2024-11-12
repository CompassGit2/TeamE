using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Data;

namespace MenuScene
{
    public class NeedMaterialCell : MonoBehaviour
    {
        [SerializeField] Image needMaterialImage;
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI amountText;

        public void Enable(MaterialData materialData, int amount)
        {
            gameObject.SetActive(true);
            amountText.text = $"×{amount}";
            needMaterialImage.sprite = materialData.MaterialImage;

            if(ItemDictionary.IsMaterialRegistered(materialData))
            {
                nameText.text = materialData.Name;
                needMaterialImage.color = new Color32(255,255,255,255);
            }
            else
            {
                nameText.text = "？？？";
                needMaterialImage.color = new Color32(0,0,0,255);
            }
            
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }

}
