using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<MaterialStack> collectedItems = new List<MaterialStack>(); // 収集したアイテムのリスト

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーン遷移しても破棄されないようにする
        }
        else
        {
            Destroy(gameObject); // 既に存在する場合は破棄
        }
    }

    public void AddCollectedItem(MaterialData item)
    {
        if (item == null) return;

        var existingStack = collectedItems.Find(stack => stack.material.Id == item.Id);
        if (existingStack != null)
        {
            existingStack.amount++; // 個数を増やす
        }
        else
        {
            collectedItems.Add(new MaterialStack(item, 1)); // 新しいアイテムを追加
        }
    }
}
