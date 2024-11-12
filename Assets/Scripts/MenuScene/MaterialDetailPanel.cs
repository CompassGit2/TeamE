using System.Collections;
using System.Collections.Generic;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MenuScene
{
    public class MaterialDetailPanel : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI idText;
        [SerializeField] TextMeshProUGUI rarityText;
        [SerializeField] Image materialImage;
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI descriptionText;

        public void Enable(MaterialDictionaryData dictionaryData)
        {
            gameObject.SetActive(true);
            idText.text = $"ID:{dictionaryData.materialData.Id}";
            rarityText.text = $"Rare{dictionaryData.materialData.Rarity}";
            materialImage.sprite = dictionaryData.materialData.MaterialImage;

            if(dictionaryData.registration)
            {
                materialImage.color = new Color32(255,255,255,255);
                nameText.text = dictionaryData.materialData.Name;
                descriptionText.text = dictionaryData.materialData.Description;
            }
            else
            {
                materialImage.color = new Color32(0,0,0,255);
                nameText.text = "？？？";
                descriptionText.text = "？？？";
            }
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }

}
