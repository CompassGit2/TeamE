using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonUI;
using Data;

namespace MenuScene
{
    public class RecipeDictionaryGridView : GridView<RecipeDictionaryCell, RecipeDictionaryData>
    {
        new class CellGroup : DefaultCellGroup { }

        protected override void SetupCellTemplate() 
        {            
            Setup<CellGroup>(cellPrefab);
        }
    }

}
