using UnityEngine;
using UnityEngine.UI;

public class CancelButton : MonoBehaviour
{


    void Start()
    {
        Button cancelButton = GetComponent<Button>();
        cancelButton.onClick.AddListener(HideUIPrefab);
    }

    private void HideUIPrefab()
    {
        GameObject uiInstance = transform.parent.gameObject;
        // ���łɃC���X�^���X�����݂���ꍇ�͔�\���ɂ��邩�j��
        if (uiInstance != null)
        {

       
            // ��\���ɂ���ꍇ
            //uiInstance.SetActive(false);
            // �j������ꍇ�i�R�����g�A�E�g���đI���j
            Destroy(uiInstance);

            // uiInstance��null�ɐݒ�
            //uiInstance = null; // ����ɂ��ĕ\���\�ɂȂ�
        }
    }


}
