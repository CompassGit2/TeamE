using System;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace ShopScene
{
    public class ReportPanel : MonoBehaviour
    {
        public Action<OrderType> ReportOrder;
        [SerializeField] OrderCell orderCell;
        [SerializeField] WeaponGridView weaponGridView;
        [SerializeField] WeaponDeliverPanel weaponDeliverPanel;
        [SerializeField] GameObject NoAcceptableWeaponText;

        List<Weapon> acceptableWeapons;

        int selectedWeaponIndex;

        public void Enable()
        {
            gameObject.SetActive(true);
        }
        public void Disable()
        {
            gameObject.SetActive(false);
        }

        void OnEnable()
        {
            weaponGridView.OnCellClicked(index => SetDeliverPanelActive(index, acceptableWeapons[index]));
            weaponDeliverPanel.DeliverWeapon = DeliverWeapon;
            orderCell.gameObject.SetActive(true);
            weaponGridView.gameObject.SetActive(false);
            NoAcceptableWeaponText.SetActive(false);
            orderCell.UpdateContent(Storage.AcceptedOrder);
        }

        public void OnClickSelectWeaponButton()
        {
            orderCell.gameObject.SetActive(false);
            weaponGridView.gameObject.SetActive(true);
            GenerateWeaponCells();
            
        }

        public void OnClickBackButton()
        {
            weaponGridView.gameObject.SetActive(false);
            orderCell.gameObject.SetActive(true);
        }

        void GenerateWeaponCells()
        {
            acceptableWeapons = new();
            if(Storage.AcceptedOrder.orderData.OrderType == OrderType.Special)
            {
                if(Storage.Pickaxe != null)
                {
                    if(CheckWeaponRequirements(Storage.Pickaxe.weapon))
                    {
                        acceptableWeapons.Add(Storage.Pickaxe);
                    }
                }
            }
            else
            {
                foreach(Weapon weapon in Storage.Weapons)
                {
                    if(CheckWeaponRequirements(weapon.weapon))
                    {
                        acceptableWeapons.Add(weapon);
                    }
                }
            }
            weaponGridView.UpdateContents(acceptableWeapons);
            if(acceptableWeapons.Count <= 0)
            {
                NoAcceptableWeaponText.SetActive(true);
            }
            else
            {
                NoAcceptableWeaponText.SetActive(false);
            }
        }

        public bool CheckWeaponRequirements(WeaponData weapon)
        {
            OrderData orderData = Storage.AcceptedOrder.orderData;

            if(orderData.OrderType == OrderType.Special)
            {
                if(weapon.Name == orderData.weaponData.Name) return true;
                
                return false;
            }

            switch(orderData.RequirementType){
                case Requirements.ByData:
                    return weapon.Name == orderData.weaponData.Name;

                case Requirements.SpecSpecifications:
                    if (orderData.specRequirements.checkLength && weapon.Length < orderData.specRequirements.requiredLength)
                    return false;
                    
                    if (orderData.specRequirements.checkWeight && weapon.Weight < orderData.specRequirements.requiredWeight)
                    return false;
                                        
                    if (orderData.specRequirements.checkSharpness && weapon.Sharpness < orderData.specRequirements.requiredSharpness)
                    return false;
                
                    return true;
                    
                case Requirements.Rarity:
                    return weapon.Rarity >= orderData.RequiredRarity;
                    
                default:
                    return false;
            }
        }

        void SetDeliverPanelActive(int index, Weapon weapon)
        {
            selectedWeaponIndex = index;
            weaponDeliverPanel.Enable(weapon);
        }

        void DeliverWeapon()
        {
            OrderType orderType = Storage.AcceptedOrder.orderData.OrderType;
            Storage.Gold += acceptableWeapons[selectedWeaponIndex].price + Storage.AcceptedOrder.orderData.Reward;
            
            if(orderType == OrderType.Normal)
            Storage.RemoveWeapon(acceptableWeapons[selectedWeaponIndex]);
            
            Storage.SetOrderFinished(Storage.AcceptedOrder);
            weaponDeliverPanel.Disable();
            ReportOrder(orderType);
        }
    }

}
