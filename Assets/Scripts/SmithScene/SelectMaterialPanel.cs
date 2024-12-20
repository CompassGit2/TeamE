using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Data;

namespace SmithScene.SelectMaterial
{
/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */


    class SelectMaterialPanel : MonoBehaviour
    {
        [SerializeField] GridView gridView = default;
        
        [SerializeField] UseMaterialPanel useMaterialPanel;
        [SerializeField] AudioSource systemSoundEffect;
        public AudioClip CellClickedSound;

        void OnEnable()
        {
            gridView.OnCellClicked(index => SetMaterialToUse(Storage.Materials[index]));

            GenerateCells(Storage.Materials);

            gridView.JumpTo(50);
        }

        void TryParseValue(InputField inputField, int min, int max, Action<int> success)
        {
            if (!int.TryParse(inputField.text, out int value))
            {
                return;
            }

            if (value < min || value > max)
            {
                inputField.text = Mathf.Clamp(value, min, max).ToString();
                return;
            }

            success(value);
        }

        void SelectCell()
        {
            if (gridView.DataCount == 0)
            {
                return;
            }

            gridView.UpdateSelection(0);

            /*
            TryParseValue(selectIndexInputField, 0, gridView.DataCount - 1, index =>
            {
                gridView.UpdateSelection(index);
                gridView.ScrollTo(index, 0.4f, Ease.InOutQuint, Alignment.Middle);
            })
            */
        }

        void GenerateCells(List<MaterialStack> materials)
        {
            gridView.UpdateContents(materials);
            SelectCell();
        }

        void SetMaterialToUse(MaterialStack material)
        {
            systemSoundEffect.PlayOneShot(CellClickedSound);
            useMaterialPanel.SetUseMaterial(material);
        }
    }


}

