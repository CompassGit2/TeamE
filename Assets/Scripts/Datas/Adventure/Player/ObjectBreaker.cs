using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBreaker : MonoBehaviour
{
    public AudioClip destroyedSound; // �j�󊮗����̌��ʉ�
    private GameObject targetObject; // �j��Ώۂ̃I�u�W�F�N�g
    private bool canBreak = false; // �j��\���ǂ����̃t���O
    public float holdTime = 3f; // �������ɕK�v�Ȏ���
    private float holdTimer = 0f;


    void Update()
    {
        // ����G���A���ō��N���b�N�������ꂽ�ꍇ�A�^�C�}�[���J�n
        if (canBreak && Input.GetMouseButton(0) && targetObject != null)
        {
            holdTimer += Time.deltaTime;

            if (holdTimer >= holdTime)
            {
                // �j�󉹂��Đ�
                AudioSource targetAudioSource = targetObject.GetComponent<AudioSource>();
                if (targetAudioSource != null && destroyedSound != null)
                {
                    targetAudioSource.PlayOneShot(destroyedSound); // �j�󉹂��Đ�
                    Debug.Log("�j�󉹂��Đ����܂���"); // �f�o�b�O���O�ǉ�
                }
                else
                {
                    Debug.LogWarning("�j�󉹂̍Đ��Ɏ��s���܂���"); // �G���[���O�ǉ�
                }


                Destroy(targetObject); // �I�u�W�F�N�g��j��
                Debug.Log("���̂�j�󂵂܂���");

                // �j���Ƀt���O�ƃ^�C�}�[�����Z�b�g
                canBreak = false;
                targetObject = null;
                holdTimer = 0f;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            holdTimer = 0f; // �^�C�}�[���Z�b�g
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Destructible"))
        {
            targetObject = collision.gameObject;
            canBreak = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == targetObject)
        {
            canBreak = false;
            targetObject = null;
            holdTimer = 0f;
        }
    }
}
