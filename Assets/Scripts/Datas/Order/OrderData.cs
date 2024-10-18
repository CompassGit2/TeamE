using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        [SerializeField] private string Description;//依頼の詳細
        [SerializeField] private requirements requirementType;//依頼条件
        [SerializeField] private int Rank;//依頼のランク＝世界ランクと同じ
        public requirements RequirementType => requirementType;//要求方式のゲッター’
        public int rank=>Rank;//レア度のゲッター
        ///<summary>
        /// 以下依頼条件のカスタムインスペクタービューに関する設定↓
        /// </summary>
        [SerializeField] private string PropertybyName;//武器名による直接指名時に現れる

        [System.Serializable]
        public struct Property
        {
            public int Length;
            public int Weight;
            public int Sharpness;
        }
        [SerializeField] private Property properties = new() { Length =0,Weight = 0,Sharpness= 0};//武器スペックに対して要求を出す
        [SerializeField] private int PropertybyRarity;//レア度、即ち剣のレア度に対して要求を出す
    }



    public enum orderType
    {
        Normal,//一般の依頼
        Special //昇級試験の依頼（新しいレア度のピッケルを作るやつ）
            ///ピックルをアップグレードするやつ
    }
    public enum requirements
    {
        ByName,//武器名による直接指名
        SpecSpecifications,//武器スペックに対して要求を出す
        Rarity　//レア度に対して要求を出す
    }
}


