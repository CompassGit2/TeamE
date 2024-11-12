using CommonUI;
using Data;

namespace MenuScene
{
    public class MaterialDictionaryGridView : GridView<MaterialDictionaryCell, MaterialDictionaryData>
    {
        new class CellGroup : DefaultCellGroup { }

        protected override void SetupCellTemplate() 
        {            
            Setup<CellGroup>(cellPrefab);
        }


    }

}
