namespace Data
{
    [System.Serializable]
    public class OrderDetail
    {
        //ここからが問題
        //1.武器名による直接指名
        //2.武器スペックに対して要求を出す
        //3.レア度に対して要求を出す
        public OrderData Order;

        public OrderDetail(OrderData orderData)
        {
            this.Order = orderData;
        }

    }
    

}
