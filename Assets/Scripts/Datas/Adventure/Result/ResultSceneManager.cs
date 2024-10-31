using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data; // ������Data���O��Ԃ��C���|�[�g
using Data.Database;

public class ResultSceneManager : MonoBehaviour
{
    public ResultItemDisplay[] resultItemDisplays; // UI�v�f�̔z��
    public MaterialDatabase materialDatabase; // �}�e���A���f�[�^�x�[�X�̎Q��

    // �擾�����A�C�e���̏���\�����郁�\�b�h
    public void DisplayResults(List<MaterialStack> collectedMaterials)
    {
        for (int i = 0; i < resultItemDisplays.Length; i++)
        {
            if (i < collectedMaterials.Count)
            {
                // collectedMaterials[i]��Data.MaterialStack�^�ł��邱�Ƃ��m�F
                resultItemDisplays[i].SetItemInfo(collectedMaterials[i]); // �A�C�e������ݒ�
                resultItemDisplays[i].gameObject.SetActive(true); // �A�C�e����\��
            }
            else
            {
                resultItemDisplays[i].gameObject.SetActive(false); // �A�C�e�����Ȃ��ꍇ�͔�\��
            }
        }
    }
}

