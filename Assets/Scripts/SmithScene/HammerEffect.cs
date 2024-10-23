using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SmithScene.Game
{
    public class HammerEffect : MonoBehaviour
    {
        [SerializeField] Animator HitAnimator;
        [SerializeField] Animator CritAnimator;
        
        [SerializeField] Hammer hammer;
        
        void Start()
        {
            hammer.HammerHit += PlayAnimation;
        }

        void PlayAnimation(HammerHitResult hammerHitResult)
        {
            HitAnimator.SetTrigger("Hit");
            switch(hammerHitResult)
            {
                case HammerHitResult.Critical:
                    CritAnimator.SetTrigger("Critical");
                    break;
                case HammerHitResult.Excellent:
                    CritAnimator.SetTrigger("Excellent");
                    break;
            }
        }
    }

}

