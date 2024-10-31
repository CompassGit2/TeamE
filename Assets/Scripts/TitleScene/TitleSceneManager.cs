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
        }
        
        [SerializeField] MaterialDatabase materialDatabase;
        public void MoveMenuScene()
        {
            Storage.AddMaterial(materialDatabase.materialList[0],2);
            SceneManager.LoadScene("MenuScene");
        }
    }

}
