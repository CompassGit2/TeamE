using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace ShopScene
{
    public class QuestPanel : MonoBehaviour
    {
        [SerializeField] ReceptionPanel receptionPanel;
        [SerializeField] ReportPanel reportPanel;

        void OnEnable()
        {
            receptionPanel.AcceptOrder = SetReportPanelActive;
            reportPanel.ReportOrder = SetReceptionPanelActive;
            
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
    }

}
