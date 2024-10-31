using UnityEngine;
using UnityEngine.EventSystems;

public class ItemBox : MonoBehaviour, IPointerClickHandler
{
    public GameObject uiPrefab;       // 表示したいUIプレファブ
    public GameObject canvasPrefab;    // プレファブ化されたキャンバス
    private GameObject canvasInstance;  // インスタンス化されたキャンバス
    private GameObject uiInstance;      // UIプレファブのインスタンス

    private float lastClickTime;
    private const float doubleClickThreshold = 0.25f; // ダブルクリックの判定時間

    void Start()
    {
        canvasInstance = GameObject.Find("MaterialShop_Canvas");
        // キャンバスが未インスタンス化の場合にインスタンス化
        if (canvasInstance == null)
        {
            canvasInstance = Instantiate(canvasPrefab);
            canvasInstance.name = "MainCanvas"; // 名前を付けると後でわかりやすい
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // ダブルクリックを検知
        if (Time.time - lastClickTime < doubleClickThreshold)
        {
            // すでにインスタンスが存在する場合は何もしない
            if (uiInstance == null)
            {
                ShowUIPrefab();
                //lastClickTime = 0;
            }
            else
            {
                // すでに表示されているUIを非表示にするか破棄
                //HideUIPrefab();
            }
        }
        lastClickTime = Time.time;
    }

    private void ShowUIPrefab()
    {
        // UIプレファブをインスタンス化
        uiInstance = Instantiate(uiPrefab);

        // UIプレファブをキャンバスの子に設定
        uiInstance.transform.SetParent(canvasInstance.transform, false); // ワールド座標を保持しない

        RectTransform rectTransform = uiInstance.GetComponent<RectTransform>();

        // RectTransformを使ってUIを画面中央に配置
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f); // アンカーを中央に設定
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);     // ピボットも中央に
        rectTransform.anchoredPosition = Vector2.zero;     // 画面中央に配置
    }

    private void HideUIPrefab()
    {
        // すでにインスタンスが存在する場合は非表示にするか破棄
        if (uiInstance != null)
        {
            // 非表示にする場合
            uiInstance.SetActive(false);
            // 破棄する場合（コメントアウトして選択）
            // Destroy(uiInstance);

            // uiInstanceをnullに設定
            uiInstance = null; // これにより再表示可能になる
        }
    }
    

}
