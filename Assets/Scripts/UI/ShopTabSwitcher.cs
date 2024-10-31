using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class ShopTabSwitcher : MonoBehaviour
{

    [SerializeField] private Toggle MateriaTab;
    [SerializeField] private Toggle SwordTab;

    public void SwitchtoMaterial()
    {
        MateriaTab.isOn = true;
        //SwordRibbon.isOn = false;
    }

    public void SwitchtoSword()
    {
        SwordTab.isOn = true;
    }

}