using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{
    public GameObject itemSlotPrefab;
    public Transform resultPanel;

    void Start()
    {
        DisplayCollectedItems();
    }

    void DisplayCollectedItems()
    {
        foreach (Transform child in resultPanel)
        {
            Destroy(child.gameObject); // 現在のスロットを削除
        }

        foreach (var stack in GameManager.Instance.collectedItems)
        {
            CreateSlot(stack);
        }
    }

    void CreateSlot(MaterialStack stack)
    {
        var slot = Instantiate(itemSlotPrefab, resultPanel);
        var imageComponent = slot.GetComponent<Image>();
        var textComponent = slot.GetComponentInChildren<Text>();

        if (imageComponent != null)
        {
            imageComponent.sprite = stack.material.MaterialImage;
        }

        if (textComponent != null)
        {
            textComponent.text = $"{stack.material.Name} (×{stack.amount})"; // 個数を表示
        }
    }
}
