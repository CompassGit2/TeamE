using UnityEngine;
using UnityEngine.UI;

public class CancelButton : MonoBehaviour
{


    void Start()
    {
        Button cancelButton = GetComponent<Button>();
        cancelButton.onClick.AddListener(HideUIPrefab);
    }

    private void HideUIPrefab()
    {
        GameObject uiInstance = transform.parent.gameObject;
        // すでにインスタンスが存在する場合は非表示にするか破棄
        if (uiInstance != null)
        {

       
            // 非表示にする場合
            //uiInstance.SetActive(false);
            // 破棄する場合（コメントアウトして選択）
            Destroy(uiInstance);

            // uiInstanceをnullに設定
            //uiInstance = null; // これにより再表示可能になる
        }
    }


}
