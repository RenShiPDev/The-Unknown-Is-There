using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class MoneyManager : MonoBehaviour
{
    public UnityEvent<string> OnMoney;

    public static MoneyManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        OnMoney?.Invoke("MPoints: " + GetMoney().ToString());
    }

    public void AddMoney(int money)
    {
        PlayerPrefs.SetInt("Money", GetMoney()+money);
        OnMoney?.Invoke("MPoints: " + GetMoney().ToString());
    }

    public int GetMoney() 
    {
        return PlayerPrefs.GetInt("Money");
    }
}
