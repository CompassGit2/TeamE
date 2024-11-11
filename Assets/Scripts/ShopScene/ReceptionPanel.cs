using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

namespace ShopScene
{
    public class ReceptionPanel : MonoBehaviour
    {
        public Action AcceptOrder;
        [SerializeField] OrderGridView gridView;
        [SerializeField] GameObject acceptOrderPanel;
        [SerializeField] GameObject noAcceptableQuestText;
        List<Order> acceptableOrders;
        Order selectedOrder;

        
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
            acceptableOrders = Storage.Orders.Where(i => i.isFinished == false).ToList();
            gridView.OnCellClicked(index => SetAcceptPanelActive(acceptableOrders[index]));
            GenerateCells();
        }

        void GenerateCells()
        {
            gridView.UpdateContents(acceptableOrders);
            
            if(acceptableOrders.Count <= 0)
            {
                noAcceptableQuestText.SetActive(true);
            }
            else
            {
                noAcceptableQuestText.SetActive(false);
            }
        }

        void SetAcceptPanelActive(Order order)
        {
            selectedOrder = order;
            acceptOrderPanel.SetActive(true);
        }

        public void OnClickAcceptOrderButton()
        {
            Storage.SetOrderAccept(selectedOrder);
            acceptOrderPanel.SetActive(false);
            AcceptOrder();
        }
    }

}
