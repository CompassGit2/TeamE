using System.Collections;
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
            button.interactable = true;
            _useMaterial = useMaterial;
        }

        public void Disable()
        {
            button.interactable = false;
        }

        public void OnStartButtonClicked()
        {
            foreach (MaterialStack stack in _useMaterial)
            {
                Storage.RemoveMaterial(stack.material, stack.amount);
            }
            smithGameManager.GameStart(recipe).Forget();
            selectMaterialPanel.SetActive(false);
        }

    }

}
