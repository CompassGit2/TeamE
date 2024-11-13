using System.Collections.Generic;
using Data.Database;

namespace Data
{
    
    public static class ItemDictionary
    {
        public static void Initialize(MaterialDatabase materialDatabase, RecipeDatabase recipeDatabase)
        {
            materialDictionary = new List<MaterialDictionaryData>();
            foreach(MaterialData materialData in materialDatabase.materialList)
            {
                MaterialDictionaryData materialDictionaryData = new (materialData);
                materialDictionary.Add(materialDictionaryData);
            }

            recipeDictionary = new List<RecipeDictionaryData>();
            foreach(RecipeData recipeData in recipeDatabase.recipeList)
            {
                RecipeDictionaryData recipeDictionaryData = new (recipeData);
                recipeDictionary.Add(recipeDictionaryData);
            }
        }

        /* 素材辞書 -------------------------------------------------------------------------------- */
        public static List<MaterialDictionaryData> MaterialDictionary
        {
            get => materialDictionary;
        }

        private static List<MaterialDictionaryData> materialDictionary = new List<MaterialDictionaryData>();

        public static void RegisterMaterialDictionary(MaterialData materialData)
        {
            MaterialDictionaryData materialDictionaryData = materialDictionary.Find(i => i.materialData == materialData);
            materialDictionaryData.registration = true;
        }

        public static bool IsMaterialRegistered(MaterialData materialData)
        {
            MaterialDictionaryData materialDictionaryData = materialDictionary.Find(i => i.materialData == materialData);
            return materialDictionaryData.registration;
        }

        /* レシピ辞書 ------------------------------------------------------------------------------- */
        public static List<RecipeDictionaryData> RecipeDictionary
        {
            get => recipeDictionary;
        }
        private static List<RecipeDictionaryData> recipeDictionary = new List<RecipeDictionaryData>();

        public static void RegisterRecipeDictionary(RecipeData recipeData)
        {
            RecipeDictionaryData recipeDictionaryData = recipeDictionary.Find(i => i.recipeData == recipeData);
            recipeDictionaryData.registration = true;
        }

        public static void RegisterRecipeDictionaryByWeapon(Weapon weapon)
        {
            RecipeDictionaryData recipeDictionaryData = recipeDictionary.Find(i => i.recipeData.Weapon == weapon.weapon);
            recipeDictionaryData.registration = true;
        }

    }

    public class MaterialDictionaryData
    {
        public MaterialData materialData;
        public bool registration;

        public MaterialDictionaryData(MaterialData materialData)
        {
            this.materialData = materialData;
            registration = false;
        }
    }

    public class RecipeDictionaryData
    {
        public RecipeData recipeData;
        public bool registration;
        public RecipeDictionaryData(RecipeData recipeData)
        {
            this.recipeData = recipeData;
            registration = false;
        }
    }
}
