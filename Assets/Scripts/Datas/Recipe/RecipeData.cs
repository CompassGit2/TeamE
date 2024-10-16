using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Data/Recipe")]
    public class RecipeData : ScriptableObject
    {
        public int Id;
        public WeaponData Weapon;
        public MaterialData Material1;
        public MaterialData Material2;
        public MaterialData Material3;
        public int MaxQuality;
    }
}

