using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void change_SmithScene()
    {
        SceneManager.LoadScene("SmithScene");
    }

    public void change_ShopScene()
    {
        SceneManager.LoadScene("ShopScene");
    }

    public void change_AdventureScene()
    {
        SceneManager.LoadScene("AdventureScene");
    }

}