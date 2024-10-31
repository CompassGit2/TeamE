using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBreaker : MonoBehaviour
{
    public AudioClip destroyedSound; // 破壊完了時の効果音
    private GameObject targetObject; // 破壊対象のオブジェクト
    private bool canBreak = false; // 破壊可能かどうかのフラグ
    public float holdTime = 3f; // 長押しに必要な時間
    private float holdTimer = 0f;


    void Update()
    {
        // 判定エリア内で左クリックが押された場合、タイマーを開始
        if (canBreak && Input.GetMouseButton(0) && targetObject != null)
        {
            holdTimer += Time.deltaTime;

            if (holdTimer >= holdTime)
            {
                // 破壊音を再生
                AudioSource targetAudioSource = targetObject.GetComponent<AudioSource>();
                if (targetAudioSource != null && destroyedSound != null)
                {
                    targetAudioSource.PlayOneShot(destroyedSound); // 破壊音を再生
                    Debug.Log("破壊音を再生しました"); // デバッグログ追加
                }
                else
                {
                    Debug.LogWarning("破壊音の再生に失敗しました"); // エラーログ追加
                }


                Destroy(targetObject); // オブジェクトを破壊
                Debug.Log("物体を破壊しました");

                // 破壊後にフラグとタイマーをリセット
                canBreak = false;
                targetObject = null;
                holdTimer = 0f;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            holdTimer = 0f; // タイマーリセット
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Destructible"))
        {
            targetObject = collision.gameObject;
            canBreak = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == targetObject)
        {
            canBreak = false;
            targetObject = null;
            holdTimer = 0f;
        }
    }
}
