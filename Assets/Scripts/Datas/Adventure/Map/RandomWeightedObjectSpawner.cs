using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWeightedObjectSpawner : MonoBehaviour
{
    // �o���I�u�W�F�N�g�Ƃ��̏d�݁i�m���j
    [System.Serializable]
    public class SpawnObject
    {
        public GameObject prefab;  // �I�u�W�F�N�g�̃v���n�u
        public float weight;       // �m���̏d��
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
            GameObject selectedObject = GetRandomObjectByWeight();

            // �z�u�n�_�ɃI�u�W�F�N�g���C���X�^���X��
            Instantiate(selectedObject, spawnPoint.position, spawnPoint.rotation);
        }
    }

    // �m���Ɋ�Â��ă����_���ɃI�u�W�F�N�g��I������֐�
    GameObject GetRandomObjectByWeight()
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
                return obj.prefab;
            }
        }

        // �f�t�H���g�ōŏ��̃I�u�W�F�N�g��Ԃ��i���S�΍�j
        return objectsToSpawn[0].prefab;
    }
}
