using Data;//OrderData.csの中の名前空間
using System.Collections.Generic;
using System.Linq;
using Data.Database;
using UnityEditor;
using UnityEngine;



public static class OrderManager
{
    private static OrderDatabase database;//データベース
    
    private static bool isInitialized = false;//データベースの初期化flag
    public static List<OrderData> AllOrders;//代入はInitializeで行うべき
    //public static List<OrderData> AllOrders=> orderDatabase.GetOrderList();//こう書くとバグる?

    //全ての依頼
    public static List<OrderData> AcceptableOrders { get; private set; }//今の世界ランクで引き受けられる依頼
    
    public static int WorldRank = 0;
    //世界ランク　ピッケルのランクでもある
    public static OrderData CurrentOrder { get; private set; }
    //現在選択中の依頼
    //public static List<OrderData> CompletedOrders { get; private set; }
    //完了した依頼
    public static int FinishedNormalOrders = 0;
    //完了した通常依頼の数
    public static int FinishedSpecialOrders = 0;
    //特殊依頼の数

    public static int[] OrderCountsByRank { get; private set; }
    //ランクごとの依頼数

    public static bool HasDatabase()
    {
        return database != null;
    }


    // 必要に応じて、特定の注文を取得するメソッドを追加
    public static OrderData GetOrderById(int id)
    {
        return AllOrders.Find(order => order);
    }
    //※初期化、データベースのインスタンスを受け取る
    public static void Initialize(OrderDatabase _orderDatabase)
    {
        if (!isInitialized)
        {
            database = _orderDatabase;
            isInitialized = true;
            Debug.Log("OrderManager initialized.");
            AllOrders = database.GetOrderList();
            if(AllOrders==null)
                Debug.Log("OrderManager: AllOrders is null.");
            else
            {
                //Debug.Log("OrderManager: AllOrders count: " + AllOrders.Count);
                //AcceptableOrders = AllOrders.Where(order => order.rank == WorldRank && order.orderType == orderType.Normal).ToList();
                //Debug.Log("OrderManager: AcceptableOrders count: " + AcceptableOrders.Count);
            }
        }
        else
        {
            Debug.Log("OrderManager is already initialized.");
        }
    }

   //依頼を引き受けるメソッド
   //OrderUIManagerの中でAcceptableOrdersを更新する前に呼び出す必要がある
    public static void FetchOrdersByRank()
    {

        Debug.Log($"OrderManager state: WorldRank={WorldRank}, FinishedNormalOrders={FinishedNormalOrders}");
        Debug.Log($"OrderDatabase null? {database == null}");
        Debug.Log($"AllOrders null? {AllOrders == null}");
        if (FinishedNormalOrders == 0)//初回の呼び出し時
            AcceptableOrders = AllOrders.Where(order => order.rank == WorldRank && order.orderType == orderType.Normal).ToList();
        else if (FinishedNormalOrders == 3)//3つの通常依頼が完了したら特殊依頼を引き受ける
            AcceptableOrders = AllOrders.Where(order => order.rank == WorldRank && order.orderType == orderType.Special).ToList();
    }


    //依頼を受けるメソッド
    public static void AcceptOrder(OrderData order)
    {
        if(CurrentOrder== null)
        {
            CurrentOrder = order;
            AcceptableOrders.Remove(order);
        }
    }

    //依頼を完了させるメソッド
    public static void CompleteOrder()
    {
        AcceptableOrders.Remove(CurrentOrder);
        //CompletedOrders.Add(CurrentOrder);
        //※static型変数はリスト構造内に入れることが出来ない。。。
        if (CurrentOrder.orderType == orderType.Normal)
            FinishedNormalOrders++;
        else if(CurrentOrder.orderType == orderType.Special)
            FinishedSpecialOrders++;
        
        //依頼完了時所持金の処理
        Storage.Gold += CurrentOrder.reward;

        CurrentOrder = null;

        UpdateWorldRank();
    }

    //依頼が正しく完了されたか（要求に合っているか）どうかを確認するメソッド
    public static bool CheckOrder(OrderDetail ordercompleted)
    {
        return false;
    }
 

    //昇級依頼が完了されたら世界ランクを上げる処理
    public static void UpdateWorldRank()
    {
        if (FinishedSpecialOrders == 1)
        {
            WorldRank++;
            FinishedSpecialOrders = 0;
            FinishedNormalOrders = 0;
        }   
    }


       
}

