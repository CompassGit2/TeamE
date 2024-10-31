using System.Collections.Generic;
using Data;
using UnityEngine;
using ResultScene;

public class ResultItemDisplay : MonoBehaviour
{
    List<MaterialStack> getMaterialStacks;

    [SerializeField] GameObject cellPrefab;
    void Start()
    {
        getMaterialStacks = GetItems.MaterialStacks;
        GenerateItemInfo();
        GetItems.RemoveItems();
    }

    public void GenerateItemInfo()
    {
        foreach(MaterialStack m in getMaterialStacks)
        {
            GameObject cellObj = Instantiate(cellPrefab, this.transform);
            Cell cell = cellObj.GetComponent<Cell>();
            cell.SetItemData(m.material.MaterialImage, m.amount, m.material.Name);
        }
    }
}
