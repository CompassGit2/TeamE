using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oreMining : MonoBehaviour
{
    // ドロップするアイテムのリスト
    public GameObject[] dropItems;
    // 各アイテムのドロップ確率 (0-100の範囲、例: 20.0f なら20%の確率)
    public float[] dropChances;

    // 掘られる際に呼び出されるメソッド
    public void MineOre()
    {
        // 各アイテムごとにドロップ判定
        for (int i = 0; i < dropItems.Length; i++)
        {
            if (DropItem(dropChances[i]))
            {
                // アイテムをドロップ
                Instantiate(dropItems[i], transform.position, Quaternion.identity);
            }
        }

        // 鉱石が消えるか、破壊される場合など
        Destroy(gameObject);
    }

    // アイテムのドロップを判定する関数
    private bool DropItem(float chance)
    {
        // 0から100の範囲でランダムな数値を生成し、chanceより小さい場合はドロップ
        float randomValue = UnityEngine.Random.Range(0f, 100f);
        return randomValue <= chance;
    }
}
