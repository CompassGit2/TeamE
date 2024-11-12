using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace MenuScene
{
    public class MaterialDictionaryPanel : MonoBehaviour
    {
        [SerializeField] MaterialDictionaryGridView gridView;
        [SerializeField] MaterialDetailPanel materialDetailPanel;

        void OnEnable()
        {
            materialDetailPanel.Disable();
            gridView.OnCellClicked(index => SetMaterialToRightPage(ItemDictionary.MaterialDictionary[index]));
            GenerateCells();
        }

        void OnDisable()
        {
            materialDetailPanel.Disable();
        }

        void GenerateCells()
        {
            gridView.UpdateContents(ItemDictionary.MaterialDictionary);
        }

        void SetMaterialToRightPage(MaterialDictionaryData materialDictionaryData)
        {
            materialDetailPanel.Enable(materialDictionaryData);
        } 
    }

}
