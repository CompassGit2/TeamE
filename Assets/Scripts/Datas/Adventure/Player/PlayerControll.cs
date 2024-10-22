using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject inventoryUI;
    private bool isInventoryOpen = false;
    public InventoryManager inventoryManager;
    private Sprite someSprite;

    void Update()
    {
        // WASDキーの入力を取得
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // 移動量を計算
        Vector3 move = new Vector3(moveX, moveY, 0) * moveSpeed * Time.deltaTime;

        // プレイヤーを移動
        transform.position += move;

        if (Input.GetKeyDown(KeyCode.E))
        {
            isInventoryOpen = !isInventoryOpen;  
            inventoryUI.SetActive(isInventoryOpen); 
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            inventoryManager.AddItem(1, 1); 
        }
    }
}
