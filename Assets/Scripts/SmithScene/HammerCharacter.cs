using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmithScene.Game
{
    public class HammerCharacter : MonoBehaviour
    {
        [SerializeField] Hammer hammer;
        Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
            hammer.HammerHit += PlayHitAnimation;
        }

        void PlayHitAnimation(HammerHitResult hammerHitResult)
        {
            animator.SetTrigger("Hit");
        }
    }
}
