using System;
using UnityEngine;

namespace SmithScene.Game
{
    public class Water : MonoBehaviour
    {
        public Action PutInWater;
        [SerializeField] AudioSource audioSource;
        public AudioClip audioClip;

        void OnEnable()
        {
            audioSource.PlayOneShot(audioClip);
            PutInWater();
        }
    }

}
