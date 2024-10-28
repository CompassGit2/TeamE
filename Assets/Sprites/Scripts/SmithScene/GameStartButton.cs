using System.Collections.Generic;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SmithScene.SelectMaterial
{
    public class GameStartButton : MonoBehaviour
    {
        [SerializeField] Game.SmithGameManager smithGameManager;
        [SerializeField] GameObject selectMaterialPanel;
        [SerializeField] TextMeshProUGUI buttonText;
        [SerializeField] AudioSource systemSoundEffect;
        public AudioClip enableSound;
        public AudioClip clickSound;
        public RecipeData recipe;
        Button button;
        Color32 textColor;

        List<MaterialStack> _useMaterial;
        void Start()
        {
            button = GetComponent<Button>();
            textColor = buttonText.color;
            Disable();
        }

        public void Enable(RecipeData recipeData, List<MaterialStack> useMaterial)
        {
            recipe = recipeData;
            systemSoundEffect.PlayOneShot(enableSound);
            button.interactable = true;
            buttonText.color = new Color32(textColor.r, textColor.g, textColor.b, 255);
            _useMaterial = useMaterial;
        }

        public void Disable()
        {
            buttonText.color = new Color32(textColor.r, textColor.g, textColor.b, 50);
            button.interactable = false;
            
        }

        public void OnStartButtonClicked()
        {
            systemSoundEffect.PlayOneShot(clickSound);
            foreach (MaterialStack stack in _useMaterial)
            {
                Storage.RemoveMaterial(stack.material, stack.amount);
            }
            smithGameManager.GameStart(recipe).Forget();
            selectMaterialPanel.SetActive(false);
        }

    }

}
