using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Data/Recipe")]
    public class RecipeData : ScriptableObject
    {
        public int Id;
        public WeaponData Weapon;
        public MaterialData Material1;
        public int AmountMaterial1;
        public MaterialData Material2;
        public int AmountMaterial2;
        public MaterialData Material3;
        public int AmountMaterial3;
        public int MaxQuality;
        public float RiseTemperatureSensitivity;
        public float DownTemperatureSensitivity;
    }
}

