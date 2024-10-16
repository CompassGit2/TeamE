using UnityEngine;

namespace Data
{   
    [CreateAssetMenu(menuName = "Data/Weapon")]
    public class WeaponData : ScriptableObject 
    {
        public int Id;
        public string Name;
        public Sprite WeaponImage;
        public int Rarity;
        public int Length;
        public int Weight;
        public int Sharpness;
        public int Bonus;
        public int Attack;
        public int Price;
        public string Description;

    }
}

