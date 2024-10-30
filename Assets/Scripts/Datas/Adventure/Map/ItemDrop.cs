using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public int itemId; // ���̃I�u�W�F�N�g�����A�C�e����ID
    public Inventory inventory; // �C���x���g���X�N���v�g�ւ̎Q��

    void Start()
    {
        // �V�[������Inventory�C���X�^���X���擾
        inventory = FindObjectOfType<Inventory>();
        if (inventory == null)
        {
            Debug.LogError("Inventory not found in the scene!");
        }
    }
    void OnDestroy()
    {
        if (inventory != null)
        {
            inventory.AddItemById(itemId); // �I�u�W�F�N�g���j�󂳂ꂽ�Ƃ��ɃA�C�e����ǉ�
        }
    }
}
