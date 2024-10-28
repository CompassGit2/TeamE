using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SmithScene.Game
{
    public enum HammerHitResult
    {
        Good,
        Excellent,
        Critical,
        Miss
    }

    public class Hammer : MonoBehaviour
    {
        public Action<HammerHitResult> HammerHit;
        [SerializeField] GameObject movePointer;
        [SerializeField] GameObject targetPointer;
        [SerializeField] AudioSource hammerHitAudioSource;
        [SerializeField] AudioSource hammerEffectAudioSource;
        public AudioClip HammerHitSound;
        public AudioClip CritSound;
        public AudioClip ExSound;
        public AudioClip MissSound;
        Vector3 initialPosition;
        
        [Tooltip("一周の時間")]
        public float size;

        [Tooltip("Criticalが出る範囲")]
        public float critThreshold = 0f;
        [Tooltip("Excellentが出る範囲")]
        public float exThreshold = 0f;
        [Tooltip("Goodが出る範囲")]
        public float goodThreshold = 0f;
        private float angle;
        private float loopRange = 360f;
        private float crosshairPos = 0f;
        private float targetPos;
        private float lastClickTime = 0f;
        private float clickCooldown = 0.05f;
        // Start is called before the first frame update
        void Start()
        {
            initialPosition = new Vector3(movePointer.transform.position.x, movePointer.transform.position.y, movePointer.transform.position.z);
            SetTargetPointer();
        }

        // Update is called once per frame
        void Update()
        {
            crosshairPos += 1f;
            crosshairPos = Mathf.Repeat(crosshairPos, size);
            angle = loopRange / size * crosshairPos;

            movePointer.transform.position = new Vector3(
                // X軸
                initialPosition.x + Mathf.Sin(Mathf.Deg2Rad * angle) * 12 * 0.2f,

                // Y軸
                initialPosition.y + Mathf.Sin(Mathf.Deg2Rad * angle * 2) * 0.6f,

                // Z軸
                initialPosition.z
            );
        
            // マウスの状態の取得
            var mouse = Mouse.current;
            if (mouse != null) {
                if(Time.time - lastClickTime >= clickCooldown)
                {
                    if (mouse.leftButton.wasPressedThisFrame)
                    {
                        Hit();
                        lastClickTime = Time.time;
                    } 
                }
                else
                {
                    if (mouse.leftButton.wasPressedThisFrame)
                    {
                        Debug.Log("Cooldown now");
                    }
                }
            }
            else
            {
                Debug.Log("Mouse Not Found");
            }    
        }

        void SetTargetPointer()
        {
            targetPos = UnityEngine.Random.Range(1,size);
            float targetAngle = loopRange / size * targetPos;
            
            targetPointer.transform.position = new Vector3(
                // X軸
                initialPosition.x + Mathf.Sin(Mathf.Deg2Rad * targetAngle) * 12 * 0.2f,

                // Y軸
                initialPosition.y + Mathf.Sin(Mathf.Deg2Rad * targetAngle * 2) * 0.6f,

                // Z軸
                initialPosition.z
            ); 
        }

        void Hit()
        {
            hammerHitAudioSource.PlayOneShot(HammerHitSound);
            float diff = Mathf.Abs(crosshairPos - targetPos);
            float wrappedDiff = Mathf.Min(diff, size - diff);

            if (wrappedDiff <= critThreshold)
            {
                Debug.Log("Critical!");
                HammerHit(HammerHitResult.Critical);
                hammerEffectAudioSource.PlayOneShot(CritSound);
            }
            else if(wrappedDiff <= exThreshold)
            {
                Debug.Log("Excellent");
                HammerHit(HammerHitResult.Excellent);
                hammerEffectAudioSource.PlayOneShot(ExSound);
            }
            else if(wrappedDiff <= goodThreshold)
            {
                Debug.Log("Good");
                HammerHit(HammerHitResult.Good);
            }
            else
            {
                Debug.Log("Miss...");
                HammerHit(HammerHitResult.Miss);
                hammerEffectAudioSource.PlayOneShot(MissSound);
            }
            SetTargetPointer();
        }
    }

}
