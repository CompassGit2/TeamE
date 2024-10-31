using UnityEngine;
using UnityEngine.UI;
using Data;
using Data.Database;
using System.Collections.Generic;
using static Unity.Collections.AllocatorManager;

public class IntegratedShopUIManager : MonoBehaviour
{
    [Header("Databases")]
    [SerializeField] private ShopDatabase shopDatabase;
    [SerializeField] private WeaponDatabase weaponDatabase;

    [Header("Containers")]
    [SerializeField] private Transform materialContainer;
    [SerializeField] private Transform weaponContainer;
    [SerializeField] private GridLayoutGroup materialGridLayout;
    [SerializeField] private GridLayoutGroup weaponGridLayout;

    [Header("UI Panels")]
    [SerializeField] private GameObject materialShopPanel;
    [SerializeField] private GameObject weaponShopPanel;
    [SerializeField] private GameObject QuestShopPanel;
    [SerializeField] private GameObject Blocker;
    public GameObject blocker { get => Blocker; }  // 読み取り専用のプロパティを追加


    [Header("Prefabs")]
    [SerializeField] private GameObject itemUIPrefab;
    [SerializeField] private GameObject itemDetailDialogPrefab;
    //[SerializeField] private GameObject warningIconPrefab;

    [Header("UI Elements")]
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject GoldBar;

    private ItemDetailDialog detailDialog;
    private ShopDetail currentMaterialItem;
    private int currentQuantity = 1;

    void Awake()
    {
        closeButton.onClick.AddListener(CloseWindow);
        shopDatabase.Initialize();//データベースの初期化
        //テスト用↓※後で消して！
        Weapon weapon1 = new Weapon(weaponDatabase.GetWeaponData("鉄の剣"), 100, 10, 100);
        Storage.AddWeapon(weapon1);
        Debug.Log("新しい剣が生成されました");
    }

    void OnEnable()
    {
        SetupGridLayouts();

        shopDatabase.UpdateManually();//データベースの更新
        GoldBar.SetActive(true);//ゴールドバーを表示
        Blocker.SetActive(false);

        // プレハブが設定されているか確認
        if (itemDetailDialogPrefab == null)
        {
            Debug.LogError("ItemDetailDialogPrefab がインスペクターで設定されていません");
            return;
        }

        // 詳細ダイアログの初期化
        detailDialog = Instantiate(itemDetailDialogPrefab,transform).GetComponent<ItemDetailDialog>();
        detailDialog.Hide();
        
        // 両方のショップUIを更新
        Debug.Log("素材ショップUIの更新");
        CreateMaterialShopUI();
        Debug.Log("武器ショップUIの更新");
        CreateWeaponShopUI();
    }

    void OnDisable()
    {
        if (detailDialog != null)
        {
            Destroy(detailDialog.gameObject);
            detailDialog = null;
        }
        GoldBar.SetActive(false);
    }

    private void SetupGridLayouts()
    {
        SetupGridLayout(materialGridLayout, materialContainer);
        SetupGridLayout(weaponGridLayout, weaponContainer);
    }

    private void SetupGridLayout(GridLayoutGroup grid, Transform container)
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

    public void CreateMaterialShopUI()
    {
        ClearContainer(materialContainer);
        var shopItems = shopDatabase.GetShopList();
        Debug.Log("素材ショップのアイテム数は"+shopItems.Count);
        foreach (var item in shopItems)
        {
            CreateShopItemUI(item);
        }

        //ゴールドバーを表示
        GoldBar.transform.Find("GoldAmount").GetComponent<Text>().text = Storage.Gold.ToString();
        
    }

 
    public void CreateWeaponShopUI()
    {
        ClearContainer(weaponContainer);
        //テスト用の武器リストを取得
        //Weapon weapon1=new Weapon(weaponDatabase.GetWeaponData("鉄の剣"),100,10,100);
        //Storage.AddWeapon(weapon1);
        List<Weapon> playerWeapons = Storage.Weapons;
        Debug.Log("今所持の剣の数は" + Storage.Weapons.Count);
        foreach (var weapon in playerWeapons)
        {
            CreateWeaponListItem(weapon);
        }

        //ゴールドバーを表示
        GoldBar.transform.Find("GoldAmount").GetComponent<Text>().text = Storage.Gold.ToString();
    }

