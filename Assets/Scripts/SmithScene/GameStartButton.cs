using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace SmithScene.SelectMaterial
{
    public class GameStartButton : MonoBehaviour
    {
        [SerializeField] Game.SmithGameManager smithGameManager;
        [SerializeField] GameObject selectMaterialPanel;
        [SerializeField] AudioSource systemSoundEffect;
        public AudioClip enableSound;
        public AudioClip clickSound;
        public RecipeData recipe;
        Button button;

        List<MaterialStack> _useMaterial;
        void Start()
        {
            button = GetComponent<Button>();
            button.interactable = false;
        }

        public void Enable(RecipeData recipeData, List<MaterialStack> useMaterial)
        {
            recipe = recipeData;
            systemSoundEffect.PlayOneShot(enableSound);
            button.interactable = true;
            _useMaterial = useMaterial;
        }

        public void Disable()
        {
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
