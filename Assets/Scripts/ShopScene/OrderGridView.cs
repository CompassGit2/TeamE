using Data;
using CommonUI;

namespace ShopScene
{
    public class OrderGridView : GridView<OrderCell, Order>
    {
        new class CellGroup : DefaultCellGroup { }

        protected override void SetupCellTemplate() => Setup<CellGroup>(cellPrefab);
    }

}
