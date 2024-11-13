using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] ShopScene.ShopItemTable shopItemTable;
    // ���j���[�V�[���ɑJ�ڂ��郁�\�b�h
    public void GoToMenu()
    {
        shopItemTable.AddShopItem();
        SceneManager.LoadScene("MenuScene"); // ���j���[�V�[���̖��O�𐳊m�ɋL�q
    }
}
