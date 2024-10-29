using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Data/Material")]
    public class MaterialData : ScriptableObject
    {
        public int Id;
        public string Name;
        public Sprite MaterialImage;
        public int Rarity;
        public int Price;
        public string Description;

    }
}


