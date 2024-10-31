using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Data;
using UnityEditor.AssetImporters;


public class ItemDetailDialog : MonoBehaviour
{
    //[SerializeField]
    private IntegratedShopUIManager IntegratedshopUIManager;

    [Header("UI References")]
    private Image ownEdgeImage;
    private Text sliderText;
    private Text stockNum;
    private Text minNum;
    private Text maxNum;
    private Text materialTitle;
    private Text materialDescription;
    private Image materialImage;
    private Button increaseButton;
    private Button decreaseButton;
    private Button buyButton;
    private Button cancelButton;
    private Slider slider;
    private GameObject handleSlideArea;
    private Text priceText;
    private GameObject blocker;
    //private Text quantityText;
   
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

        //必要なコンポーネントの参照を取得する
        // SliderTextは階層が深いので、Find で順に探す
        sliderText = transform.Find("BuyMenuPaper/Slider/SliderText").GetComponent<Text>();
        handleSlideArea=transform.Find("BuyMenuPaper/Handle Slide Area").gameObject;
        priceText=transform.Find("BuyMenuPaper/PriceText").GetComponent<Text>();

        // 各テキストコンポーネントの取得
        stockNum = transform.Find("BuyMenuPaper/StockNum").GetComponent<Text>();
        minNum = transform.Find("BuyMenuPaper/MinNum").GetComponent<Text>();
        maxNum = transform.Find("BuyMenuPaper/MaxNum").GetComponent<Text>();
        materialTitle = transform.Find("BuyMenuPaper/MaterialTitle").GetComponent<Text>();
        materialDescription = transform.Find("BuyMenuPaper/MaterialDescription").GetComponent<Text>();

        // Image コンポーネントの取得
        materialImage = transform.Find("BuyMenuPaper/MaterialImage").GetComponent<Image>();

        // Button コンポーネントの取得
        increaseButton = transform.Find("BuyMenuPaper/IncreaseButton").GetComponent<Button>();
        decreaseButton = transform.Find("BuyMenuPaper/DecreaseButton").GetComponent<Button>();
        buyButton = transform.Find("BuyMenuPaper/BuyButton_UI").GetComponent<Button>();
        cancelButton = transform.Find("CancelButton").GetComponent<Button>();

        // Slider コンポーネントの取得
        slider = transform.Find("BuyMenuPaper/Slider").GetComponent<Slider>();

        //IntegratedShopUIManagerの参照をセット
        IntegratedshopUIManager = FindObjectOfType<IntegratedShopUIManager>();

        //ブロッカーの取得
        blocker = IntegratedshopUIManager.blocker;
        
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
        maxQuantity = Mathf.Min(currentShopDetail.amount,
            Storage.Gold / currentShopDetail.material.Price); // 所持金による制限も考慮
        maxNum.text = maxQuantity.ToString();
        slider.minValue = 1;
        slider.maxValue = maxQuantity;
        Debug.Log($"Slider max value: {maxQuantity}");
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
        increaseButton.gameObject.SetActive(true);
        decreaseButton.gameObject.SetActive(true);
        slider.gameObject.SetActive(true);
        //transform.Find("BuyMenuPaper/Handle Slide Area").gameObject.SetActive(true);
        handleSlideArea.SetActive(true);
        stockNum.gameObject.SetActive(true);
        minNum.gameObject.SetActive(true);
        maxNum.gameObject.SetActive(true);
    }
    public void DeactiavteButtonsforWeaponUI()
    {
        increaseButton.gameObject.SetActive(false);
        decreaseButton.gameObject.SetActive(false);
        slider.gameObject.SetActive(false);
        //transform.Find("BuyMenuPaper/Handle Slide Area").gameObject.SetActive(false);
        handleSlideArea.SetActive(false);
        stockNum.gameObject.SetActive(false);
        minNum.gameObject.SetActive(false);
        maxNum.gameObject.SetActive(false);
    }

    private void UpdateQuantityUI()
    {
        currentQuantity = Mathf.RoundToInt(slider.value);
        
        if (isMaterialPurchase)//購入時のみ数量を表示する
            sliderText.text = $"購入数:{currentQuantity.ToString()}";
            
        else//売却時は表示しない
            sliderText.text = string.Empty;//$"";


        // ボタンの有効/無効を更新
        increaseButton.interactable = currentQuantity < maxQuantity;
        decreaseButton.interactable = currentQuantity > 1;

        // 購入ボタンの有効/無効（金の）
        int transactionAmount = isMaterialPurchase
            ? -(currentShopDetail.material.Price * currentQuantity)//素材を買う
            : currentWeapon.price+currentWeapon.bonus;//剣を売る

        priceText.text = isMaterialPurchase ? $"{transactionAmount}"
            :$"+{transactionAmount}";


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

    //以下全部Onclickにより呼び出される処理
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
            IntegratedshopUIManager.CreateMaterialShopUI();
        }
        else
        {
            // 武器販売処理
                Storage.Gold += (currentWeapon.price+currentWeapon.bonus);
                Storage.RemoveWeapon(currentWeapon);
                Debug.Log("売却ボタン押下" + Storage.Weapons.Count);
                IntegratedshopUIManager.CreateWeaponShopUI();
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