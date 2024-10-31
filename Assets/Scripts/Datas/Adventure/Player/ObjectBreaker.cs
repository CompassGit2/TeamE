using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectBreaker : MonoBehaviour
{
    [SerializeField] GameObject miningSliderObj;
    Slider miningSlider;
    public AudioClip destroyedSound; // �j�󊮗����̌��ʉ�
    private GameObject targetObject; // �j��Ώۂ̃I�u�W�F�N�g
    private bool canBreak = false; // �j��\���ǂ����̃t���O
    public float holdTime = 3f; // �������ɕK�v�Ȏ���
    private float holdTimer = 0f;

    private GameObject audioManager;

    void Start()
    {
        // AudioManager�I�u�W�F�N�g��T��
        audioManager = GameObject.Find("AudioManager");
        miningSlider = miningSliderObj.GetComponent<Slider>();
    }


    void Update()
    {
        // ����G���A���ō��N���b�N�������ꂽ�ꍇ�A�^�C�}�[���J�n
        if (canBreak && Input.GetMouseButton(0) && targetObject != null)
        {
            miningSliderObj.SetActive(true);
            holdTimer += Time.deltaTime;
            miningSlider.value = holdTimer; 

            if (holdTimer >= holdTime)
            {
                Destroy(targetObject);
                Debug.Log("���̂�j�󂵂܂���");

                // �j�󊮗����̌��ʉ����Đ�
                if (destroyedSound != null && audioManager != null)
                {
                    // AudioSource���A�^�b�`����Ă��邩�m�F
                    AudioSource audioSource = audioManager.GetComponent<AudioSource>();
                    if (audioSource != null)
                    {
                        audioSource.PlayOneShot(destroyedSound);
                        Debug.Log("�j�󉹂��Đ����܂���");
                    }
                }

                // �j���Ƀt���O�ƃ^�C�}�[�����Z�b�g
                canBreak = false;
                targetObject = null;
                holdTimer = 0f;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            miningSliderObj.SetActive(false);
            holdTimer = 0f;
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
