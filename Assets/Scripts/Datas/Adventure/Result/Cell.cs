using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ResultScene
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] Image itemImage;
        [SerializeField] TextMeshProUGUI itemCountTMP;
        [SerializeField] TextMeshProUGUI itemNameTMP;

        public void SetItemData(Sprite image, int amount, string name)
        {
            itemImage.sprite = image;
            itemCountTMP.text = amount.ToString();
            itemNameTMP.text = name;
        }
    }

}
