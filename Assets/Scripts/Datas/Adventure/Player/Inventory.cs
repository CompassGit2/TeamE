using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Data;
using Data.Database;

public class Inventory : MonoBehaviour
{
    public MaterialDatabase materialDatabase; // MaterialDatabase�̎Q��
    public GameObject itemSlotPrefab;
    public Transform inventoryPanel;
    public Text descriptionText; // ��������\������Text

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

    // �A�C�e�������W���郁�\�b�h
    public void CollectItem(MaterialData item)
    {
        if (item != null)
        {
            AddItemById(item.Id); // �A�C�e�����C���x���g���ɒǉ�
            Debug.Log($"�A�C�e�������W���܂���: {item.Name}");
        }
        else
        {
            Debug.LogError("���W����A�C�e����null�ł��B");
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
            textComponent.text = $"{stack.material.Name} (�~{stack.amount})"; // ����\��
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
            descriptionText.text = $"{item.Name}\n���A�x: {item.Rarity}\n���i: {item.Price}\n����: {item.Description}";
            Debug.Log($"��������\��: {item.Name} - {item.Description}");
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