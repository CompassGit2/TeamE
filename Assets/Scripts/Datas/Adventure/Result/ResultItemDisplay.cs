using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultItemDisplay : MonoBehaviour
{
    public Image itemImage; // アイテムの画像を表示するImage
    public Text itemNameText; // アイテムの名前を表示するText
    public Text itemCountText; // アイテムの個数を表示するText

    public void SetItemInfo(Data.MaterialStack materialStack)
    {
        if (materialStack != null)
        {
            itemImage.sprite = materialStack.material.MaterialImage; // 画像設定
            itemNameText.text = materialStack.material.Name; // 名前設定
            itemCountText.text = materialStack.amount.ToString(); // 個数設定
        }
    }
}
