namespace Data
{
    [System.Serializable]
    public class OrderDetail
    {
        public OrderData FinishedOrder;
        public Weapon CraftedWeapon;

        public OrderDetail(OrderData _order, Weapon _weapon)
        {
            this.FinishedOrder=_order;
            this.CraftedWeapon=_weapon;
        }
    }
    
}
