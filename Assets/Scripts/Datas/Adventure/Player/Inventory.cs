using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Data;
using Data.Database;
using TMPro;

public class Inventory : MonoBehaviour
{
    public MaterialDatabase materialDatabase; // MaterialDatabaseの参照
    public GameObject itemSlotPrefab;
    public Transform inventoryPanel;
    public TextMeshProUGUI descriptionText;

    private List<MaterialData> inventoryItems = new List<MaterialData>();

    public void AddItemById(int itemId)
    {
        MaterialData newItem = materialDatabase.materialList.Find(item => item.Id == itemId);

        if (newItem != null)
        {
            var existingItem = inventoryItems.Find(item => item.Id == newItem.Id);
            if (existingItem != null)
            {
                // 必要であれば個数などの属性を管理
            }
            else
            {
                MaterialData itemCopy = ScriptableObject.CreateInstance<MaterialData>();
                itemCopy.Id = newItem.Id;
                itemCopy.Name = newItem.Name;
                itemCopy.MaterialImage = newItem.MaterialImage;
                itemCopy.Rarity = newItem.Rarity;
                itemCopy.Price = newItem.Price;
                itemCopy.Description = newItem.Description;

                inventoryItems.Add(itemCopy);
                CreateSlot(itemCopy);
            }
            UpdateInventoryUI();
        }
        else
        {
            Debug.LogWarning($"Item with ID '{itemId}' not found in MaterialDatabase.");
        }
    }

    void CreateSlot(MaterialData item)
    {
        var slot = Instantiate(itemSlotPrefab, inventoryPanel);
        slot.GetComponent<Image>().sprite = item.MaterialImage;
        slot.GetComponentInChildren<Text>().text = $"{item.Name} (×1)";
        slot.GetComponent<Button>().onClick.AddListener(() => ShowItemDescription(item));
    }

    void ShowItemDescription(MaterialData item)
    {
        descriptionText.text = $"{item.Name}\nレア度: {item.Rarity}\n価格: {item.Price}\n説明: {item.Description}";
    }

    void UpdateInventoryUI()
    {
        // 必要に応じてスロットUIをリフレッシュ
    }
}
