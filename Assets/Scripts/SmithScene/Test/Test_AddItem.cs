using System.Collections;
using System.Collections.Generic;
using Data;
using Data.Database;
using UnityEngine;

public class Test_AddItem : MonoBehaviour
{
    [SerializeField] MaterialDatabase materialDatabase;
    public void AddDummyItem()
    {
        foreach(MaterialStack i in GenerateMaterial())
        {
            Storage.AddItem(i.material, i.amount);
        }
    }

    List<MaterialStack> GenerateMaterial()
    {
        List<MaterialStack> dummyItems = new List<MaterialStack>();
        for (int i = 0; i < 5; i++)
        {
            MaterialStack dummyItem = new MaterialStack(materialDatabase.GetMaterialList()[i],10);
            dummyItems.Add(dummyItem);
        }
        return dummyItems;
    }
}
