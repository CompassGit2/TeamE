using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBreaker : MonoBehaviour
{
    private GameObject targetObject; // �j��Ώۂ̃I�u�W�F�N�g
    private bool canBreak = false; // �j��\���ǂ����̃t���O
    void Update()
    {
        // ����G���A���ō��N���b�N�������ꂽ�ꍇ�ɂ̂ݕ��̂�j��
        if (canBreak && Input.GetMouseButtonDown(0) && targetObject != null) // 0 �͍��N���b�N��\��
        {
            Destroy(targetObject);
            Debug.Log("���̂�j�󂵂܂���");

            // �j���Ƀt���O�����Z�b�g
            canBreak = false;
            targetObject = null;
        }
    }

    // ����G���A�ɓ��������Ƀt���O��ݒ�
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Destructible")) 
        {
            targetObject = collision.gameObject;
            canBreak = true;
        }
    }

    // ����G���A����o�����Ƀt���O�����Z�b�g
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == targetObject)
        {
            canBreak = false;
            targetObject = null;
        }
    }
}
