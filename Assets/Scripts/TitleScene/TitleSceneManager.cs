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
        [SerializeField] OrderDatabase orderDatabase;
        public void StartNewGame()
        {
            PlayerData.SetWorldRank(0);
            ItemDictionary.Initialize(materialDatabase, recipeDatabase);
            
            foreach(OrderData orderData in orderDatabase.GetNormalOrdersByRank(PlayerData.WorldRank))
            {
                Storage.AddOrderData(orderData);
            }

            Storage.AddMaterial(materialDatabase.materialList[0],2);
            Storage.Gold = 150;

            SceneManager.LoadScene("MenuScene");
        }
    }

}
