using Data;
using CommonUI;

namespace ShopScene
{
    public class MaterialGridView : GridView<MaterialCell, MaterialStack>
    {
        new class CellGroup : DefaultCellGroup { }
        protected override void SetupCellTemplate() 
        {            
            Setup<CellGroup>(cellPrefab);
        }
    }
}
