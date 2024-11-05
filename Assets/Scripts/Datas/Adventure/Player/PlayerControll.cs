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
    public AudioSource WalkAudio; // �v���C���[�̑����p��AudioSource
    private Rigidbody2D rb;
    private KeyCode currentKey = KeyCode.None;
    private float lastMoveX = 0.0f;
    private float lastMoveY = -1.0f; // �f�t�H���g�͉�������

    void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // E�L�[�������ꂽ��C���x���g�����J��
        if (Input.GetKeyDown(KeyCode.E))
        {
            isInventoryOpen = !isInventoryOpen;  // �J���g�O��
            inventoryUI.SetActive(isInventoryOpen);  // �C���x���g��UI�̕\����؂�ւ�
        }
        if (isInventoryOpen)
        {
            return; // �ړ��������s��Ȃ�
        }

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
            if (!WalkAudio.isPlaying) // �������Đ�
            {
                WalkAudio.Play();
            }
        }
        else
        {
            WalkAudio.Stop(); // �������~
        }

        animator.SetFloat("MoveX", lastMoveX);
        animator.SetFloat("MoveY", lastMoveY);

        // �v���C���[�̈ړ���AddForce�Ŏ��s
        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized * speed;

        // �͂�������
        rb.AddForce(movement);

        // �ő呬�x�𒴂��Ȃ��悤�ɂ���
        if (rb.velocity.magnitude > Maxspeed)
        {
            rb.velocity = rb.velocity.normalized * Maxspeed;
        }
    }
}