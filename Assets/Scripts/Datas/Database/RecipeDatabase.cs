using System.Collections.Generic;
using UnityEngine;

namespace Data.Database
{
    [CreateAssetMenu(menuName = "Database/RecipeDatabase")]
    public class RecipeDatabase : ScriptableObject
    {
        public List<RecipeData> recipeList = new List<RecipeData>();

        public List<RecipeData> GetWeaponList()
        {
            return recipeList;
        }

    }
}

