using UnityEngine;
using Data;

namespace MenuScene
{
    public class RecipeDictionaryPanel : MonoBehaviour
    {
        [SerializeField] RecipeDictionaryGridView gridView;
        [SerializeField] RecipeDetailPanel recipeDetailPanel;
        

        void OnEnable()
        {
            recipeDetailPanel.Disable();
            gridView.OnCellClicked(index => SetRecipeToRightPage(ItemDictionary.RecipeDictionary[index]));
            GenerateCells();
        }

        void OnDisable() 
        {
            recipeDetailPanel.Disable();    
        }

        void GenerateCells()
        {
            gridView.UpdateContents(ItemDictionary.RecipeDictionary);
        }

        void SetRecipeToRightPage(RecipeDictionaryData recipeDictionaryData)
        {
            recipeDetailPanel.Enable(recipeDictionaryData);
        } 
    }

}
