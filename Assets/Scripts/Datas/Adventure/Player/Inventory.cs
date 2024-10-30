using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Data;
using Data.Database;
using TMPro;

public class Inventory : MonoBehaviour
{
    public MaterialDatabase materialDatabase; // MaterialDatabaseの参照
    public GameObject itemSlotPrefab;
    public Transform inventoryPanel;
    public TextMeshProUGUI descriptionText;

    private List<MaterialStack> inventoryItems = new List<MaterialStack>(); // MaterialStackのリスト

    public void AddItemById(int itemId)
    {
        MaterialData newItem = materialDatabase.materialList.Find(item => item.Id == itemId);

        if (newItem != null)
        {
            var existingStack = inventoryItems.Find(stack => stack.material.Id == newItem.Id);
            if (existingStack != null)
            {
                // 個数を増やす
                existingStack.amount++;
            }
            else
            {
                // 新しいアイテムを追加
                MaterialStack newStack = new MaterialStack(newItem, 1);
                inventoryItems.Add(newStack);
                CreateSlot(newStack);
            }
            UpdateInventoryUI();
        }
        else
        {
            Debug.LogWarning($"Item with ID '{itemId}' not found in MaterialDatabase.");
        }
    }

    void CreateSlot(MaterialStack stack)
    {
        var slot = Instantiate(itemSlotPrefab, inventoryPanel);
        slot.GetComponent<Image>().sprite = stack.material.MaterialImage;
        slot.GetComponentInChildren<Text>().text = $"{stack.material.Name} (×{stack.amount})"; // 個数を表示
        slot.GetComponent<Button>().onClick.AddListener(() => ShowItemDescription(stack.material));
    }

    void ShowItemDescription(MaterialData item)
    {
        descriptionText.text = $"{item.Name}\nレア度: {item.Rarity}\n価格: {item.Price}\n説明: {item.Description}";
        Debug.Log($"説明文を表示: {item.Name} - {item.Description}");
    }

    void UpdateInventoryUI()
    {
        // 必要に応じてスロットUIをリフレッシュ
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject); // 現在のスロットを削除
        }

        // 再生成
        foreach (var stack in inventoryItems)
        {
            CreateSlot(stack);
        }
    }

    // インベントリのアイテムを取得
    public List<MaterialStack> GetInventoryItems()
    {
        return inventoryItems;
    }

    // インベントリをクリア
    public void ClearInventory()
    {
        inventoryItems.Clear(); // アイテムリストをクリア
        UpdateInventoryUI(); // UIを更新
    }
}
