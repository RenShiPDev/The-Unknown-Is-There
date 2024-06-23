using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VendingMachine : MonoBehaviour
{
    public UnityEvent<GameObject> OnBuy;
    [SerializeField] private int _price;
    [SerializeField] private GameObject _product;

    public void BuySomething()
    {
        if(SaverManger.Instance.GetMoney() >= _price)
        {
            SaverManger.Instance.AddMoney(-_price);
            OnBuy?.Invoke(_product);
        }
    }
}
