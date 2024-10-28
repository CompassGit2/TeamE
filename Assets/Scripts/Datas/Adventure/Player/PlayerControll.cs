using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public float speed;
    public float Maxspeed;
    public float drag = 20.0f;    // 摩擦力の強さ
    public Animator animator;
    public GameObject inventoryUI; 
    private bool isInventoryOpen = false;  
    public string targetTag = "Destructible"; // 破壊対象のタグ
    private bool isDestroying = false; 
    private float destroyDelay = 0.5f; 
    private Coroutine destroyCoroutine;
    public float destroyRange = 2f;
    public float destroyAngle = 45f;
    private Rigidbody2D rb;
    private KeyCode currentKey = KeyCode.None;
    private float lastMoveX = 0.0f;
    private float lastMoveY = -1.0f; // デフォルトは下を向く

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        // 水平方向と垂直方向の入力を取得
        float moveHorizontal = 0.0f;
        float moveVertical = 0.0f;

        // 現在のキー入力が無効であれば、新しい入力を受け付ける
        if (currentKey == KeyCode.None)
        {
            if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                currentKey = KeyCode.W;
                moveVertical = 1.0f;  // 上移動
            }
            else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                currentKey = KeyCode.S;
                moveVertical = -1.0f;  // 下移動
            }
            else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                currentKey = KeyCode.A;
                moveHorizontal = -1.0f;  // 左移動
            }
            else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A))
            {
                currentKey = KeyCode.D;
                moveHorizontal = 1.0f;  // 右移動
            }
        }
        else
        {
            // 現在のキー入力を保持
            if (Input.GetKey(currentKey))
            {
                switch (currentKey)
                {
                    case KeyCode.W:
                        moveVertical = 1.0f;  // 上移動
                        break;
                    case KeyCode.S:
                        moveVertical = -1.0f;  // 下移動
                        break;
                    case KeyCode.A:
                        moveHorizontal = -1.0f;  // 左移動
                        break;
                    case KeyCode.D:
                        moveHorizontal = 1.0f;  // 右移動
                        break;
                }
            }
            else
            {
                // 現在のキーが放されたら、キー入力をリセット
                currentKey = KeyCode.None;
            }
        }

        // アニメーションのパラメータを設定
        int speedValue = (int)(Mathf.Abs(moveHorizontal) + Mathf.Abs(moveVertical));
        animator.SetInteger("Speed", speedValue);

        if (speedValue > 0)
        {
            lastMoveX = moveHorizontal;
            lastMoveY = moveVertical;
        }

        animator.SetFloat("MoveX", lastMoveX);
        animator.SetFloat("MoveY", lastMoveY);

        // 入力に基づいて力を計算
        Vector2 movement = new Vector2(moveHorizontal, moveVertical) * speed;

        // 力を加える
        rb.AddForce(movement);

        // 最大速度を超えないようにする
        if (rb.velocity.magnitude > Maxspeed)
        {
            rb.velocity = rb.velocity.normalized * Maxspeed;
        }

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
