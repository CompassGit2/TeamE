using UnityEngine;
using UnityEngine.UI;
using Data;

public class OrderDetailDialog : MonoBehaviour
{
    [Header("UI References")]
     private Text weaponTitleText;
     private Text rewardText;
    private Image weaponIcon;
    private Text descriptionText;
    private Button cancelButton;
    private Button completeButton;
    private GameObject completionPanel;

    private OrderData currentOrder;
    private Weapon selectedWeapon;
    private OrderUIManager orderUIManager;

    void Awake()//OnEnable?
    {
        orderUIManager = FindObjectOfType<OrderUIManager>();
        cancelButton.onClick.AddListener(OnCancelButtonClicked);
        completeButton.onClick.AddListener(OnCompleteButtonClicked);
        
        //参照を引っ張ってくる
        weaponIcon=transform.Find("WeaponIcon").GetComponent<Image>();
        rewardText=transform.Find("PriceText").GetComponent<Text>();
        weaponTitleText = transform.Find("WeaponTitle").GetComponent<Text>();
        descriptionText=transform.Find("WeaponDescription").GetComponent<Text>();
        cancelButton = transform.Find("CancelButton").GetComponent<Button>();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Initialize(Weapon weapon)
    {
        currentOrder=orderUIManager.CurrentOrder;
        selectedWeapon = weapon;
    }

    public void ShowOrderCompletionDialog()
    {
        // UI更新
        weaponTitleText.text = $"{selectedWeapon.weapon.Name}を納品しますか？";
        rewardText.text = $"{selectedWeapon.weapon.BasePrice + selectedWeapon.bonus + currentOrder.reward}G取得可能";
        descriptionText.text = selectedWeapon.weapon.Description;
        weaponIcon.sprite = selectedWeapon.weapon.WeaponImage;

        Show();
    }

 

    private void OnCompleteButtonClicked()
    {
        if (currentOrder != null && selectedWeapon != null)
        {
            // 依頼完了処理
            OrderDetail completedOrder = new OrderDetail(currentOrder, selectedWeapon);
            Storage.RemoveWeapon(selectedWeapon); // 武器を消費
            Storage.Gold += selectedWeapon.weapon.BasePrice + selectedWeapon.bonus + currentOrder.reward; // 報酬を付与
            OrderManager.CompleteOrder(); // 依頼を完了状態に
            
            Hide();
            orderUIManager.ShowOrderSelection();
        }
        //初期化
        currentOrder=null;
        selectedWeapon=null;
    }

    private void OnCancelButtonClicked()
    {
        Hide();
    }
}