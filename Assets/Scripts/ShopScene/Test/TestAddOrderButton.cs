using Data;
using Data.Database;
using UnityEngine;

public class TestAddOrderButton : MonoBehaviour
{
    [SerializeField] OrderDatabase orderDatabase;
    public void OnClickButton()
    {
        foreach(OrderData orderData in orderDatabase.normalOrderList)
        {
            Storage.AddOrderData(orderData);
        }
    }
}
