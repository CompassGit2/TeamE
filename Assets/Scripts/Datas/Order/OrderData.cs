using UnityEngine;

namespace Data
{
    /// <summary>
    /// 依頼のデータ
    /// </summary>
    [CreateAssetMenu(menuName = "Data/Order")]

    public class OrderData : ScriptableObject
    {
        public int Id;
        public Sprite OrderImage;//依頼のアイコン画像
        public OrderType OrderType;//依頼の種類
        public int Reward;//報酬金額（人件費という感じで）
        [TextArea(3, 10)]
        public string Description;
        public Requirements RequirementType;//依頼条件
        public int Rank;//依頼のランク＝世界ランクと同じ
        
        // 以下依頼条件のカスタムインスペクタービューに関する設定↓
        public WeaponData weaponData;//武器名による直接指名時に現れる
        public SpecRequirements specRequirements;
        public int RequiredRarity;//レア度、即ち剣のレア度に対して要求を出す

    }
}



