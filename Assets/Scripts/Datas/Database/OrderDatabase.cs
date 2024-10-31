using System.Collections.Generic;
using UnityEngine;

namespace Data.Database
{
    [CreateAssetMenu(menuName = "Database/OrderDatabase")]
    public class OrderDatabase : ScriptableObject
    {
        public List<OrderData> orderList = new List<OrderData>();

        public List<OrderData> GetOrderList()
        {
            return orderList;
        }

    }
}