    private void CreateShopItemUI(ShopDetail item)
    {
        GameObject itemUI = Instantiate(itemUIPrefab, materialContainer);
        
        Text nameText = itemUI.transform.Find("NameText").GetComponent<Text>();
        Text priceText = itemUI.transform.Find("PriceText").GetComponent<Text>();
        Text stockText = itemUI.transform.Find("StockText").GetComponent<Text>();
        Image iconImage = itemUI.transform.Find("Icon").GetComponent<Image>();

        nameText.text = item.material.Name;
        //Debug.Log("今問題が出たアイテムの名前は" + nameText.text);
        priceText.text = item.material.Price.ToString() + "G";
        stockText.text = "在庫数:"+item.amount.ToString();
        iconImage.sprite = item.material.MaterialImage;

        Button button = itemUI.GetComponent<Button>();
        if (button == null) button = itemUI.AddComponent<Button>();
        button.onClick.AddListener(() => ShowItemDetail(item));

        
    }

    private void CreateWeaponListItem(Weapon weapon)
    {
        GameObject weaponUI = Instantiate(itemUIPrefab, weaponContainer);
        
        Text nameText = weaponUI.transform.Find("NameText").GetComponent<Text>();
        Text priceText = weaponUI.transform.Find("PriceText").GetComponent<Text>();
        Text stockText = weaponUI.transform.Find("StockText").GetComponent<Text>();
        Image iconImage = weaponUI.transform.Find("Icon").GetComponent<Image>();

        nameText.text = weapon.weapon.Name;
        priceText.text = $"{weapon.price + weapon.bonus}G";
        //priceText.fontSize = 28;
        stockText.text = string.Empty;
        iconImage.sprite = weapon.weapon.WeaponImage;

        //if (IsWeaponRequiredForCurrentOrder(weapon))
        //{
        //    CreateWarningIcon(weaponUI.transform);
        //}

        Button button = weaponUI.GetComponent<Button>();
        if (button == null) button = weaponUI.AddComponent<Button>();
        button.onClick.AddListener(() => ShowWeaponDetail(weapon));

    }

    /*private void CreateWarningIcon(Transform parent)
    {
        GameObject warningIcon = Instantiate(warningIconPrefab, parent);
        RectTransform warningRect = warningIcon.GetComponent<RectTransform>();
        warningRect.anchoredPosition = new Vector2(-90, 40);
        warningRect.sizeDelta = new Vector2(20, 20);
    }*/

    private void ShowItemDetail(ShopDetail item)
    {
        currentMaterialItem = item;
        detailDialog.Initialize(item);
        detailDialog.ShowMaterialPurchaseMenu();
        detailDialog.Show();
        Blocker.SetActive(true);
    }

    private void ShowWeaponDetail(Weapon weapon)
    {
        detailDialog.Initialize(weapon);
        if (IsWeaponRequiredForCurrentOrder(weapon))
        {
            // 警告メッセージを表示する場合はここで
        }
        detailDialog.ShowWeaponPurchaseMenu();
        detailDialog.Show();
        Blocker.SetActive(true);
    }

    private bool IsWeaponRequiredForCurrentOrder(Weapon weapon)
    {
        OrderData currentOrder = OrderManager.CurrentOrder;
        if (currentOrder == null) return false;

        return currentOrder.RequirementType switch
        {
            requirements.ByName => weapon.weapon.Name == currentOrder.WeaponName,
            requirements.Rarity => weapon.weapon.Rarity == currentOrder.RequiredRarity,
            requirements.SpecSpecifications => false,
            _ => false,
        };
    }

    private void ClearContainer(Transform container)
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }

    private void CloseWindow()
    {
        Debug.Log("Close Window");
        QuestShopPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}