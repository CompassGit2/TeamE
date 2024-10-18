using Data;
using System.Collections.Generic;

public static class OrderManager
{
    public static List<OrderDetail> orders = new List<OrderDetail>();
    public static List<OrderDetail> fetchingOrders = new List<OrderDetail>();//今の世界レア度
    public static int WorldRank = 0;//世界ランク　ピッケルのランクでもある
    //引き受け可能な依頼
    //今引き受けている依頼
    //に分けとく必要あり

        public static void AddOrder(OrderData order)
        {
            orders.Add(new OrderDetail(order));
        }

        public static void RemoveOrder(OrderDetail order)
        {
            orders.Remove(order);
        }

        public static void FetchOrders()
        {
            //現存している取得可能な依頼を探す
        }

        


}

