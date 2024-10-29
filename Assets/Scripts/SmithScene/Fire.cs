using System;
using UnityEngine;


namespace SmithScene.Game
{
    public class Fire : MonoBehaviour
    {
        public Action RiseTemperature;
        [SerializeField] AudioSource audioSource;

        void OnEnable()
        {
            audioSource.Play();
        }

        void OnDisable() 
        {
            audioSource.Stop();    
        }

        void Update()
        {
            RiseTemperature();
        }
    }

}
