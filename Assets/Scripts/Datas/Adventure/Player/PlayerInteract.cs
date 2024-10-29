using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactRange = 2f;  // �C���^���N�g�͈̔�
    public string targetTag = "Interactable";  // �C���^���N�g�Ώۂ̃^�O

    void Update()
    {
        // F�L�[�������ꂽ�Ƃ��ɃC���^���N�g�������s��
        if (Input.GetKeyDown(KeyCode.F))
        {
            InteractWithObject();
        }
    }

    void InteractWithObject()
    {
        // �v���C���[�̑O����Ray���΂�
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, interactRange);

        if (hit.collider != null && hit.collider.CompareTag(targetTag))
        {
            // �C���^���N�g�����������ɒǉ�
            Debug.Log("Interacted with: " + hit.collider.name);
        }
    }
}
