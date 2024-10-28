using FancyScrollView;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Data;

namespace SmithScene.SelectMaterial
{
    class Cell : FancyGridViewCell<MaterialStack, Context>
    {
        [SerializeField] Button button;
        [SerializeField] Image image = default;
        [SerializeField] TextMeshProUGUI amount = default;
        [SerializeField] TextMeshProUGUI useAmount = default;

        public override void Initialize()
        {
            button.onClick.AddListener(() => Context.OnCellClicked?.Invoke(Index));
        }

        public override void UpdateContent(MaterialStack itemData)
        {
            amount.text = itemData.amount.ToString();
            image.sprite = itemData.material.MaterialImage;


        }

        public override void UpdatePosition(float position)
        {
            // position は 0.0 ~ 1.0 の値です
            // position に基づいてスクロールの外観を自由に制御できます
            base.UpdatePosition(position);
        }

    }

}
