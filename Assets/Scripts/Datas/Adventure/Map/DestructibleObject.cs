using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public GameObject destructionEffect;
    public void DestroyObject()
    {
        // 破壊エフェクトを生成（必要な場合）
        if (destructionEffect != null)
        {
            Instantiate(destructionEffect, transform.position, transform.rotation);
        }

        // オブジェクトを破壊
        Destroy(gameObject);
    }
}
