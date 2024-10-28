using System.Collections;
using System.Collections.Generic;
using Data;
using SmithScene.Game;
using UnityEngine;

public class TestGameStart : MonoBehaviour
{
    [SerializeField] SmithGameManager smithGameManager;
    [SerializeField] RecipeData recipeData;
    public void OnTestGameStartButtonClick()
    {
        smithGameManager.GameStart(recipeData).Forget();
    }
}
