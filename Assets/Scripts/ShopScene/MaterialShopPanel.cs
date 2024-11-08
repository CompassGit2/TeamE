using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace ShopScene
{
    public class MaterialShopPanel : MonoBehaviour
    {
        [SerializeField] MaterialGridView materialGridView;

        [SerializeField] BuyPanel buyPanel;

        int selectedMaterialIndex;

        void OnEnable()
        {
            buyPanel.BuyMaterial = BuyItem;
            materialGridView.OnCellClicked(index => SetBuyPanelActive(index, Storage.ShopMaterials[index]));
            
            GenerateCells(Storage.ShopMaterials);

        }

        void GenerateCells(List<MaterialStack> materialStacks)
        {
            materialGridView.UpdateContents(materialStacks);
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
            materialGridView.UpdateContents(Storage.ShopMaterials);
        }
    }

}
