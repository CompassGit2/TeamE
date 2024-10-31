using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Data;
using Data.Database;
using Unity.VisualScripting;

public class OrderUIManager : MonoBehaviour
{
    [Header("Databases")]
    [SerializeField] private OrderDatabase orderDatabase;

    [Header("Containers")]
    [SerializeField] private Transform orderContainer;
    [SerializeField] private Transform weaponContainer;
    [SerializeField] private GridLayoutGroup orderGridLayout;
    [SerializeField] private GridLayoutGroup weaponGridLayout;

    [Header("UI Panels")]
    [SerializeField] private GameObject orderSelectionPanel;
    [SerializeField] private GameObject weaponSelectionPanel;
    [SerializeField] private GameObject QuestShopPanel;
    
    [Header("CurrentOrder Status")]
    [SerializeField] private Image currentOrderStatus;

    [Header("Prefabs")]
    [SerializeField] private GameObject orderUIPrefab;
    [SerializeField] private GameObject weaponUIPrefab;

    [Header("Buttons")]
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    private OrderData currentOrder;//進行中の依頼
    public OrderData CurrentOrder  => currentOrder;

    private OrderData SelectedOrder;// 選択中の依頼UI
    private bool onlyOneHighlight=false;//ハイライトは一個しかないようにするためのフラグ
    private List<Weapon> VerifiedWeapons = new List<Weapon>();// 納品可能な武器リスト
    private OrderDetailDialog orderDetailDialog;

    void Awake()
    {
        SetupGridLayouts();
        //confirmButton.onClick.AddListener(ConfirmOrder);//OKボタンにイベントを設定
        //最初、非表示の状態ではイベントが呼び出されないようにしとく。
        //何かraycast targetで制御することも出来そうだけど、とりあえずこれで。
        cancelButton.onClick.AddListener(CloseWindow);
        currentOrderStatus = QuestShopPanel.transform.Find("QuestButton/CurrentOrderStatus").GetComponent<Image>();

    }


    void OnDisable()
    {
        if (orderDetailDialog != null)
        {
            Destroy(orderDetailDialog.gameObject);
            orderDetailDialog = null;
        }
    }
    private void ConfirmOrder()
    {
        // 保険として、選択中の依頼がない場合は何もしない
        if (SelectedOrder == null) return;
        // 依頼を受け取る
        OrderManager.AcceptOrder(SelectedOrder);
        SelectedOrder = null;
        Debug.Log("今依頼を受注した");
        RefreshOrderList();
    }

    private void CheckVerifiedWeapon()
    {
        Debug.Log("今何本の剣を持っているかをチェック"+Storage.Weapons.Count);
        // 今持っている武器の中でが納品可能な武器があるかをかチェック
        foreach(var sword in Storage.Weapons)
        {
            if (IsWeaponValidForCurrentOrder(sword))
            {
                VerifiedWeapons.Add(sword);
                Debug.Log("鉄の剣を納品可能リストに追加");
                Debug.Log(VerifiedWeapons.Count);
            }
                
        }
    }

    void OnEnable()
    {       
        RefreshOrderList();
        // 依頼選択ボタンの色を変更
       ConfirmButtonInvisiable();
    }


    private void SetupGridLayouts()
    {
        // 依頼表示用のレイアウト設定
        if (orderGridLayout == null)
        {
            orderGridLayout = orderContainer.GetComponent<GridLayoutGroup>();
            if (orderGridLayout == null)
            {
                orderGridLayout = orderContainer.gameObject.AddComponent<GridLayoutGroup>();
                // 固定サイズで3つの依頼が横に並ぶように設定
                orderGridLayout.cellSize = new Vector2(380, 200); // より大きなサイズに調整
                orderGridLayout.spacing = new Vector2(20, 0); // 横方向のスペースのみ
                orderGridLayout.startCorner = GridLayoutGroup.Corner.UpperLeft;
                orderGridLayout.startAxis = GridLayoutGroup.Axis.Horizontal;
                orderGridLayout.childAlignment = TextAnchor.MiddleCenter; // 中央寄せ
                orderGridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                orderGridLayout.constraintCount = 3;
            }
        }

        // 武器選択用のレイアウトは従来通りScrollViewを使用
        SetupWeaponGridLayout(weaponGridLayout, weaponContainer);
    }

