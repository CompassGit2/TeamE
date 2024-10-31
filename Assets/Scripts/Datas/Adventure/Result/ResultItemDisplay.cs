using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultItemDisplay : MonoBehaviour
{
    public Image itemImage; // �A�C�e���̉摜��\������Image
    public Text itemNameText; // �A�C�e���̖��O��\������Text
    public Text itemCountText; // �A�C�e���̌���\������Text

    public void SetItemInfo(Data.MaterialStack materialStack)
    {
        if (materialStack != null)
        {
            itemImage.sprite = materialStack.material.MaterialImage; // �摜�ݒ�
            itemNameText.text = materialStack.material.Name; // ���O�ݒ�
            itemCountText.text = materialStack.amount.ToString(); // ���ݒ�
        }
    }
}
