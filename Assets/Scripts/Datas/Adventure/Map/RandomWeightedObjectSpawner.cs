using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWeightedObjectSpawner : MonoBehaviour
{
    // 出現オブジェクトとその重み（確率）
    [System.Serializable]
    public class SpawnObject
    {
        public GameObject prefab;  // オブジェクトのプレハブ
        public float weight;       // 確率の重み
    }

    // 配置するオブジェクトのリスト
    public List<SpawnObject> objectsToSpawn;

    // 配置地点のリスト
    public Transform[] spawnPoints;

    void Start()
    {
        // 各配置地点に対してランダムにオブジェクトを配置
        foreach (Transform spawnPoint in spawnPoints)
        {
            // 確率に基づいてオブジェクトを選択
            GameObject selectedObject = GetRandomObjectByWeight();

            // 配置地点にオブジェクトをインスタンス化
            Instantiate(selectedObject, spawnPoint.position, spawnPoint.rotation);
        }
    }

    // 確率に基づいてランダムにオブジェクトを選択する関数
    GameObject GetRandomObjectByWeight()
    {
        // 総重量（確率の合計）を計算
        float totalWeight = 0f;
        foreach (SpawnObject obj in objectsToSpawn)
        {
            totalWeight += obj.weight;
        }

        // 0からtotalWeightの間でランダムな値を生成
        float randomValue = UnityEngine.Random.Range(0, totalWeight);

        // ランダム値に対応するオブジェクトを選択
        float currentWeight = 0f;
        foreach (SpawnObject obj in objectsToSpawn)
        {
            currentWeight += obj.weight;
            if (randomValue <= currentWeight)
            {
                return obj.prefab;
            }
        }

        // デフォルトで最初のオブジェクトを返す（安全対策）
        return objectsToSpawn[0].prefab;
    }
}
