using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<MaterialStack> collectedItems = new List<MaterialStack>(); // ���W�����A�C�e���̃��X�g

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[���J�ڂ��Ă��j������Ȃ��悤�ɂ���
        }
        else
        {
            Destroy(gameObject); // ���ɑ��݂���ꍇ�͔j��
        }
    }

    public void AddCollectedItem(MaterialData item)
    {
        if (item == null) return;

        var existingStack = collectedItems.Find(stack => stack.material.Id == item.Id);
        if (existingStack != null)
        {
            existingStack.amount++; // ���𑝂₷
        }
        else
        {
            collectedItems.Add(new MaterialStack(item, 1)); // �V�����A�C�e����ǉ�
        }
    }
}
