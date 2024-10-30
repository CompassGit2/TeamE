using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectWithDrop : MonoBehaviour
{
    // �h���b�v�A�C�e���̃N���X
    [System.Serializable]
    public class DropItem
    {
        public GameObject itemPrefab;  // �A�C�e���̃v���n�u
        public float dropChance;       // �h���b�v����m��
    }

    // �X�|�[������I�u�W�F�N�g�Ƃ��̏d�݁i�m���j�A�����Ă��̃I�u�W�F�N�g�̃h���b�v���X�g
    [System.Serializable]
    public class SpawnObject
    {
        public GameObject prefab;          // �X�|�[������I�u�W�F�N�g
        public float weight;               // �X�|�[���̏d�݁i�m���j
        public List<DropItem> dropItems;   // �h���b�v�A�C�e���̃��X�g
    }

    // �z�u����I�u�W�F�N�g�̃��X�g
    public List<SpawnObject> objectsToSpawn;

    // �z�u�n�_�̃��X�g
    public Transform[] spawnPoints;

    void Start()
    {
        // �e�z�u�n�_�ɑ΂��ă����_���ɃI�u�W�F�N�g��z�u
        foreach (Transform spawnPoint in spawnPoints)
        {
            // �m���Ɋ�Â��ăI�u�W�F�N�g��I��
            SpawnObject selectedObject = GetRandomObjectByWeight();

            // �z�u�n�_�ɃI�u�W�F�N�g���C���X�^���X��
            GameObject spawnedObject = Instantiate(selectedObject.prefab, spawnPoint.position, spawnPoint.rotation);

            // �h���b�v�A�C�e���𔻒�
            DropRandomItem(selectedObject);
        }
    }

    // �m���Ɋ�Â��ă����_���ɃI�u�W�F�N�g��I������֐�
    SpawnObject GetRandomObjectByWeight()
    {
        // ���d�ʁi�m���̍��v�j���v�Z
        float totalWeight = 0f;
        foreach (SpawnObject obj in objectsToSpawn)
        {
            totalWeight += obj.weight;
        }

        // 0����totalWeight�̊ԂŃ����_���Ȓl�𐶐�
        float randomValue = UnityEngine.Random.Range(0, totalWeight);

        // �����_���l�ɑΉ�����I�u�W�F�N�g��I��
        float currentWeight = 0f;
        foreach (SpawnObject obj in objectsToSpawn)
        {
            currentWeight += obj.weight;
            if (randomValue <= currentWeight)
            {
                return obj;
            }
        }

        // �f�t�H���g�ōŏ��̃I�u�W�F�N�g��Ԃ��i���S�΍�j
        return objectsToSpawn[0];
    }

    // �����_���Ƀh���b�v�A�C�e�������肷��֐�
    void DropRandomItem(SpawnObject obj)
    {
        foreach (DropItem item in obj.dropItems)
        {
            // �����_���l�𐶐����āA�h���b�v�m���Ɣ�r
            float randomDrop = UnityEngine.Random.Range(0f, 1f);  // 0����1�̊Ԃ̃����_���Ȓl
            if (randomDrop <= item.dropChance)
            {
                // �A�C�e�����h���b�v
                Instantiate(item.itemPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
