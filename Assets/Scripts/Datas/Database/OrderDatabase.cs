using System.Collections.Generic;
using UnityEngine;

namespace Data.Database
{
    [CreateAssetMenu(menuName = "Database/OrderDatabase")]
    public class OrderDatabase : ScriptableObject
    {
        public List<OrderData> normalOrderList = new List<OrderData>();
        public List<OrderData> specialOrderList = new List<OrderData>();

        public List<OrderData> GetOrderList()
        {
            return normalOrderList;
        }

        public List<OrderData> GetNormalOrdersByRank(int rank)
        {
            List<OrderData> foundOrders = new();
            foreach (OrderData order in normalOrderList)
            {
                if(order.Rank == rank)
                {
                    foundOrders.Add(order);
                }
            }

            return foundOrders;
        }

        public OrderData GetSpecialOrderByRank(int rank)
        {
            return specialOrderList[rank];
        }

    }
}