using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SmithScene.Game
{
    public class Fire : MonoBehaviour
    {
        public Action RiseTemperature;

        void Update()
        {
            RiseTemperature();
        }
    }

}
