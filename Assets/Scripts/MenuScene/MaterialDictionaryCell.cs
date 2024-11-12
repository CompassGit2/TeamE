using FancyScrollView;
using Data;
using CommonUI;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MenuScene
{
    public class MaterialDictionaryCell : FancyGridViewCell<MaterialDictionaryData, Context>
    {
        [SerializeField] Image materialImage;
        [SerializeField] TextMeshProUGUI idText;
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] Button button;

        public override void Initialize()
        {
            button.onClick.AddListener(() => Context.OnCellClicked?.Invoke(Index));
        }

        public override void UpdateContent(MaterialDictionaryData materialDictionaryData)
        {
            if(materialDictionaryData.registration == true)
            {
                materialImage.sprite = materialDictionaryData.materialData.MaterialImage;
                materialImage.color = new Color32(255,255,255,255);
                idText.text = $"ID:{materialDictionaryData.materialData.Id}";
                nameText.text = materialDictionaryData.materialData.Name;
            }
            else
            {
                materialImage.sprite = materialDictionaryData.materialData.MaterialImage;
                materialImage.color = new Color32(0,0,0,255);
                idText.text = $"ID:{materialDictionaryData.materialData.Id}";
                nameText.text = "？？？";
            }
        }
    }

}

