using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public int itemId; // このオブジェクトが持つアイテムのID
    public Inventory inventory; // インベントリスクリプトへの参照

    void OnDestroy()
    {
        if (inventory != null)
        {
            inventory.AddItemById(itemId); // オブジェクトが破壊されたときにアイテムを追加
        }
    }
}
