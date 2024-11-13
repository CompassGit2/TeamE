using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using UnityEngine.UIElements;

namespace ShopScene
{
    public class ShopItemTable : MonoBehaviour
    {
        [SerializeField] List<MaterialStack> rank0ShopItemTable = new List<MaterialStack>();
        [SerializeField] List<MaterialStack> rank1ShopItemTable = new List<MaterialStack>();
        [SerializeField] List<MaterialStack> rank2ShopItemTable = new List<MaterialStack>();
        [SerializeField] List<MaterialStack> rank3ShopItemTable = new List<MaterialStack>();
        [SerializeField] List<MaterialStack> rank4ShopItemTable = new List<MaterialStack>();
        [SerializeField] List<MaterialStack> rank5ShopItemTable = new List<MaterialStack>();
        List<List<MaterialStack>> itemTables;

        void Start()
        {
            itemTables = new() {rank0ShopItemTable, rank1ShopItemTable, rank2ShopItemTable, rank3ShopItemTable, rank4ShopItemTable, rank5ShopItemTable};
        }

        public void AddShopItem()
        {
            Storage.SetShopMaterial(itemTables[PlayerData.WorldRank]);
        }
    }
    
}
