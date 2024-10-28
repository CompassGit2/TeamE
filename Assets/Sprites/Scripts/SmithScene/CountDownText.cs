using UnityEngine;
using TMPro;

namespace SmithScene.Game
{
    public class CountDownText : MonoBehaviour
    {
        public AudioClip CountDownSound;
        public AudioClip StartSound;
        public AudioClip FinishSound;
        TextMeshProUGUI text;
        AudioSource audioSource;
        // Start is called before the first frame update
        void Start()
        {
            text = this.GetComponent<TextMeshProUGUI>();
            audioSource = GetComponent<AudioSource>();
        }

        public void SetText1()
        {
            text.SetText("1");
        }
        public void SetText2()
        {
            text.SetText("2");
        }
        public void SetText3()
        {
            text.SetText("3");
        }
        public void SetTextStart()
        {
            text.SetText("Start!");
        }
        public void SetTextFinish()
        {
            text.SetText("Finish!");
        }
        public void PlayCountDownSound()
        {
            audioSource.PlayOneShot(CountDownSound);
        }
        public void PlayStartSound()
        {
            audioSource.PlayOneShot(StartSound);
        }
        public void PlayFinishSound()
        {
            audioSource.PlayOneShot(FinishSound);
        }
    }

}

