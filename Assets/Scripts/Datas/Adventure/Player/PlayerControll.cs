using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public float speed;
    public float Maxspeed;
    public float drag = 20.0f;    // ���C�͂̋���
    public Animator animator;
    public GameObject inventoryUI; 
    private bool isInventoryOpen = false;  
    public string targetTag = "Destructible"; // �j��Ώۂ̃^�O
    private bool isDestroying = false; 
    private float destroyDelay = 0.5f; 
    private Coroutine destroyCoroutine;
    public float destroyRange = 2f;
    public float destroyAngle = 45f;
    private Rigidbody2D rb;
    private KeyCode currentKey = KeyCode.None;
    private float lastMoveX = 0.0f;
    private float lastMoveY = -1.0f; // �f�t�H���g�͉�������

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        // ���������Ɛ��������̓��͂��擾
        float moveHorizontal = 0.0f;
        float moveVertical = 0.0f;

        // ���݂̃L�[���͂������ł���΁A�V�������͂��󂯕t����
        if (currentKey == KeyCode.None)
        {
            if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                currentKey = KeyCode.W;
                moveVertical = 1.0f;  // ��ړ�
            }
            else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                currentKey = KeyCode.S;
                moveVertical = -1.0f;  // ���ړ�
            }
            else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                currentKey = KeyCode.A;
                moveHorizontal = -1.0f;  // ���ړ�
            }
            else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A))
            {
                currentKey = KeyCode.D;
                moveHorizontal = 1.0f;  // �E�ړ�
            }
        }
        else
        {
            // ���݂̃L�[���͂�ێ�
            if (Input.GetKey(currentKey))
            {
                switch (currentKey)
                {
                    case KeyCode.W:
                        moveVertical = 1.0f;  // ��ړ�
                        break;
                    case KeyCode.S:
                        moveVertical = -1.0f;  // ���ړ�
                        break;
                    case KeyCode.A:
                        moveHorizontal = -1.0f;  // ���ړ�
                        break;
                    case KeyCode.D:
                        moveHorizontal = 1.0f;  // �E�ړ�
                        break;
                }
            }
            else
            {
                // ���݂̃L�[�������ꂽ��A�L�[���͂����Z�b�g
                currentKey = KeyCode.None;
            }
        }

        // �A�j���[�V�����̃p�����[�^��ݒ�
        int speedValue = (int)(Mathf.Abs(moveHorizontal) + Mathf.Abs(moveVertical));
        animator.SetInteger("Speed", speedValue);

        if (speedValue > 0)
        {
            lastMoveX = moveHorizontal;
            lastMoveY = moveVertical;
        }

        animator.SetFloat("MoveX", lastMoveX);
        animator.SetFloat("MoveY", lastMoveY);

        // ���͂Ɋ�Â��ė͂��v�Z
        Vector2 movement = new Vector2(moveHorizontal, moveVertical) * speed;

        // �͂�������
        rb.AddForce(movement);

        // �ő呬�x�𒴂��Ȃ��悤�ɂ���
        if (rb.velocity.magnitude > Maxspeed)
        {
            rb.velocity = rb.velocity.normalized * Maxspeed;
        }

        // E�L�[�������ꂽ��C���x���g�����J��
        if (Input.GetKeyDown(KeyCode.E))
        {
            isInventoryOpen = !isInventoryOpen;  // �J���g�O��
            inventoryUI.SetActive(isInventoryOpen);  // �C���x���g��UI�̕\����؂�ւ�
        }

        if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButton(0))
            {
                Debug.Log("���N���b�N��������܂���");
                if (!isDestroying)
                {
                    isDestroying = true;
                    destroyCoroutine = StartCoroutine(DestroyTarget());
                }
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
            Vector3 position = transform.position; // �v���C���[�̈ʒu
            Debug.Log($"�v���C���[�ʒu: {position}"); // �ǉ�    

            // �͈͓��ɂ���I�u�W�F�N�g���擾
            Collider[] colliders = Physics.OverlapSphere(position, destroyRange);
            Debug.Log($"�͈͓��I�u�W�F�N�g��: {colliders.Length}"); // �ǉ�

            foreach (Collider collider in colliders)
            {
                Debug.Log($"�I�u�W�F�N�g: {collider.gameObject.name}, �^�O: {collider.tag}");

                if (collider.CompareTag(targetTag))
                {
                    Vector3 forwardDirection = transform.up; // �v���C���[�̌����Ă������
                    Vector3 directionToObject = (collider.transform.position - position).normalized;
                    float angle = Vector3.Angle(forwardDirection, directionToObject);

                    Debug.Log($"�p�x: {angle}"); // �ǉ�


                    if (angle <= destroyAngle)
                    {
                        DestructibleObject destructible = collider.GetComponent<DestructibleObject>();
                        if (destructible != null)
                        {
                            Debug.Log($"�j��Ώ�: {collider.gameObject.name}");
                            destructible.DestroyObject(); // �I�u�W�F�N�g��j��
                        }
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
