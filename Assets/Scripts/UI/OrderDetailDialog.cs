using UnityEngine;
using UnityEngine.UI;
using Data;

public class OrderDetailDialog : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Text orderTitleText;
    [SerializeField] private Text orderTypeText;
    [SerializeField] private Text rewardText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text requirementText;
    [SerializeField] private Image orderIcon;
    [SerializeField] private Button acceptButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button completeButton;
    [SerializeField] private GameObject acceptPanel;
    [SerializeField] private GameObject completionPanel;

    private OrderData currentOrder;
    private Weapon selectedWeapon;
    private OrderUIManager orderUIManager;

    void Awake()
    {
        orderUIManager = FindObjectOfType<OrderUIManager>();
        
        acceptButton.onClick.AddListener(OnAcceptButtonClicked);
        cancelButton.onClick.AddListener(OnCancelButtonClicked);
        completeButton.onClick.AddListener(OnCompleteButtonClicked);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Initialize(OrderData order)
    {
        currentOrder = order;
        selectedWeapon = null;

        // UI更新
        orderTitleText.text = order.name;
        orderTypeText.text = order.orderType == orderType.Normal ? "通常依頼" : "昇級依頼";
        rewardText.text = $"{order.reward}G";
        descriptionText.text = order.description;
        orderIcon.sprite = order.orderIcon;

        // 要求内容の表示
        string reqText = "要求内容:\n";
        switch (order.RequirementType)
        {
            case requirements.ByName:
                reqText += $"武器名: {order.WeaponName}";
                break;
            case requirements.Rarity:
                reqText += $"レアリティ: {order.RequiredRarity}以上";
                break;
            case requirements.SpecSpecifications:
                reqText += $"長さ: {order.Requirements.requiredLength}以上\n";
                reqText += $"重さ: {order.Requirements.requiredWeight}以上\n";
                reqText += $"鋭さ: {order.Requirements.requiredSharpness}以上";
                break;
        }
        requirementText.text = reqText;

        acceptPanel.SetActive(true);
        completionPanel.SetActive(false);
    }

    public void ShowCompletionDialog(Weapon weapon)
    {
        selectedWeapon = weapon;
        currentOrder = OrderManager.CurrentOrder;

        // UI更新
        orderTitleText.text = "依頼完了報告";
        descriptionText.text = $"選択された武器:\n{weapon.weapon.Name}\n\n本当にこの武器で依頼を完了しますか？";
        
        acceptPanel.SetActive(false);
        completionPanel.SetActive(true);
        Show();
    }

    private void OnAcceptButtonClicked()
    {
        OrderManager.AcceptOrder(currentOrder);
        Hide();
        orderUIManager.RefreshOrderList();
    }

    private void OnCompleteButtonClicked()
    {
        if (currentOrder != null && selectedWeapon != null)
        {
            // 依頼完了処理
            OrderDetail completedOrder = new OrderDetail(currentOrder, selectedWeapon);
            Storage.RemoveWeapon(selectedWeapon); // 武器を消費
            Storage.Gold += currentOrder.reward; // 報酬を付与
            OrderManager.CompleteOrder(); // 依頼を完了状態に
            
            Hide();
            orderUIManager.ShowOrderSelection();
        }
    }

    private void OnCancelButtonClicked()
    {
        Hide();
    }
}