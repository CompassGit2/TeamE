using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TitleScene
{
    public class TitleSceneManager : MonoBehaviour
    {
        public void MoveMenuScene()
        {
            SceneManager.LoadScene("MenuScene");
        }
    }

}
