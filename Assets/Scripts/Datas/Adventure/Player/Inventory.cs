using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Data;
using Data.Database;
using TMPro;

public class Inventory : MonoBehaviour
{
    public MaterialDatabase materialDatabase; // MaterialDatabase�̎Q��
    public GameObject itemSlotPrefab;
    public Transform inventoryPanel;
    public TextMeshProUGUI descriptionText;

    private List<MaterialStack> inventoryItems = new List<MaterialStack>(); // MaterialStack�̃��X�g

    public void AddItemById(int itemId)
    {
        MaterialData newItem = materialDatabase.materialList.Find(item => item.Id == itemId);

        if (newItem != null)
        {
            var existingStack = inventoryItems.Find(stack => stack.material.Id == newItem.Id);
            if (existingStack != null)
            {
                // ���𑝂₷
                existingStack.amount++;
            }
            else
            {
                // �V�����A�C�e����ǉ�
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
        slot.GetComponentInChildren<Text>().text = $"{stack.material.Name} (�~{stack.amount})"; // ����\��
        slot.GetComponent<Button>().onClick.AddListener(() => ShowItemDescription(stack.material));
    }

    void ShowItemDescription(MaterialData item)
    {
        descriptionText.text = $"{item.Name}\n���A�x: {item.Rarity}\n���i: {item.Price}\n����: {item.Description}";
        Debug.Log($"��������\��: {item.Name} - {item.Description}");
    }

    void UpdateInventoryUI()
    {
        // �K�v�ɉ����ăX���b�gUI�����t���b�V��
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject); // ���݂̃X���b�g���폜
        }

        // �Đ���
        foreach (var stack in inventoryItems)
        {
            CreateSlot(stack);
        }
    }

    // �C���x���g���̃A�C�e�����擾
    public List<MaterialStack> GetInventoryItems()
    {
        return inventoryItems;
    }

    // �C���x���g�����N���A
    public void ClearInventory()
    {
        inventoryItems.Clear(); // �A�C�e�����X�g���N���A
        UpdateInventoryUI(); // UI���X�V
    }
}