    private void SetupWeaponGridLayout(GridLayoutGroup grid, Transform container)
    {
        if (grid == null)
        {
            grid = container.GetComponent<GridLayoutGroup>();
            if (grid == null)
            {
                grid = container.gameObject.AddComponent<GridLayoutGroup>();
                grid.cellSize = new Vector2(220, 300);
                grid.spacing = new Vector2(10, 10);
                grid.startCorner = GridLayoutGroup.Corner.UpperLeft;
                grid.startAxis = GridLayoutGroup.Axis.Horizontal;
                grid.childAlignment = TextAnchor.UpperLeft;
                grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                grid.constraintCount = 5;
            }
        }
    }

    public void RefreshOrderList()
    {
        ClearContainer(orderContainer);
        
        // 現在受注中の依頼がある場合は表示しない
        if (OrderManager.CurrentOrder != null)
        {
            Debug.Log("現在受注中の依頼があります、完了報告後に新しい依頼を受けられます。");
            ShowCurrentOrderStatus();
            CheckVerifiedWeapon();
            if (VerifiedWeapons.Count > 0)
            {
                //依頼の要求にあっている剣を所持しているのであれば、武器納品用選択UIを表示
                ShowWeaponSelectionUI();
            }

            CloseWindow();
            //return;
        }

        if (OrderManager.CurrentOrder == null)
        {
            OrderManager.FetchOrdersByRank();//受付可能な依頼をOrderManager側で更新
        }
        
        var availableOrders = OrderManager.AcceptableOrders;
        //Debug.Log(availableOrders.Count);
        foreach (var order in availableOrders)
        {
            CreateOrderUI(order);
        }
    }

    private void ShowCurrentOrderStatus()
    {
        // メッセージを表示するUIを作成
        currentOrderStatus.transform.Find("StatusText").GetComponent<Text>().text = "現在受注中の依頼があります、完了報告後に新しい依頼を受けられます。";
        currentOrderStatus.gameObject.SetActive(true);
    }

    private void CreateOrderUI(OrderData order)
    {
        GameObject orderUI = Instantiate(orderUIPrefab, orderContainer);
        
        // 基本情報の設定
        Text orderTypeText = orderUI.transform.Find("OrderTypeText").GetComponent<Text>();
        //Text rewardText = orderUI.transform.Find("RewardText").GetComponent<Text>();
        Text descriptionText = orderUI.transform.Find("DescriptionText").GetComponent<Text>();
        Image orderIcon = orderUI.transform.Find("OrderIcon").GetComponent<Image>();
        Text requirementTypeText=orderUI.transform.Find("RequirementTypeText").GetComponent<Text>();

        orderTypeText.text = order.orderType == orderType.Normal ? "通常依頼" : "昇級依頼";
        //rewardText.text = $"{order.reward}G";
        descriptionText.text = order.description;
        orderIcon.sprite = order.orderIcon;
        //requirementTypeText.text=order.RequirementType.ToString();
        requirementTypeText.text = order.RequirementType switch
        {
            requirements.ByName => $"名前指定：{order.WeaponName}",
            requirements.Rarity => $"レア度指定：{order.RequiredRarity}",
            requirements.SpecSpecifications => $"スペック指定",
            //$"長さ指定:{order.Requirements.requiredLength} " +
            //$"鋭さ指定：{order.Requirements.requiredSharpness}  " +
            //$"重量指定：{order.Requirements.requiredWeight}",
            _ => "不明な要求"
        };
        Button button = orderUI.GetComponent<Button>();
        
        //button.onClick.AddListener(() => ShowOrderDetail(order));
        //ダイアログ画面を廃止して選択した依頼のみをハイライトにする

        //まだどの依頼も選択していない時点で、OKボタンにイベントを追加する必要はない
        button.onClick.AddListener(()=> SelectCurrentOrder(order));
        button.onClick.AddListener(()=> ShowHighlightEdge(orderUI));
    }

