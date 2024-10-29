using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public float speed;
    public float Maxspeed;
    public Animator animator;
    public GameObject inventoryUI;
    private bool isInventoryOpen = false;
    private Rigidbody2D rb;
    private KeyCode currentKey = KeyCode.None;
    private float lastMoveX = 0.0f;
    private float lastMoveY = -1.0f; // デフォルトは下を向く

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
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

        // プレイヤーの移動をAddForceで実行
        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized * speed;

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
    }
}

        

    


