using System.Collections;
using System.Collections.Generic;
using Data;
using Data.Database;
using UnityEngine;

public class TestInitializeDictionaryButton : MonoBehaviour
{
    [SerializeField] MaterialDatabase materialDatabase;
    [SerializeField] RecipeDatabase recipeDatabase;
    public void OnClickButton()
    {
        ItemDictionary.Initialize(materialDatabase, recipeDatabase);
    }
}
