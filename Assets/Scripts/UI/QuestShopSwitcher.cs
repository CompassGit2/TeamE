using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class QuestShopSwitcher : MonoBehaviour
{
	public GameObject QuestShopPanel;
	public GameObject Questspace;
	public GameObject Shopspace;

	void OnEnable()
	{
       
    }

	public void SwitchToQuest()
	{
		QuestShopPanel.SetActive(false);
		Shopspace.SetActive(false);
		Questspace.SetActive(true);
	}

	public void SwitchToShop()
	{
        QuestShopPanel.SetActive(false);
        Questspace.SetActive(false);
		Shopspace.SetActive(true);
	}

}

