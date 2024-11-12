using UnityEngine;
using UnityEngine.UI;
using Data;
using CommonUI;
using FancyScrollView;
using TMPro;

namespace MenuScene
{
    public class RecipeDictionaryCell : FancyGridViewCell<RecipeDictionaryData, Context> 
    {
        [SerializeField] Image weaponImage;
        [SerializeField] TextMeshProUGUI idText;
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] Button button;

        public override void Initialize()
        {
            button.onClick.AddListener(() => Context.OnCellClicked?.Invoke(Index));
        }

        public override void UpdateContent(RecipeDictionaryData recipeDictionaryData)
        {
            if(recipeDictionaryData.registration == true)
            {
                weaponImage.sprite = recipeDictionaryData.recipeData.Weapon.WeaponImage;
                weaponImage.color = new Color32(255,255,255,255);
                idText.text = $"ID:{recipeDictionaryData.recipeData.Id}";
                nameText.text = recipeDictionaryData.recipeData.Weapon.Name;
            }
            else
            {
                weaponImage.sprite = recipeDictionaryData.recipeData.Weapon.WeaponImage;
                weaponImage.color = new Color32(0,0,0,255);
                idText.text = $"ID:{recipeDictionaryData.recipeData.Id}";
                nameText.text = "？？？";
            }
        }
    }

}
