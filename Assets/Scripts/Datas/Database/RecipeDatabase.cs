using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Database
{
    [CreateAssetMenu(menuName = "Database/RecipeDatabase")]
    public class RecipeDatabase : ScriptableObject
    {
        public List<RecipeData> recipeList = new List<RecipeData>();

        public List<RecipeData> GetRecipeList()
        {
            return recipeList;
        }

        /// <summary>
        /// 使用する素材でレシピを検索
        /// </summary>
        /// <param name="useMaterials">使用する素材のリスト</param>
        /// <returns></returns>
        public List<RecipeData> SearchRecipeByMaterials(List<MaterialStack> useMaterials)
        {
            List<RecipeData> foundRecipe = new List<RecipeData>();
            List<MaterialStack> sortedUse = new List<MaterialStack>(useMaterials);
            sortedUse.Sort((a, b) => a.material.Id.CompareTo(b.material.Id));

            foreach(RecipeData r in recipeList)
            {
                List<MaterialStack> needMaterials = new List<MaterialStack>();
                if(r.AmountMaterial1 > 0)
                {
                    needMaterials.Add(new MaterialStack(r.Material1, r.AmountMaterial1));
                }
                if(r.AmountMaterial2 > 0)
                {
                    needMaterials.Add(new MaterialStack(r.Material2, r.AmountMaterial2));
                }
                if(r.AmountMaterial3 > 0)
                {
                    needMaterials.Add(new MaterialStack(r.Material3, r.AmountMaterial3));
                }

                if(sortedUse.Count != needMaterials.Count) continue;

                needMaterials.Sort((a, b) => a.material.Id.CompareTo(b.material.Id));
                
                if(CompareMaterials(sortedUse, needMaterials))
                {
                    foundRecipe.Add(r);
                }

            }

            return foundRecipe;
        }

        bool CompareMaterials(List<MaterialStack> a, List<MaterialStack> b)
        {
            for(int i = 0; i < a.Count; i++)
            {
                if(a[i].material.Id != b[i].material.Id || a[i].amount != b[i].amount)
                {
                    return false;
                }
            }
            return true;
        }

    }
}



