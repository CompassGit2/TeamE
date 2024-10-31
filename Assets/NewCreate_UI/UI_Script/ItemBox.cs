using UnityEngine;
using UnityEngine.EventSystems;

public class ItemBox : MonoBehaviour, IPointerClickHandler
{
    public GameObject uiPrefab;       // �\��������UI�v���t�@�u
    public GameObject canvasPrefab;    // �v���t�@�u�����ꂽ�L�����o�X
    private GameObject canvasInstance;  // �C���X�^���X�����ꂽ�L�����o�X
    private GameObject uiInstance;      // UI�v���t�@�u�̃C���X�^���X

    private float lastClickTime;
    private const float doubleClickThreshold = 0.25f; // �_�u���N���b�N�̔��莞��

    void Start()
    {
        canvasInstance = GameObject.Find("MaterialShop_Canvas");
        // �L�����o�X�����C���X�^���X���̏ꍇ�ɃC���X�^���X��
        if (canvasInstance == null)
        {
            canvasInstance = Instantiate(canvasPrefab);
            canvasInstance.name = "MainCanvas"; // ���O��t����ƌ�ł킩��₷��
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // �_�u���N���b�N�����m
        if (Time.time - lastClickTime < doubleClickThreshold)
        {
            // ���łɃC���X�^���X�����݂���ꍇ�͉������Ȃ�
            if (uiInstance == null)
            {
                ShowUIPrefab();
                //lastClickTime = 0;
            }
            else
            {
                // ���łɕ\������Ă���UI���\���ɂ��邩�j��
                //HideUIPrefab();
            }
        }
        lastClickTime = Time.time;
    }

    private void ShowUIPrefab()
    {
        // UI�v���t�@�u���C���X�^���X��
        uiInstance = Instantiate(uiPrefab);

        // UI�v���t�@�u���L�����o�X�̎q�ɐݒ�
        uiInstance.transform.SetParent(canvasInstance.transform, false); // ���[���h���W��ێ����Ȃ�

        RectTransform rectTransform = uiInstance.GetComponent<RectTransform>();

        // RectTransform���g����UI����ʒ����ɔz�u
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f); // �A���J�[�𒆉��ɐݒ�
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);     // �s�{�b�g��������
        rectTransform.anchoredPosition = Vector2.zero;     // ��ʒ����ɔz�u
    }

    private void HideUIPrefab()
    {
        // ���łɃC���X�^���X�����݂���ꍇ�͔�\���ɂ��邩�j��
        if (uiInstance != null)
        {
            // ��\���ɂ���ꍇ
            uiInstance.SetActive(false);
            // �j������ꍇ�i�R�����g�A�E�g���đI���j
            // Destroy(uiInstance);

            // uiInstance��null�ɐݒ�
            uiInstance = null; // ����ɂ��ĕ\���\�ɂȂ�
        }
    }
    

}
