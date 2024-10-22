using System;
using UnityEngine;

namespace SmithScene.Game
{
    public class Water : MonoBehaviour
    {
        public Action PutInWater;

        void OnEnable()
        {
            PutInWater();
        }
    }

}
