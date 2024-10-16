using UnityEngine;

namespace ItemData
{
    [CreateAssetMenu(menuName = "Database/Material")]
    public class MaterialData : ScriptableObject
    {
        public int Id;
        public string Name;
        public Sprite MaterialImage;
        public int Rarity;
        public string description;
        public int price;

    }
}


