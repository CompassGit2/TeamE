using UnityEngine;

namespace Data
{
    /// <summary>
    /// 依頼のデータ
    /// </summary>
    [CreateAssetMenu(menuName = "Data/Order")]

    public class OrderData : ScriptableObject
    {
        [SerializeField] private int Id;
        [SerializeField] private string Name;//依頼の名前
        [SerializeField] private Sprite OrderImage;//依頼のアイコン画像
        [SerializeField] private orderType OrderType;//依頼の種類
        [SerializeField] private int Reward;//報酬金額（人件費という感じで）
        [TextArea(3, 10)]
        [SerializeField] private string Description;//依頼の詳細
        [SerializeField] private requirements requirementType;//依頼条件
        [SerializeField] private int Rank;//依頼のランク＝世界ランクと同じ
        public requirements RequirementType => requirementType;//要求方式のゲッター’
        public int rank=>Rank;//レア度のゲッター
        public orderType orderType => OrderType;//依頼種類のゲッター
        public int reward => Reward;//報酬金額のゲッター
        public string description => Description;
        public Sprite orderIcon => OrderImage;
        ///<summary>
        /// 以下依頼条件のカスタムインスペクタービューに関する設定↓
        /// </summary>
        [SerializeField] private WeaponData weaponData;//武器名による直接指名時に現れる
        public string WeaponName => weaponData.Name;//武器名による直接指名時に現れる

        [System.Serializable]
        public class WeaponRequirements
        {
            public bool checkLength;     // 長さをチェックするか
            public bool checkWeight;     // 重さをチェックするか
            public bool checkSharpness;  // 鋭さをチェックするか
            
            public int requiredLength;   // 必要な長さ（最小値）
            public int requiredWeight;   // 必要な重さ（最小値）
            public int requiredSharpness; // 必要な鋭さ（最小値）
        }

        [SerializeField]
        private WeaponRequirements weaponRequirements = new WeaponRequirements();

        public WeaponRequirements Requirements => weaponRequirements;

        // 武器が要求を満たしているかチェックするメソッド
        public bool CheckWeaponRequirements(WeaponData weapon)
        {
            switch(requirementType){
                case requirements.ByData:
                    return weapon.Name == weaponData.Name;

                case requirements.SpecSpecifications:
                    if (weaponRequirements.checkLength && weapon.Length <= weaponRequirements.requiredLength)
                    return false;
                
                    if (weaponRequirements.checkWeight && weapon.Weight <= weaponRequirements.requiredWeight)
                    return false;
                    
                    if (weaponRequirements.checkSharpness && weapon.Sharpness <= weaponRequirements.requiredSharpness)
                    return false;
                
                return true;
                    
                case requirements.Rarity:
                    return weapon.Rarity == RequiredRarity;
                    
                default:
                    return false;

            }
        }
        [SerializeField] private int requiredRarity;//レア度、即ち剣のレア度に対して要求を出す
        public int RequiredRarity => requiredRarity;//レア度、即ち剣のレア度に対して要求を出すのゲッター
    }

      
    }


    public enum orderType
    {
        Normal,//一般の依頼
        Special //昇級試験の依頼（新しいレア度のピッケルを作るやつ）
            ///ピックルをアップグレードするやつ
    }
    public enum requirements
    {
        ByData,//武器名による直接指名
        SpecSpecifications,//武器スペックに対して要求を出す
        Rarity//レア度に対して要求を出す
    }



