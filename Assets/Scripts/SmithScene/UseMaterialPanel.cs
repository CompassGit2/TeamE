using System;
using System.Collections.Generic;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SmithScene.SelectMaterial
{
    public class UseMaterialPanel : MonoBehaviour
    {
        
        [SerializeField] List<Image> materialImages;
        [SerializeField] List<TextMeshProUGUI> materialAmounts;
        [SerializeField] RecipePanel recipePanel;


        // 使用素材リスト
        List<MaterialStack> useMaterials = new List<MaterialStack>();

        /// <summary>
        /// 使用素材リストに素材をセットする
        /// </summary>
        /// <param name="material"></param>
        /// <returns>セットした個数</returns>
        public int SetUseMaterial(MaterialStack materialStack)
        {
            // 使用素材リストの中に既に同じ素材があるか調べる
            foreach(MaterialStack mtStack in useMaterials)
            {
                // あった場合1個追加
                if(mtStack.material == materialStack.material)
                {
                    if(materialStack.amount >= mtStack.amount+1)
                    {
                        mtStack.amount += 1;
                        UpdateUseMaterial();
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }

                }
            }

            // 無かった場合
            // リストに空きがある場合追加
            if(useMaterials.Count < 3)
            {
                if(materialStack.amount > 0)
                {
                    MaterialStack mtStack = new(materialStack.material,1);
                    useMaterials.Add(mtStack);
                    UpdateUseMaterial();
            
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {   
                // 空きが無い場合何もしない
                return 0;
            }

        }

        

        void UpdateUseMaterial()
        {
            for(int i = 0; i < 3; i++)
            {
                if(i < useMaterials.Count)
                {
                    materialImages[i].sprite = useMaterials[i].material.MaterialImage;
                    materialAmounts[i].text = useMaterials[i].amount.ToString();
                }
                else
                {
                    materialImages[i].sprite = null;
                    materialAmounts[i].text = "0";
                }

            }
            recipePanel.SearchRecipe(useMaterials);   

        }

        /// <summary>
        /// 使用素材リストから素材を消す(wip)
        /// </summary>
        /// <param name="materialNum"></param>
        /// <returns></returns>
        public void RemoveUseMaterial(int materialNum)
        {
            if(materialNum > useMaterials.Count -1)
            {
                return;
            }
            useMaterials[materialNum].amount -= 1;
            if(useMaterials[materialNum].amount <= 0)
            {
                useMaterials.RemoveAt(materialNum);
            }
            UpdateUseMaterial();
            return;
        }
    }
}

