using Data.Database;
using UnityEngine;

[DefaultExecutionOrder(-1)]//�����s�������ŏ��Ɏ��s���ꖳ���ƃo�O��
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
        // Awake�Ŏ��s�����ꍇ�̃o�b�N�A�b�v
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