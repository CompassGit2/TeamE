using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    public float timeLimit = 300f;
    public TextMeshProUGUI timerText;
    private float timeRemaining;
    void Start()
    {
        timeRemaining = timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0; 
            SceneManager.LoadScene("ResultScene"); 
        }
        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        // •ª‚Æ•b‚É•ª‚¯‚é
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        // Žc‚èŽžŠÔ‚ðUI‚É•\Ž¦
        timerText.text = string.Format("Time : {0:D2}:{1:D2}", minutes, seconds);
    }
}
