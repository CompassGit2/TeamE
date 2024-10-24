using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public float moveSpeed = 5f;  // プレイヤーの移動速度
    public GameObject inventoryUI; 
    private bool isInventoryOpen = false;  
    public string targetTag = "Destructible"; // 破壊対象のタグ
    private bool isDestroying = false; 
    private float destroyDelay = 0.5f; 
    private Coroutine destroyCoroutine;
    public float destroyRange = 2f;
    public float destroyAngle = 45f;
    void Update()
    {
        // 横移動の入力を取得
        float moveX = Input.GetAxis("Horizontal");
        // 縦移動の入力を取得（横移動がない場合のみ）
        float moveY = moveX == 0 ? Input.GetAxis("Vertical") : 0;

        // 移動量を計算
        Vector3 move = new Vector3(moveX, moveY, 0) * moveSpeed * Time.deltaTime;

        // プレイヤーを移動
        transform.position += move;

        // Eキーが押されたらインベントリを開閉
        if (Input.GetKeyDown(KeyCode.E))
        {
            isInventoryOpen = !isInventoryOpen;  // 開閉をトグル
            inventoryUI.SetActive(isInventoryOpen);  // インベントリUIの表示を切り替え
        }

        if (Input.GetMouseButton(0))
        {
            if (!isDestroying)
            {
                isDestroying = true;
                destroyCoroutine = StartCoroutine(DestroyTarget());
            }
        }
        else if (isDestroying)
        {
            isDestroying = false;
            if (destroyCoroutine != null)
            {
                StopCoroutine(destroyCoroutine);
            }
        }
    }
    private IEnumerator DestroyTarget()
    {
        while (isDestroying)
        {
            // プレイヤーの前方の方向を取得
            Vector3 forwardDirection = transform.up; // プレイヤーの向いている方向
            Vector3 position = transform.position; // プレイヤーの位置

            // 範囲内にあるオブジェクトを取得
            Collider[] colliders = Physics.OverlapSphere(position, destroyRange);

            foreach (Collider collider in colliders)
            {
                // 破壊対象のタグを持っているかチェック
                if (collider.CompareTag(targetTag))
                {
                    // プレイヤーの向いている方向との角度を計算
                    Vector3 directionToObject = (collider.transform.position - position).normalized;
                    float angle = Vector3.Angle(forwardDirection, directionToObject);

                    // 指定した角度内にあるかチェック
                    if (angle <= destroyAngle)
                    {
                        Destroy(collider.gameObject); // オブジェクトを破壊
                    }
                }
            }

            // 次のフレームまで待機
            yield return new WaitForSeconds(destroyDelay);
        }
    }

    private void OnDrawGizmos()
    {
        // デバッグ用: 破壊範囲を可視化
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, destroyRange); // 範囲の球
        Gizmos.DrawLine(transform.position, transform.position + transform.up * destroyRange); // 向いている方向
    }
}
