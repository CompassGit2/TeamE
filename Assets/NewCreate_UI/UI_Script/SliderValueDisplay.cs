using UnityEngine;
using UnityEngine.UI;

public class SliderValueDisplay : MonoBehaviour
{
    public Slider slider;  // スライダーの参照
    public Text valueText;  // 値を表示するテキストの参照

    void Start()
    {
        // スライダーの初期値をテキストに表示
        UpdateValueText(slider.value);

        // スライダーの値が変更されたときにUpdateValueTextを呼び出す
        slider.onValueChanged.AddListener(delegate { UpdateValueText(slider.value); });
    }

    // スライダーの値をテキストに反映する
    void UpdateValueText(float value)
    {
        valueText.text = value.ToString("0");  // 整数で表示
    }
}
