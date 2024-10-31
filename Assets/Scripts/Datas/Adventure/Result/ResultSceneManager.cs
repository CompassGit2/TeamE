using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data; // ここでData名前空間をインポート
using Data.Database;

public class ResultSceneManager : MonoBehaviour
{
    public ResultItemDisplay[] resultItemDisplays; // UI要素の配列
    public MaterialDatabase materialDatabase; // マテリアルデータベースの参照

    // 取得したアイテムの情報を表示するメソッド
    public void DisplayResults(List<MaterialStack> collectedMaterials)
    {
        for (int i = 0; i < resultItemDisplays.Length; i++)
        {
            if (i < collectedMaterials.Count)
            {
                // collectedMaterials[i]がData.MaterialStack型であることを確認
                resultItemDisplays[i].SetItemInfo(collectedMaterials[i]); // アイテム情報を設定
                resultItemDisplays[i].gameObject.SetActive(true); // アイテムを表示
            }
            else
            {
                resultItemDisplays[i].gameObject.SetActive(false); // アイテムがない場合は非表示
            }
        }
    }
}

