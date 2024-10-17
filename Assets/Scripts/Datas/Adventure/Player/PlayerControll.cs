using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject inventoryUI;
    private bool isInventoryOpen = false;

    void Update()
    {
        // WASD�L�[�̓��͂��擾
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // �ړ��ʂ��v�Z
        Vector3 move = new Vector3(moveX, moveY, 0) * moveSpeed * Time.deltaTime;

        // �v���C���[���ړ�
        transform.position += move;

        if (Input.GetKeyDown(KeyCode.E))
        {
            isInventoryOpen = !isInventoryOpen;  
            inventoryUI.SetActive(isInventoryOpen); 
        }
    }
}
