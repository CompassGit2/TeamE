using UnityEngine;
using UnityEngine.UI;

public class TabButtonController : MonoBehaviour
{
    [Header("Toggles")]
    [SerializeField] private Toggle materialToggle;
    [SerializeField] private Toggle weaponToggle;

    [Header("Panels")]
    [SerializeField] private GameObject materialShopPanel;
    [SerializeField] private GameObject weaponShopPanel;

    private void Start()
    {
        
        materialToggle.onValueChanged.AddListener(OnMaterialTabChanged);
        weaponToggle.onValueChanged.AddListener(OnWeaponTabChanged);

        materialToggle.isOn = true;
        UpdatePanels(true);
    }

    private void OnMaterialTabChanged(bool isOn)
    {
        if (isOn)
        {
            UpdatePanels(true);
        }
    }

    private void OnWeaponTabChanged(bool isOn)
    {
        if (isOn)
        {
            UpdatePanels(false);
        }
    }

    private void UpdatePanels(bool isMaterialShop)
    {
        materialShopPanel.SetActive(isMaterialShop);
        weaponShopPanel.SetActive(!isMaterialShop);
    }
}