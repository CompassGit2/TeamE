using UnityEngine;
using UnityEngine.SceneManagement;

namespace SmithScene.SelectMaterial
{
    public class MenuBackButton : MonoBehaviour
    {
        public void OnButtonClicked()
        {
            SceneManager.LoadScene("MenuScene");
        }
    }

}

