using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public float moveSpeed = 5f;  // �v���C���[�̈ړ����x
    public GameObject inventoryUI; 
    private bool isInventoryOpen = false;  
    public string targetTag = "Destructible"; // �j��Ώۂ̃^�O
    private bool isDestroying = false; 
    private float destroyDelay = 0.5f; 
    private Coroutine destroyCoroutine;
    public float destroyRange = 2f;
    public float destroyAngle = 45f;
    void Update()
    {
        // ���ړ��̓��͂��擾
        float moveX = Input.GetAxis("Horizontal");
        // �c�ړ��̓��͂��擾�i���ړ����Ȃ��ꍇ�̂݁j
        float moveY = moveX == 0 ? Input.GetAxis("Vertical") : 0;

        // �ړ��ʂ��v�Z
        Vector3 move = new Vector3(moveX, moveY, 0) * moveSpeed * Time.deltaTime;

        // �v���C���[���ړ�
        transform.position += move;

        // E�L�[�������ꂽ��C���x���g�����J��
        if (Input.GetKeyDown(KeyCode.E))
        {
            isInventoryOpen = !isInventoryOpen;  // �J���g�O��
            inventoryUI.SetActive(isInventoryOpen);  // �C���x���g��UI�̕\����؂�ւ�
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
            // �v���C���[�̑O���̕������擾
            Vector3 forwardDirection = transform.up; // �v���C���[�̌����Ă������
            Vector3 position = transform.position; // �v���C���[�̈ʒu

            // �͈͓��ɂ���I�u�W�F�N�g���擾
            Collider[] colliders = Physics.OverlapSphere(position, destroyRange);

            foreach (Collider collider in colliders)
            {
                // �j��Ώۂ̃^�O�������Ă��邩�`�F�b�N
                if (collider.CompareTag(targetTag))
                {
                    // �v���C���[�̌����Ă�������Ƃ̊p�x���v�Z
                    Vector3 directionToObject = (collider.transform.position - position).normalized;
                    float angle = Vector3.Angle(forwardDirection, directionToObject);

                    // �w�肵���p�x���ɂ��邩�`�F�b�N
                    if (angle <= destroyAngle)
                    {
                        Destroy(collider.gameObject); // �I�u�W�F�N�g��j��
                    }
                }
            }

            // ���̃t���[���܂őҋ@
            yield return new WaitForSeconds(destroyDelay);
        }
    }

    private void OnDrawGizmos()
    {
        // �f�o�b�O�p: �j��͈͂�����
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, destroyRange); // �͈͂̋�
        Gizmos.DrawLine(transform.position, transform.position + transform.up * destroyRange); // �����Ă������
    }
}
