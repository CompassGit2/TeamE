using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Data;

public class TimerController : MonoBehaviour
{
    public float timeLimit = 300f;
    public TextMeshProUGUI timerText;
    private float timeRemaining;

    void Start()
    {
        timeRemaining = timeLimit;
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            MoveInventoryItemsToStorage(); // インベントリのアイテムをストレージに移動
            SceneManager.LoadScene("ResultScene");
        }
        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("Time : {0:D2}:{1:D2}", minutes, seconds);
    }

    void MoveInventoryItemsToStorage()
    {
        Inventory inventory = FindObjectOfType<Inventory>();

        if (inventory != null)
        {
            foreach (var item in inventory.GetInventoryItems())
            {
                Storage.AddMaterial(item.material, item.amount);
            }
            inventory.ClearInventory(); // インベントリをクリアする
        }
    }
}
