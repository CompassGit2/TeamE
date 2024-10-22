using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ItemDatabase
{
    public int MaterialID;  // アイテムのID
    public Sprite MaterialIcon;  // アイテムの画像
}
public class InventoryManager : MonoBehaviour
{
    public List<ItemDatabase> itemDatabase;  
    public List<InventoryItem> inventoryItems = new List<InventoryItem>(); 
    public GameObject inventoryUI; 
    public Transform itemSlotParent;  
    public GameObject itemSlotPrefab;

    public void AddItem(int materialID, int count)
    {
        // 既にインベントリにあるか確認
        InventoryItem existingItem = inventoryItems.Find(item => item.MaterialID == materialID);

        if (existingItem != null)
        {
            // 既存のアイテムならカウントを増やす
            existingItem.itemCount += count;
        }
        else
        {
            // 新しいアイテムを追加
            Sprite itemIcon = GetMaterialIcon(materialID);  // アイテムIDから対応する画像を取得
            InventoryItem newItem = new InventoryItem();
            newItem.MaterialID = materialID;
            newItem.itemIcon = itemIcon;
            newItem.itemCount = count;
            inventoryItems.Add(newItem);
        }

        UpdateInventoryUI();
    }

    private Sprite GetMaterialIcon(int itemID)
    {
        ItemDatabase item = itemDatabase.Find(i => i.MaterialID == itemID);
        return item != null ? item.MaterialIcon : null;
    }

    private void UpdateInventoryUI()
    {
        foreach (Transform child in itemSlotParent)
        {
            Destroy(child.gameObject);
        }

        foreach (InventoryItem item in inventoryItems)
        {
            GameObject slot = Instantiate(itemSlotPrefab, itemSlotParent);
            slot.transform.Find("ItemImage").GetComponent<UnityEngine.UI.Image>().sprite = item.itemIcon;
            slot.transform.Find("ItemCountText").GetComponent<UnityEngine.UI.Text>().text = item.itemCount.ToString();
        }
    }
}
