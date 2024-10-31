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

    private GameObject audioManager;

    void Start()
    {
        // AudioManagerオブジェクトを探す
        audioManager = GameObject.Find("AudioManager");
    }


    void Update()
    {
        // 判定エリア内で左クリックが押された場合、タイマーを開始
        if (canBreak && Input.GetMouseButton(0) && targetObject != null)
        {
            holdTimer += Time.deltaTime;

            if (holdTimer >= holdTime)
            {
                Destroy(targetObject);
                Debug.Log("物体を破壊しました");

                // 破壊完了時の効果音を再生
                if (destroyedSound != null && audioManager != null)
                {
                    // AudioSourceがアタッチされているか確認
                    AudioSource audioSource = audioManager.GetComponent<AudioSource>();
                    if (audioSource != null)
                    {
                        audioSource.PlayOneShot(destroyedSound);
                        Debug.Log("破壊音を再生しました");
                    }
                }

                // 破壊後にフラグとタイマーをリセット
                canBreak = false;
                targetObject = null;
                holdTimer = 0f;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            holdTimer = 0f;
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
