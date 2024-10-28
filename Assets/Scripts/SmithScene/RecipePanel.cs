using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Data;
using Data.Database;

namespace SmithScene.SelectMaterial
{
    public class RecipePanel : MonoBehaviour
    {
        [SerializeField] RecipeDatabase recipeDatabase;
        [SerializeField] GameObject foundRecipePanel;
        [SerializeField] Image weaponImage;
        [SerializeField] TextMeshProUGUI weaponName;
        [SerializeField] TextMeshProUGUI rarityTMP;
        [SerializeField] TextMeshProUGUI lengthTMP;
        [SerializeField] TextMeshProUGUI weightTMP;
        [SerializeField] TextMeshProUGUI sharpnessTMP;
        [SerializeField] GameStartButton gameStartButton;
        public RecipeData selectedRecipe;
        public bool RecipeExist = false;

        public void SearchRecipe(List<MaterialStack> useMaterials)
        {
            List<RecipeData> recipe = recipeDatabase.SearchRecipeByMaterials(useMaterials);
            if(recipe.Count > 0)
            {
                weaponImage.sprite = recipe[0].Weapon.WeaponImage;
                weaponName.text = recipe[0].Weapon.Name;
                rarityTMP.text = recipe[0].Weapon.Rarity.ToString();
                lengthTMP.text = recipe[0].Weapon.Length.ToString();
                weightTMP.text = recipe[0].Weapon.Weight.ToString();
                sharpnessTMP.text = recipe[0].Weapon.Sharpness.ToString();
                RecipeExist = true;
                selectedRecipe = recipe[0];
                foundRecipePanel.SetActive(true);
                gameStartButton.Enable(recipe[0],useMaterials);
            }
            else
            {
                RecipeExist = false;
                foundRecipePanel.SetActive(false);
                gameStartButton.Disable();
            }
        }
    }

}
