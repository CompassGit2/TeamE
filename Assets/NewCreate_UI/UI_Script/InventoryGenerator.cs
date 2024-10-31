using System.Collections.Generic;
using UnityEngine;

public class InventoryGenerator : MonoBehaviour
{
    public GameObject itemSlotPrefab;  // アイテム欄のPrefab
    public Transform contentPanel;     // ボックスを追加する親オブジェクト（UIのコンテンツ）
    public int numberOfSlots = 10;     // 生成したいボックスの個数

    private List<GameObject> itemSlots = new List<GameObject>();  // 生成されたアイテム欄のリスト

    void Start()
    {
        GenerateInventorySlots();
    }

    // 指定した個数のアイテム欄を生成するメソッド
    void GenerateInventorySlots()
    {
        for (int i = 0; i < numberOfSlots; i++)
        {
            // Prefabから新しいアイテム欄をインスタンス化して親オブジェクトに配置
            GameObject newSlot = Instantiate(itemSlotPrefab, contentPanel);

            // アイテム欄をリストに追加して管理（後でアクセスするため）
            itemSlots.Add(newSlot);
        }
    }
}
