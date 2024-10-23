using Cysharp.Threading.Tasks;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace SmithScene.Game
{

    public class SmithGameManager : MonoBehaviour
    {
        public enum GameProgress
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

        [SerializeField] GameObject fireObject;
        [SerializeField] GameObject hammerObject;
        [SerializeField] GameObject waterObject;
        Hammer hammer;

        [SerializeField] Slider timeLimitSlider;
        [SerializeField] TextMeshProUGUI timeText;
        [SerializeField] Slider qualitySlider;
        [SerializeField] Slider temperatureSlider;
        [SerializeField] Slider suitTempSlider;

        RecipeData recipe;

        private float timeLimit;
        int nowQuality;
        int maxQuality;
        int temperature;
        float tempRiseSensi;
        float tempDownSensi;

        private int bonus;

        public GameProgress gameProgress = GameProgress.BeforeGame;
        PlayerAction playerAction = PlayerAction.None;
        Keyboard current;
        
        void Start()
        {
            Application.targetFrameRate = 60;
            current = Keyboard.current;
            gameProgress = GameProgress.BeforeGame;
            fireObject.GetComponent<Fire>().RiseTemperature += TempChange;
            hammer = hammerObject.GetComponent<Hammer>();
            hammer.HammerHit += OnHammerHit;
            waterObject.GetComponent<Water>().PutInWater += OnPutInWater;

        }
        

        void Initialize(RecipeData recipeData)
        {
            recipe = recipeData;
            timeLimit = 120.0f + (20.0f * recipe.Weapon.Rarity);
            timeLimitSlider.maxValue = timeLimit;
            timeLimitSlider.value = timeLimit;
            timeText.text = timeLimit.ToString("f1") + "秒";

            nowQuality = 0;
            maxQuality = recipe.MaxQuality;
            qualitySlider.maxValue = maxQuality;

            temperature = 2000;
            tempRiseSensi = recipe.RiseTemperatureSensitivity;
            tempDownSensi = recipe.DownTemperatureSensitivity;

            bonus = 0;

            suitTempSlider.value = recipe.SuitableTemperature;
        }

        // Update is called once per frame
        void Update()
        {
            if(gameProgress == GameProgress.InGame)
            {
                //時間をカウントダウンする
                timeLimit -= Time.deltaTime;
        
                //時間を表示する
                timeText.text = timeLimit.ToString("f1") + "秒";
                timeLimitSlider.value = timeLimit;
        
                //countdownが0以下になったとき
                if (timeLimit <= 0)
                {
                    gameProgress = GameProgress.AfterGame;
                    GameFinish().Forget();
                }

                CheckPlayerAction();
                switch(playerAction)
                {
                    case PlayerAction.Hammer:
                        fireObject.SetActive(false);
                        waterObject.SetActive(false);
                        hammerObject.SetActive(true);
                        break;
                    case PlayerAction.Water:
                        fireObject.SetActive(false);
                        hammerObject.SetActive(false);
                        waterObject.SetActive(true);
                        break;
                    case PlayerAction.Fire:
                        waterObject.SetActive(false);
                        hammerObject.SetActive(false);
                        fireObject.SetActive(true);
                        break;
                    case PlayerAction.None:
                        waterObject.SetActive(false);
                        fireObject.SetActive(false);
                        hammerObject.SetActive(false);
                        break;
                }

                TempChange();
                qualitySlider.value = nowQuality;
            }
            if(gameProgress == GameProgress.AfterGame)
            {
                fireObject.SetActive(false);
                hammerObject.SetActive(false);
            }
        }

        public async UniTaskVoid GameStart(RecipeData recipeData)
        {
            Initialize(recipeData);
            
            await CountDown();
            gameProgress = GameProgress.InGame;
        }

        public async UniTaskVoid GameFinish()
        {
            if(gameProgress != GameProgress.AfterGame)
            {
                gameProgress = GameProgress.AfterGame;
            }

            
            await UniTask.Delay(2000);
            if(nowQuality / maxQuality >= 0.5)
            {
                bonus += (nowQuality / maxQuality * 100) - 50;
                if(temperature <= 500)
                {
                    bonus += 10;
                }
                Weapon weapon = new Weapon(recipe.Weapon, bonus, (recipe.Weapon.Sharpness + recipe.Weapon.Weight) + (bonus * recipe.Weapon.Rarity / 10), recipe.Weapon.BasePrice + (recipe.Weapon.BasePrice * bonus / 100));
            }
            else
            {

            }
        }

        async UniTask CountDown()
        {
            await UniTask.Delay(4000);
        }

        void CheckPlayerAction()
        {
            current = Keyboard.current;
            if(current == null)
            {
                Debug.LogWarning("Keyboard not found.");
                current = Keyboard.current;
                return;
            }

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

        void TempChange()
        {
            if(playerAction == PlayerAction.Fire)
            {
                if(temperature < 10000)
                {
                    temperature += (int)((10 * tempRiseSensi) + (recipe.Weapon.Rarity * tempRiseSensi));
                }
                else
                {
                    temperature = 10000;
                }
            }
            else
            {
                if(temperature > 0)
                {
                    temperature -= (int)((3 * tempDownSensi) + (recipe.Weapon.Rarity * tempDownSensi));
                }
                else
                {
                    temperature = 0;
                }
            }
            temperatureSlider.value = temperature;
            
            if(temperature > recipe.SuitableTemperature + GetNoProgressDistance())
            {
                if(nowQuality > 0)
                {
                    nowQuality -= 1 + (1 * recipe.Weapon.Rarity);
                }
                else
                {
                    nowQuality = 0;
                }
            }

        }

        void OnHammerHit(HammerHitResult hammerHitResult)
        {
            float distanceFromSuite = Mathf.Abs(temperature - recipe.SuitableTemperature);
            float noProgressDistance = GetNoProgressDistance();
            if(distanceFromSuite > noProgressDistance)
            {
                return;
            }

            float progressMultiplier = 1f - (distanceFromSuite / noProgressDistance);
            
            float baseQuality = 0f;
            switch(hammerHitResult)
            {
                case HammerHitResult.Critical:
                    baseQuality = 20f;
                    bonus += 3;
                    break;
                case HammerHitResult.Excellent:
                    baseQuality = 16f;
                    bonus += 1;
                    break;
                case HammerHitResult.Good:
                    baseQuality = 10f;
                    break;
                case HammerHitResult.Miss:
                    baseQuality = 0f;
                    break;
            }
            AddQuality((int)(baseQuality * progressMultiplier));
        }

        void OnPutInWater()
        {
            if(temperature - 1000 >= 0)
            {
                temperature -= 1000;
            }
            else
            {
                temperature = 0;
            }
        }

        void AddQuality(int value)
        {
            if(nowQuality + value > maxQuality)
            {
                nowQuality = maxQuality;
            }
            else
            {
                nowQuality += value;
            }
            Debug.Log(nowQuality.ToString());
        }


        float GetNoProgressDistance()
        {
            float maxDistance = 2000f;
            float minDistance = 500f;

            return Mathf.Lerp(maxDistance, minDistance, recipe.Weapon.Rarity/ 6f);
        }

    }


}