    public void SelectCurrentOrder(OrderData order)
    {
        if (SelectedOrder != null && SelectedOrder != order)
            return;

        if(SelectedOrder == order)
        {
            SelectedOrder = null;
            ConfirmButtonInvisiable();
            confirmButton.onClick.RemoveListener(ConfirmOrder);
        }
        else
        {
            SelectedOrder = order;
            ConfirmButtonVisible();//OKボタンを表示
            confirmButton.onClick.AddListener(ConfirmOrder);//OKボタンに依頼受付イベントを設定
        }
        
    }

    public void ShowHighlightEdge(GameObject OrderUI)
    {
        //選択した依頼のUIにハイライトエッジを表示
        //一回目のクリックでハイライトエッジを表示、二回目のクリックで非表示にする

        if (onlyOneHighlight&&!OrderUI.transform.Find("HighlightEdge").gameObject.activeSelf)
            return;

        if (OrderUI.transform.Find("HighlightEdge").gameObject.activeSelf)
        {
            OrderUI.transform.Find("HighlightEdge").gameObject.SetActive(false);
            onlyOneHighlight = false;
        }

        else
        {
            OrderUI.transform.Find("HighlightEdge").gameObject.SetActive(true);
            onlyOneHighlight = true;
        }
            
    }

    public void ConfirmButtonInvisiable()
    {
        Color buttonColor = confirmButton.image.color;
        Color textColor = confirmButton.GetComponentInChildren<Text>().color;

        //透明にする（非表示にする）
        buttonColor.a = 0.3f;
        textColor.a = 0.3f;

        confirmButton.image.color = buttonColor;
        confirmButton.GetComponentInChildren<Text>().color = textColor;
    }

    public void ConfirmButtonVisible()
    {
        Color buttonColor = confirmButton.image.color;
        Color textColor = confirmButton.GetComponentInChildren<Text>().color;

        //不透明にする（出現させる）
        buttonColor.a = 1.0f;
        textColor.a = 1.0f;

        confirmButton.image.color = buttonColor;
        confirmButton.GetComponentInChildren<Text>().color = textColor;
    }

    public void ShowWeaponSelectionUI()
    {
        ClearContainer(weaponContainer);
        orderSelectionPanel.SetActive(false);
        weaponSelectionPanel.SetActive(true);

        //var playerWeapons = Storage.Weapons;
        foreach (var weapon in VerifiedWeapons )
        {
                CreateWeaponSelectionUI(weapon);
        }
    }


    private void CreateWeaponSelectionUI(Weapon weapon)
    {
        GameObject weaponUI = Instantiate(weaponUIPrefab, weaponContainer);
        
        Text nameText = weaponUI.transform.Find("NameText").GetComponent<Text>();
        Image weaponIcon = weaponUI.transform.Find("Icon").GetComponent<Image>();

        nameText.text = weapon.weapon.Name;
        weaponIcon.sprite = weapon.weapon.WeaponImage;

        Button button = weaponUI.GetComponent<Button>();
        button.onClick.AddListener(() => OnWeaponSelected(weapon));
    }

    private bool IsWeaponValidForCurrentOrder(Weapon weapon)
    {
        OrderData order = OrderManager.CurrentOrder;
        if (order == null) return false;

        return order.RequirementType switch
        {
            requirements.ByName => weapon.weapon.Name == order.WeaponName,
            requirements.Rarity => weapon.weapon.Rarity == order.RequiredRarity,
            requirements.SpecSpecifications => 
                weapon.weapon.Weight >= order.Requirements.requiredWeight &&
                weapon.weapon.Length >=order.Requirements.requiredLength&&
                weapon.weapon.Sharpness >= order.Requirements.requiredSharpness,
            _ => false
        };
    }

    



    private void OnWeaponSelected(Weapon weapon)
    {
        // 完了報告の確認ダイアログを表示
        orderDetailDialog.Initialize(weapon);
        orderDetailDialog.ShowOrderCompletionDialog();
        orderDetailDialog.Show();
    }

    private void ClearContainer(Transform container)
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }

    public void ShowOrderSelection()
    {
        orderSelectionPanel.SetActive(true);
        weaponSelectionPanel.SetActive(false);
        RefreshOrderList();
    }


    private void CloseWindow()
    {
        Debug.Log("CloseWindowが実行されているはず");
        QuestShopPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}

