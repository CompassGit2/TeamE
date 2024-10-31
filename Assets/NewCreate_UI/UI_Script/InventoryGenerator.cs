using System.Collections.Generic;
using UnityEngine;

public class InventoryGenerator : MonoBehaviour
{
    public GameObject itemSlotPrefab;  // �A�C�e������Prefab
    public Transform contentPanel;     // �{�b�N�X��ǉ�����e�I�u�W�F�N�g�iUI�̃R���e���c�j
    public int numberOfSlots = 10;     // �����������{�b�N�X�̌�

    private List<GameObject> itemSlots = new List<GameObject>();  // �������ꂽ�A�C�e�����̃��X�g

    void Start()
    {
        GenerateInventorySlots();
    }

    // �w�肵�����̃A�C�e�����𐶐����郁�\�b�h
    void GenerateInventorySlots()
    {
        for (int i = 0; i < numberOfSlots; i++)
        {
            // Prefab����V�����A�C�e�������C���X�^���X�����Đe�I�u�W�F�N�g�ɔz�u
            GameObject newSlot = Instantiate(itemSlotPrefab, contentPanel);

            // �A�C�e���������X�g�ɒǉ����ĊǗ��i��ŃA�N�Z�X���邽�߁j
            itemSlots.Add(newSlot);
        }
    }
}
