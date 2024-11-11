namespace Data
{
    public class Order
    {
        public OrderData orderData;
        public bool isFinished;

        public Order(OrderData orderData)
        {
            this.orderData = orderData;
            isFinished = false;
        }
    }

}

