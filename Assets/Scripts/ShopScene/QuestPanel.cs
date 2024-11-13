using System.Collections;
using System.Collections.Generic;
using Data;
using Data.Database;
using UnityEngine;

namespace ShopScene
{
    public class QuestPanel : MonoBehaviour
    {
        [SerializeField] OrderDatabase orderDatabase;
        [SerializeField] ReceptionPanel receptionPanel;
        [SerializeField] ReportPanel reportPanel;
        [SerializeField] ShopItemTable shopItemTable;

        void OnEnable()
        {
            receptionPanel.AcceptOrder = SetReportPanelActive;
            reportPanel.ReportOrder = OnOrderReported;
            
            if(Storage.AcceptedOrder == null)
            {
                SetReceptionPanelActive();
            }
            else
            {
                SetReportPanelActive();
            }
        }

        void SetReceptionPanelActive()
        {
            receptionPanel.Enable();
            reportPanel.Disable();
        }
        
        void SetReportPanelActive()
        {
            receptionPanel.Disable();
            reportPanel.Enable();
        }

        void OnOrderReported(OrderType orderType)
        {
            CheckNextOrderTable(orderType);
            SetReceptionPanelActive();
        }

        void CheckNextOrderTable(OrderType orderType)
        {
            if(orderType == OrderType.Normal)
            {
                if(Storage.GetNotFinishedOrder().Count <= 0)
                {
                    Storage.AddOrderData(orderDatabase.GetSpecialOrderByRank(PlayerData.WorldRank));
                }
            }
            else if(orderType == OrderType.Special)
            {
                PlayerData.SetWorldRank(PlayerData.WorldRank+1);
                foreach(OrderData orderData in orderDatabase.GetNormalOrdersByRank(PlayerData.WorldRank))
                {
                    Storage.AddOrderData(orderData);
                }
                shopItemTable.AddShopItem();
            }
        }
    }

}
