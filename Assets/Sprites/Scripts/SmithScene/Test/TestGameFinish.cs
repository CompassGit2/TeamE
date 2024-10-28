using System.Collections;
using System.Collections.Generic;
using SmithScene.Game;
using UnityEngine;

public class TestGameFinish : MonoBehaviour
{
    [SerializeField] SmithGameManager smithGameManager;
    public void OnClickTestGameFinishButton()
    {
        smithGameManager.GameFinish().Forget();
    }
}
