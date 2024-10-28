using UnityEngine;

namespace Data
{   
    /// <summary>
    /// 武器データ
    /// </summary>
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
        public int BasePrice;
        public string Description;

    }
}

