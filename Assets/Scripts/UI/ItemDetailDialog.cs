using UnityEngine;
using UnityEngine.UI;
using Data;
using TMPro;


public class ItemDetailDialog : MonoBehaviour
{
    //[SerializeField]
    private IntegratedShopUIManager integratedShopUIManager;

    [Header("UI References")]
    private Image ownEdgeImage;
    [SerializeField] private TextMeshProUGUI materialTitle;
    [SerializeField] private TextMeshProUGUI materialDescription;
    [SerializeField] private Image materialImage;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private TextMeshProUGUI sliderText;

    [SerializeField] private GameObject materialPropertiesObj;
    [SerializeField] private TextMeshProUGUI stockNum;
    [SerializeField] private TextMeshProUGUI minNum;
    [SerializeField] private TextMeshProUGUI maxNum;
    [SerializeField] private Button increaseButton;
    [SerializeField] private Button decreaseButton;
    [SerializeField] private Slider slider;

    private GameObject blocker;   
    private ShopDetail currentShopDetail;
    private Weapon currentWeapon;
    private bool isMaterialPurchase; // true = 素材購入, false = 武器販売
    private int currentQuantity = 1;
    private int maxQuantity = 1;
    private Color originalColor;

    void Awake()
    {
        //実体化された際に実行される

        //自分の縁の色を決める処理
        ownEdgeImage = GetComponent<Image>();
        originalColor = ownEdgeImage.color;

        //IntegratedShopUIManagerの参照をセット
        integratedShopUIManager = FindObjectOfType<IntegratedShopUIManager>();

        //ブロッカーの取得
        blocker = integratedShopUIManager.blocker;
        
       // ボタンのイベントリスナーを設定
        increaseButton.onClick.AddListener(IncreaseQuantity);
        decreaseButton.onClick.AddListener(DecreaseQuantity);
        buyButton.onClick.AddListener(OnBuyButtonClicked);
        cancelButton.onClick.AddListener(OnCancelButtonClicked);

        // スライダーのイベントリスナーを設定
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void OnDisable()
    {
        currentQuantity = 1;
        //Destroy(gameObject, 0.1f);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Initialize(ShopDetail b_Material)//もし素材を買いたい場合
    {
        // 初期化処理
        currentShopDetail= b_Material;
        //使えるUIコンポーネントを有効化
        ActivateButtonsforMaterialUI();
    }

    public void Initialize(Weapon s_Weapon)//もし武器を売りたい場合
    {
        // 初期化処理
        currentWeapon= s_Weapon;
        //使えないUIコンポーネントを無効化
        DeactiavteButtonsforWeaponUI();
    }

    public void ShowMaterialPurchaseMenu()
    {
        currentWeapon = null;
        isMaterialPurchase = true;
        //ブロッカーを表示
        blocker.SetActive(true);

        // UIの初期化
        materialTitle.text = $"{currentShopDetail.material.Name}を買う？";
        materialImage.sprite = currentShopDetail.material.MaterialImage;
        materialDescription.text = currentShopDetail.material.Description;
        stockNum.text = $"在庫: {currentShopDetail.amount}/{currentShopDetail.initAmount}";
        ownEdgeImage.color=originalColor;
        buyButton.GetComponentInChildren<Text>().text = "購入";

        // スライダーの設定
        maxQuantity = Mathf.Min(currentShopDetail.amount, Storage.Gold / currentShopDetail.material.Price); // 所持金による制限も考慮
        maxNum.text = maxQuantity.ToString();
        slider.minValue = 1;
        slider.maxValue = maxQuantity;
        // Debug.Log($"Slider max value: {maxQuantity}");
        slider.value = 1;

        UpdateQuantityUI();
        Show();
    }

    public void ShowWeaponPurchaseMenu()
    {
        currentShopDetail = null;
        isMaterialPurchase = false;

        //ブロッカーを表示
        blocker.SetActive(true);

        // UIの初期化
        ///一応素材と剣の変数の区別はしとかない、暇があれば後でやる
        materialTitle.text = $"{currentWeapon.weapon.Name}を売りますか？";
        materialImage.sprite = currentWeapon.weapon.WeaponImage;
        materialDescription.text = currentWeapon.weapon.Description;
        buyButton.GetComponentInChildren<Text>().text = "売却";
        ownEdgeImage.color = Color.yellow;
        //売却時は青色の売却ボタンに変更したい（予定）↓
        //buyButton.GetComponentInChildren<Image>().image
        stockNum.text = string.Empty;//全部一つしかないので数量は表示しない

        // 武器は1つずつの販売なのでスライダーを無効化
        maxQuantity = 1;
        slider.minValue = 1;
        slider.maxValue = 1;
        slider.value = 1;
        slider.interactable = false;

        UpdateQuantityUI();
        Show();
    }

    public void ActivateButtonsforMaterialUI()
    {
        materialPropertiesObj.SetActive(true);
    }
    public void DeactiavteButtonsforWeaponUI()
    {
        materialPropertiesObj.SetActive(false);
    }

    private void UpdateQuantityUI()
    {
        currentQuantity = Mathf.RoundToInt(slider.value);
        
        if (isMaterialPurchase)//購入時のみ数量を表示する
            sliderText.text = $"購入数:{currentQuantity}";
            
        else//売却時は表示しない
            sliderText.text = string.Empty;//$"";


        // ボタンの有効/無効を更新
        increaseButton.interactable = currentQuantity < maxQuantity;
        decreaseButton.interactable = currentQuantity > 1;

        // 購入ボタンの有効/無効（金の）
        int transactionAmount = isMaterialPurchase
            ? -(currentShopDetail.material.Price * currentQuantity)//素材を買う
            : currentWeapon.price;//剣を売る

        priceText.text = $"{transactionAmount}";

        
        if (isMaterialPurchase)//購入の際のみ所持金をチェックする
            //これ実は今いらなくなっている
        {
            buyButton.interactable = Storage.Gold >= transactionAmount;
        }
        else
        {
            buyButton.interactable = true;
        }

    }

    private void IncreaseQuantity()
    {
        if (currentQuantity < maxQuantity)
        {
            slider.value += 1;
        }
    }

    private void DecreaseQuantity()
    {
        if (currentQuantity > 1)
        {
            slider.value -= 1;
        }
    }

    private void OnSliderValueChanged(float value)
    {
        UpdateQuantityUI();
    }

    //以下全部onClickにより呼び出される処理
    private void OnBuyButtonClicked()
    {
        if (isMaterialPurchase)
        {
            // 素材購入処理
            int totalCost = currentShopDetail.material.Price * currentQuantity;
            if (Storage.Gold >= totalCost)
            {
                Storage.Gold -= totalCost;
                Storage.AddMaterial(currentShopDetail.material, currentQuantity);
                currentShopDetail.amount -= currentQuantity;
            }
            integratedShopUIManager.CreateMaterialShopUI();
        }
        else
        {
            // 武器販売処理
            Storage.Gold += currentWeapon.price;
            Storage.RemoveWeapon(currentWeapon);
            // Debug.Log("売却ボタン押下" + Storage.Weapons.Count);
            integratedShopUIManager.CreateWeaponShopUI();
        }

        //ブロッカーを消す
        blocker.SetActive(false);
        Hide();  
    }

    private void OnCancelButtonClicked()
    {
        //初期化
        currentShopDetail = null;
        currentWeapon = null;

        //ブロッカーを消す
        blocker.SetActive(false);
        Hide();   
    }


}