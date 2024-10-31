using System.Collections.Generic;
using UnityEngine;

namespace Data.Database
{
    [CreateAssetMenu(menuName = "Database/ShopDatabase")]
    public class ShopDatabase : ScriptableObject
    {   //Databaseという名前だが実は色んな商品のオリジナルを保存しているデータベースではない。
        //単に商店側（売る側）の倉庫という機能を発揮している
        //データのみを管理している、UI画面とは関係ない。
        
        public MaterialDatabase materialDatabase;//売ってるものは素材そのものなので
                                                 //参照元はMaterialDatabase
        private List<ShopDetail> shopList=new();
        private int _worldRank=0;//前回商品リストを生成した時のワールドランク記録

        [SerializeField]private int[] stockamountbyRank= { 60,50,40,30,20,10,5}; //各ランク商品を初期化する際の数量
        public int[] StockAmountByRank => stockamountbyRank;//
        
        private int selectedAmount = 0;//選択した商品の数量

        //void Start()//ScriptableObjectの場合Start関数は実行されないので注意QAQ
        //{
        //    Debug.Log("ShopDatabase Start");
        //    GenerateShopLists(materialDatabase, OrderManager.WorldRank);//ここで初期化
        //    SortShopList();
        //    _worldRank = OrderManager.WorldRank;
        //}

        public void Initialize()//外部から呼び出さなければいけない
        {
            Debug.Log("ShopDatabase Start");
            GenerateShopLists(materialDatabase, OrderManager.WorldRank);//ここで初期化
            SortShopList();
            _worldRank = OrderManager.WorldRank;
        }

        void Update()//こいつは手動的にワールドランクが上がったときに呼び出す必要がある？。
        {
            if(_worldRank!= OrderManager.WorldRank)
            {
                GenerateShopLists(materialDatabase, OrderManager.WorldRank);
                SortShopList();
                _worldRank = OrderManager.WorldRank;
            }
        }

        public void UpdateManually()
        {
            if (_worldRank != OrderManager.WorldRank)
            {
                GenerateShopLists(materialDatabase, OrderManager.WorldRank);
                SortShopList();
                _worldRank = OrderManager.WorldRank;
            }
        }


        public void GenerateShopLists(MaterialDatabase materialDatabase,int WorldRank)
        {
            shopList.Clear();
            List<MaterialData>allMaterials = materialDatabase.GetMaterialList();
            for (int i = 0; i < allMaterials.Count; i++)
            {
                if (allMaterials[i].Rarity <= WorldRank)
                {
                    ShopDetail shopDetail = new(allMaterials[i], stockamountbyRank[allMaterials[i].Rarity]);
                    Debug.Log("今の"+allMaterials[i].name+"の入荷数は？"+ stockamountbyRank[allMaterials[i].Rarity]);
                    //どれくらいいるかはまた後で、今はランクごとに設定してる
                    shopList.Add(shopDetail);
                }
            }
        }

        //売れたものの数を更新する
        public void UpdateShopList(ShopDetail item)
        {
            //ここで在庫の数削る前に数量上限設定の関数があるはず、これはShopManagerの方でやる
            shopList.Find(x => x.material.Id == item.material.Id).amount -= item.amount;
            if(shopList.Find(x => x.material.Id == item.material.Id).amount <= 0){
                //売り切れ用のフラグとかあれば？いらんか。
            }

        }

        //整列させる必要がある
        public void SortShopList()
        {
            shopList.Sort((x, y) => -x.material.Rarity.CompareTo(y.material.Rarity));
            //降順にする
        }

        public List<ShopDetail> GetShopList() { 
            return shopList;
        }

    }
}