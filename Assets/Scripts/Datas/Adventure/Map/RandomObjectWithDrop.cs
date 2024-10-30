using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectWithDrop : MonoBehaviour
{
    // ドロップアイテムのクラス
    [System.Serializable]
    public class DropItem
    {
        public GameObject itemPrefab;  // アイテムのプレハブ
        public float dropChance;       // ドロップする確率
    }

    // スポーンするオブジェクトとその重み（確率）、そしてそのオブジェクトのドロップリスト
    [System.Serializable]
    public class SpawnObject
    {
        public GameObject prefab;          // スポーンするオブジェクト
        public float weight;               // スポーンの重み（確率）
        public List<DropItem> dropItems;   // ドロップアイテムのリスト
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
            SpawnObject selectedObject = GetRandomObjectByWeight();

            // 配置地点にオブジェクトをインスタンス化
            GameObject spawnedObject = Instantiate(selectedObject.prefab, spawnPoint.position, spawnPoint.rotation);

            // ドロップアイテムを判定
            DropRandomItem(selectedObject);
        }
    }

    // 確率に基づいてランダムにオブジェクトを選択する関数
    SpawnObject GetRandomObjectByWeight()
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
                return obj;
            }
        }

        // デフォルトで最初のオブジェクトを返す（安全対策）
        return objectsToSpawn[0];
    }

    // ランダムにドロップアイテムを決定する関数
    void DropRandomItem(SpawnObject obj)
    {
        foreach (DropItem item in obj.dropItems)
        {
            // ランダム値を生成して、ドロップ確率と比較
            float randomDrop = UnityEngine.Random.Range(0f, 1f);  // 0から1の間のランダムな値
            if (randomDrop <= item.dropChance)
            {
                // アイテムをドロップ
                Instantiate(item.itemPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
