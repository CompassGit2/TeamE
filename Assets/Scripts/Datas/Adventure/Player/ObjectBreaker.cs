using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBreaker : MonoBehaviour
{
    public AudioClip breakingSound; // �j�󒆂̌��ʉ�
    public AudioClip destroyedSound; // �j�󊮗����̌��ʉ�
    private AudioSource audioSource;
    private GameObject targetObject; // �j��Ώۂ̃I�u�W�F�N�g
    private bool canBreak = false; // �j��\���ǂ����̃t���O
    public float holdTime = 3f; // �������ɕK�v�Ȏ���
    private float holdTimer = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // ����G���A���ō��N���b�N�������ꂽ�ꍇ�A�^�C�}�[���J�n
        if (canBreak && Input.GetMouseButton(0) && targetObject != null)
        {
            holdTimer += Time.deltaTime;

            // �j�󒆂̌��ʉ����Đ�
            if (!audioSource.isPlaying && breakingSound != null)
            {
                audioSource.clip = breakingSound;
                audioSource.Play();
            }

            if (holdTimer >= holdTime)
            {
                Destroy(targetObject);
                Debug.Log("���̂�j�󂵂܂���");

                // �j�󊮗����̌��ʉ����Đ�
                audioSource.Stop(); // �j�󒆂̉����~
                if (destroyedSound != null)
                {
                    audioSource.PlayOneShot(destroyedSound);
                }

                // �j���Ƀt���O�ƃ^�C�}�[�����Z�b�g
                canBreak = false;
                targetObject = null;
                holdTimer = 0f;
            }
        }
        else if (Input.GetMouseButtonUp(0)) 
        {
            holdTimer = 0f;
            audioSource.Stop();
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
            holdTimer = 0f; // �G���A�O�ɏo����^�C�}�[�����Z�b�g
            audioSource.Stop();
        }
    }
}
