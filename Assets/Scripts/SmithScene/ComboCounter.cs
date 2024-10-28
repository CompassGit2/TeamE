using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

namespace SmithScene.Game
{
    public class ComboCounter : MonoBehaviour
    {
        [SerializeField] Hammer hammer;
        [SerializeField] TextMeshProUGUI comboText;
        [SerializeField] TextMeshProUGUI bonusText;
        HammerHitResult lastHammerResult;
        int nowCombo;
        int maxCombo;
        int bonus;

        public void Initialize()
        {
            nowCombo = 0;
            maxCombo = 0;
            bonus = 0;
            lastHammerResult = HammerHitResult.Miss;
            HideComboText();
            hammer.HammerHit += CheckCombo;

        }

        void CheckCombo(HammerHitResult result)
        {
            if(lastHammerResult == HammerHitResult.Miss)
            {
                lastHammerResult = result;
                return;
            }

            if(result != HammerHitResult.Miss)
            {
                nowCombo ++;
                SetComboText();
                if(nowCombo > maxCombo)
                {
                    maxCombo = nowCombo;
                }
            }
            else
            {
                HideComboText();
                nowCombo = 0;
            }

            lastHammerResult = result;

            if(nowCombo >= 5)
            {
                bonus += 3;
            }
            else if(nowCombo >= 3)
            {
                bonus += 2;
            }
            else if(nowCombo <= 2 && nowCombo > 0)
            {
                bonus += 1;
            }

        }

        void SetComboText()
        {
            comboText.text = $"{nowCombo}COMBO";
            comboText.color = new Color32(255,255,255,255);
        }

        void HideComboText()
        {
            comboText.color = new Color32(255,255,255,0);
        }

        public int GetComboBonus()
        {
            return bonus;
        }
    }

}