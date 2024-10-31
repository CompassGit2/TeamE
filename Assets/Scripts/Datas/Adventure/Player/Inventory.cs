using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Data;
using Data.Database;

public class Inventory : MonoBehaviour
{
    public MaterialDatabase materialDatabase; // MaterialDatabaseの参照
    public GameObject itemSlotPrefab;
    public Transform inventoryPanel;
    public Text descriptionText; // 説明文を表示するText

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

    // アイテムを収集するメソッド
    public void CollectItem(MaterialData item)
    {
        if (item != null)
        {
            AddItemById(item.Id); // アイテムをインベントリに追加
            Debug.Log($"アイテムを収集しました: {item.Name}");
        }
        else
        {
            Debug.LogError("収集するアイテムがnullです。");
        }
    }

    void CreateSlot(MaterialStack stack)
    {
        if (stack == null)
        {
            Debug.LogError("MaterialStack is null!");
            return;
        }

        var slot = Instantiate(itemSlotPrefab, inventoryPanel);
        if (slot == null)
        {
            Debug.LogError("Failed to instantiate item slot prefab!");
            return;
        }

        var imageComponent = slot.GetComponent<Image>();
        var textComponent = slot.GetComponentInChildren<Text>();

        if (imageComponent != null)
        {
            imageComponent.sprite = stack.material.MaterialImage;
        }
        else
        {
            Debug.LogError("Image component not found on slot!");
        }

        if (textComponent != null)
        {
            textComponent.text = $"{stack.material.Name} (×{stack.amount})"; // 個数を表示
        }
        else
        {
            Debug.LogError("Text component not found in slot!");
        }
    }

    public void ShowItemDescription(MaterialData item)
    {
        if (item == null)
        {
            Debug.LogError("MaterialData item is null!");
            return;
        }

        if (descriptionText != null)
        {
            descriptionText.text = $"{item.Name}\nレア度: {item.Rarity}\n価格: {item.Price}\n説明: {item.Description}";
            Debug.Log($"説明文を表示: {item.Name} - {item.Description}");
        }
        else
        {
            Debug.LogError("DescriptionText is not assigned!");
        }
    }

    void UpdateInventoryUI()
    {
        if (inventoryPanel == null)
        {
            Debug.LogError("Inventory panel is not assigned!");
            return;
        }

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