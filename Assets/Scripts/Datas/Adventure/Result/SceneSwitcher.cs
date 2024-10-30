using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // メニューシーンに遷移するメソッド
    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuScene"); // メニューシーンの名前を正確に記述
    }
}
