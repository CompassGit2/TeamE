using UnityEngine;
using TMPro;

namespace SmithScene.Game
{
    public class CountDownText : MonoBehaviour
    {
        TextMeshProUGUI text;
        // Start is called before the first frame update
        void Start()
        {
            text = this.GetComponent<TextMeshProUGUI>();
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
    }

}

