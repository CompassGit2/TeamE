using Data;
using CommonUI;

namespace ShopScene
{
    public class WeaponGridView : GridView<WeaponCell,Weapon>
    {
        new class CellGroup : DefaultCellGroup { }
        protected override void SetupCellTemplate() 
        {            
            Setup<CellGroup>(cellPrefab);
        }
    }

}
