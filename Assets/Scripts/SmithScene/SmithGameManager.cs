using Cysharp.Threading.Tasks;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

namespace SmithScene.Game
{

    public class SmithGameManager : MonoBehaviour
    {
        enum GameProgress
        {
            BeforeGame,
            InGame,
            AfterGame,
        }

        enum PlayerAction
        {
            None,
            Hammer,
            Fire,
            Water
        }

        [SerializeField] GameObject fire;

        [SerializeField] Slider timeLimitSlider;
        [SerializeField] TextMeshProUGUI timeText;
        [SerializeField] Slider qualitySlider;
        [SerializeField] Slider temperatureSlider;

        RecipeData recipe;

        float timeLimit;
        int nowQuality;
        int maxQuality;
        int temperature;
        float tempRiseSensi;
        float tempDownSensi;

        GameProgress gameProgress = GameProgress.BeforeGame;
        PlayerAction playerAction = PlayerAction.None;
        Keyboard current;
        
        void Start()
        {
            current = Keyboard.current;
            gameProgress = GameProgress.BeforeGame;
        }
        
        // Start is called before the first frame update
        void Initialize()
        {
            timeLimit = 60.0f + (20.0f * recipe.Weapon.Rarity);
            timeLimitSlider.maxValue = timeLimit;
            timeText.text = timeLimit.ToString("f1") + "秒";

            nowQuality = 0;
            maxQuality = recipe.MaxQuality;
            qualitySlider.maxValue = maxQuality;

            tempRiseSensi = recipe.RiseTemperatureSensitivity;
            tempDownSensi = recipe.DownTemperatureSensitivity;
        }

        // Update is called once per frame
        void Update()
        {
            if(current == null)
            {
                Debug.LogWarning("Keyboard not found.");
                current = Keyboard.current;
                return;
            }


            if(gameProgress == GameProgress.InGame)
            {
                //時間をカウントダウンする
                timeLimit -= Time.deltaTime;
        
                //時間を表示する
                timeText.text = timeLimit.ToString("f1") + "秒";
        
                //countdownが0以下になったとき
                if (timeLimit <= 0)
                {
                    gameProgress = GameProgress.AfterGame;
                }

                switch(playerAction)
                {
                    case PlayerAction.Hammer:
                        break;
                    case PlayerAction.Water:
                        break;
                    case PlayerAction.Fire:
                        fire.SetActive(true);
                        break;
                    case PlayerAction.None:
                        break;
                }

            }
        }

        public async UniTaskVoid GameStart(RecipeData recipeData)
        {
            recipe = recipeData;
            Initialize();
            
            await CountDown();
            gameProgress = GameProgress.InGame;
        }

        async UniTask CountDown()
        {
            await UniTask.Delay(5000);
        }

        void CheckPlayerAction()
        {
            if(current.dKey.isPressed)
            {
                playerAction = PlayerAction.Hammer;
            }
            else if(current.aKey.isPressed)
            {
                playerAction = PlayerAction.Water;
            }
            else if(current.wKey.isPressed)
            {
                playerAction = PlayerAction.Fire;
            }
            else
            {
                playerAction = PlayerAction.None;
            }
        }
    }

}

