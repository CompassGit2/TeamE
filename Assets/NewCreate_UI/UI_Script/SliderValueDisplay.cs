using UnityEngine;
using UnityEngine.UI;

public class SliderValueDisplay : MonoBehaviour
{
    public Slider slider;  // �X���C�_�[�̎Q��
    public Text valueText;  // �l��\������e�L�X�g�̎Q��

    void Start()
    {
        // �X���C�_�[�̏����l���e�L�X�g�ɕ\��
        UpdateValueText(slider.value);

        // �X���C�_�[�̒l���ύX���ꂽ�Ƃ���UpdateValueText���Ăяo��
        slider.onValueChanged.AddListener(delegate { UpdateValueText(slider.value); });
    }

    // �X���C�_�[�̒l���e�L�X�g�ɔ��f����
    void UpdateValueText(float value)
    {
        valueText.text = value.ToString("0");  // �����ŕ\��
    }
}
