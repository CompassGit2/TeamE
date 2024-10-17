using UnityEngine;
using System.Collections.Generic;

namespace Data.Database
{
    [CreateAssetMenu(menuName = "Database/MaterialDatabase")]
    public class MaterialDatabase : ScriptableObject
    {
        public List<MaterialData> materialList = new List<MaterialData>();

        public List<MaterialData> GetMaterialList()
        {
            return materialList;
        }
    }

}
