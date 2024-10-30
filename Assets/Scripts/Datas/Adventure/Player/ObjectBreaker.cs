using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBreaker : MonoBehaviour
{
    public AudioClip breakingSound; // 破壊中の効果音
    public AudioClip destroyedSound; // 破壊完了時の効果音
    private AudioSource audioSource;
    private GameObject targetObject; // 破壊対象のオブジェクト
    private bool canBreak = false; // 破壊可能かどうかのフラグ
    public float holdTime = 3f; // 長押しに必要な時間
    private float holdTimer = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 判定エリア内で左クリックが押された場合、タイマーを開始
        if (canBreak && Input.GetMouseButton(0) && targetObject != null)
        {
            holdTimer += Time.deltaTime;

            // 破壊中の効果音を再生
            if (!audioSource.isPlaying && breakingSound != null)
            {
                audioSource.clip = breakingSound;
                audioSource.Play();
            }

            if (holdTimer >= holdTime)
            {
                Destroy(targetObject);
                Debug.Log("物体を破壊しました");

                // 破壊完了時の効果音を再生
                audioSource.Stop(); // 破壊中の音を停止
                if (destroyedSound != null)
                {
                    audioSource.PlayOneShot(destroyedSound);
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
            audioSource.Stop();
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
            holdTimer = 0f; // エリア外に出たらタイマーもリセット
            audioSource.Stop();
        }
    }
}
