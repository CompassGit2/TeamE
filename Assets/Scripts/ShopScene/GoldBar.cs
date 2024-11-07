using Data;
using TMPro;
using UnityEngine;

namespace ShopScene
{
    public class GoldBar : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI goldText;
        void Update()
        {
            goldText.text = $"{Storage.Gold}G";
        }
    }

}
