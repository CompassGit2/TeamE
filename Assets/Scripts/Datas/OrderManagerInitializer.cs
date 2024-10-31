using Data.Database;
using UnityEngine;

[DefaultExecutionOrder(-1)]//※実行順序を最初に実行これ無いとバグる
public class OrderManagerInitializer : MonoBehaviour
{
    [SerializeField] private OrderDatabase orderDatabase;
    

    private void Awake()
    {
        InitializeOrderManager();
        Debug.Log("OrderManager initialized from initializer");
    }

    private void Start()
    {
        // Awakeで失敗した場合のバックアップ
        if (!OrderManager.HasDatabase())
        {
            Debug.LogWarning("Retrying OrderManager initialization in Start");
            InitializeOrderManager();
        }
    }

    private void InitializeOrderManager()
    {
        if (orderDatabase == null)
        {
            Debug.LogError("OrderDatabase is not assigned!");
            return;
        }
        OrderManager.Initialize(orderDatabase);
    }
}