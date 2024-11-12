using System.Collections;
using System.Collections.Generic;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MenuScene
{
    public class RecipeDetailPanel : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI idText;
        [SerializeField] TextMeshProUGUI rarityText;
        [SerializeField] Image weaponImage;
        [SerializeField] TextMeshProUGUI weaponNameText;
        [SerializeField] TextMeshProUGUI descriptionText;
        [SerializeField] TextMeshProUGUI lengthText;
        [SerializeField] TextMeshProUGUI weightText;
        [SerializeField] TextMeshProUGUI sharpnessText;
        [SerializeField] List<NeedMaterialCell> needMaterialCells;

        public void Enable(RecipeDictionaryData dictionaryData)
        {
            gameObject.SetActive(true);
            idText.text = $"ID:{dictionaryData.recipeData.Id}";
            rarityText.text = $"Rare{dictionaryData.recipeData.Weapon.Rarity}";
            weaponImage.sprite = dictionaryData.recipeData.Weapon.WeaponImage;
            lengthText.text = $"長さ:{dictionaryData.recipeData.Weapon.Length}";
            weightText.text = $"重さ:{dictionaryData.recipeData.Weapon.Weight}";
            sharpnessText.text = $"鋭さ:{dictionaryData.recipeData.Weapon.Sharpness}";

            foreach(NeedMaterialCell needMaterialCell in needMaterialCells)
            {
                needMaterialCell.Disable();
            }
            if(dictionaryData.recipeData.Material1)
            {
                needMaterialCells[0].Enable(dictionaryData.recipeData.Material1, dictionaryData.recipeData.AmountMaterial1);
            }
            if(dictionaryData.recipeData.Material2)
            {
                needMaterialCells[1].Enable(dictionaryData.recipeData.Material2, dictionaryData.recipeData.AmountMaterial2);
            }
            if(dictionaryData.recipeData.Material3)
            {
                needMaterialCells[2].Enable(dictionaryData.recipeData.Material3, dictionaryData.recipeData.AmountMaterial3);
            }

            if(dictionaryData.registration)
            {
                weaponNameText.text = dictionaryData.recipeData.Weapon.Name;
                weaponImage.color = new Color32(255,255,255,255);
                descriptionText.text = dictionaryData.recipeData.Weapon.Description;
            }
            else
            {
                weaponNameText.text = "？？？";
                weaponImage.color = new Color32(0,0,0,255);
                descriptionText.text = "？？？";
            }
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }

}
