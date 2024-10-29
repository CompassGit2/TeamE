using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBreaker : MonoBehaviour
{
    private GameObject targetObject; // 破壊対象のオブジェクト
    private bool canBreak = false; // 破壊可能かどうかのフラグ
    void Update()
    {
        // 判定エリア内で左クリックが押された場合にのみ物体を破壊
        if (canBreak && Input.GetMouseButtonDown(0) && targetObject != null) // 0 は左クリックを表す
        {
            Destroy(targetObject);
            Debug.Log("物体を破壊しました");

            // 破壊後にフラグをリセット
            canBreak = false;
            targetObject = null;
        }
    }

    // 判定エリアに入った時にフラグを設定
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Destructible")) 
        {
            targetObject = collision.gameObject;
            canBreak = true;
        }
    }

    // 判定エリアから出た時にフラグをリセット
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == targetObject)
        {
            canBreak = false;
            targetObject = null;
        }
    }
}
