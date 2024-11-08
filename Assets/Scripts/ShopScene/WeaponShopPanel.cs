using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace ShopScene
{
    public class WeaponShopPanel : MonoBehaviour
    {
        [SerializeField] WeaponGridView weaponGridView;
        [SerializeField] WeaponSellPanel weaponSellPanel;

        int selectedWeaponIndex;

        void OnEnable()
        {
            weaponSellPanel.SellWeapon = SellWeapon;
            weaponGridView.OnCellClicked(index => SetSellPanelActive(index, Storage.Weapons[index]));

            GenerateCells(Storage.Weapons);
        }

        void GenerateCells(List<Weapon> weapons)
        {
            weaponGridView.UpdateContents(weapons);
        }

        void SetSellPanelActive(int index, Weapon weapon)
        {
            selectedWeaponIndex = index;
            weaponSellPanel.Enable(weapon);
        }

        void SellWeapon()
        {
            Weapon weapon = Storage.Weapons[selectedWeaponIndex];
            Storage.Gold += weapon.price;
            Storage.RemoveWeapon(weapon);
            weaponSellPanel.Disable();
            weaponGridView.UpdateContents(Storage.Weapons);
        }
        
    }

}
