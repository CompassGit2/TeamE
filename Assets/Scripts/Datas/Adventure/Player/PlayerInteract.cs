using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactRange = 2f;  // インタラクトの範囲
    public string targetTag = "Interactable";  // インタラクト対象のタグ

    void Update()
    {
        // Fキーが押されたときにインタラクト処理を行う
        if (Input.GetKeyDown(KeyCode.F))
        {
            InteractWithObject();
        }
    }

    void InteractWithObject()
    {
        // プレイヤーの前方にRayを飛ばす
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, interactRange);

        if (hit.collider != null && hit.collider.CompareTag(targetTag))
        {
            // インタラクト処理をここに追加
            Debug.Log("Interacted with: " + hit.collider.name);
        }
    }
}
