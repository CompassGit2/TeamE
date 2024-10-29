using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oreMining : MonoBehaviour
{
    // �h���b�v����A�C�e���̃��X�g
    public GameObject[] dropItems;
    // �e�A�C�e���̃h���b�v�m�� (0-100�͈̔́A��: 20.0f �Ȃ�20%�̊m��)
    public float[] dropChances;

    // �@����ۂɌĂяo����郁�\�b�h
    public void MineOre()
    {
        // �e�A�C�e�����ƂɃh���b�v����
        for (int i = 0; i < dropItems.Length; i++)
        {
            if (DropItem(dropChances[i]))
            {
                // �A�C�e�����h���b�v
                Instantiate(dropItems[i], transform.position, Quaternion.identity);
            }
        }

        // �z�΂������邩�A�j�󂳂��ꍇ�Ȃ�
        Destroy(gameObject);
    }

    // �A�C�e���̃h���b�v�𔻒肷��֐�
    private bool DropItem(float chance)
    {
        // 0����100�͈̔͂Ń����_���Ȑ��l�𐶐����Achance��菬�����ꍇ�̓h���b�v
        float randomValue = UnityEngine.Random.Range(0f, 100f);
        return randomValue <= chance;
    }
}
