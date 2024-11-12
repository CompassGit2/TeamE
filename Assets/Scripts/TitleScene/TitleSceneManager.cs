using Data;
using Data.Database;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TitleScene
{
    public class TitleSceneManager : MonoBehaviour
    {
        void Start()
        {
            Application.targetFrameRate = 60;

            ItemDictionary.Initialize(materialDatabase, recipeDatabase);
        }
        
        [SerializeField] MaterialDatabase materialDatabase;
        [SerializeField] RecipeDatabase recipeDatabase;
        public void StartNewGame()
        {
            PlayerData.SetWorldRank(0);
            ItemDictionary.Initialize(materialDatabase, recipeDatabase);
            
            Storage.AddMaterial(materialDatabase.materialList[0],2);
            Storage.Gold = 100;

            SceneManager.LoadScene("MenuScene");
        }
    }

}
