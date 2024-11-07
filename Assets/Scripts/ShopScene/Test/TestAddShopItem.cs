using System.Collections;
using System.Collections.Generic;
using Data;
using Data.Database;
using UnityEngine;

public class TestAddShopItem : MonoBehaviour
{
    [SerializeField] MaterialDatabase materialDatabase;

    public void AddItem()
    {
        foreach(MaterialData m in materialDatabase.materialList)
        {
            Storage.AddShopMaterial(m,10);
            Debug.Log("ショップにアイテムを追加しました。");
        }
    }
}
