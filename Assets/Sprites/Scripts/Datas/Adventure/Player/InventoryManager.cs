using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ItemDatabase
{
    public int MaterialID;  // �A�C�e����ID
    public Sprite MaterialIcon;  // �A�C�e���̉摜
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
        // ���ɃC���x���g���ɂ��邩�m�F
        InventoryItem existingItem = inventoryItems.Find(item => item.MaterialID == materialID);

        if (existingItem != null)
        {
            // �����̃A�C�e���Ȃ�J�E���g�𑝂₷
            existingItem.itemCount += count;
        }
        else
        {
            // �V�����A�C�e����ǉ�
            Sprite itemIcon = GetMaterialIcon(materialID);  // �A�C�e��ID����Ή�����摜���擾
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
