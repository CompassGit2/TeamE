using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace ShopScene
{
    public class ShopPanel : MonoBehaviour
    {
        [SerializeField] MaterialGridView gridView;

        [SerializeField] BuyPanel buyPanel;

        int selectedMaterialIndex;

        void OnEnable()
        {
            buyPanel.BuyMaterial = BuyItem;
            gridView.OnCellClicked(index => SetBuyPanelActive(index, Storage.ShopMaterials[index]));
            
            GenerateCells(Storage.ShopMaterials);

        }

        void GenerateCells(List<MaterialStack> materialStacks)
        {
            gridView.UpdateContents(materialStacks);
        }

        void SetBuyPanelActive(int index, MaterialStack materialStack)
        {
            selectedMaterialIndex = index;
            buyPanel.Enable(materialStack);            
        }

        void BuyItem(int amount)
        {
            MaterialData selectedMaterial = Storage.ShopMaterials[selectedMaterialIndex].material;
            Storage.AddMaterial(selectedMaterial, amount);
            Storage.Gold -= selectedMaterial.Price * amount;
            Storage.RemoveShopMaterial(selectedMaterial, amount);
            buyPanel.Disable();
            gridView.UpdateContents(Storage.ShopMaterials);
        }
    }

}
