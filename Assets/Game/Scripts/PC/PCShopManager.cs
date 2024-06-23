using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PCShopManager : MonoBehaviour
{
    [SerializeField] private PCTextShower _shower;
    [SerializeField] private TMP_Text _memeticText;
    [SerializeField] private ObjectsSpawner _spawner;

    private float _timer;

    public bool IsShop;
    public bool IsThingsShop;

    public void CheckMessage(string message)
    {
        if (message == "shop" && !_shower.IsTask)
        {
            _shower.ResetText();
            _shower.WriteString("Welcome to shop.");
            _shower.WriteString("Type - products (prod)");
            IsShop = true;
        }
        if ((message == "thingsshop" || message == "tshop") && !_shower.IsTask)
        {
            _shower.ResetText();
            _shower.WriteString("Welcome to THINGS shop.");
            _shower.WriteString("Type - products (prod)");
            IsThingsShop = true;
        }

        if (IsShop)
        {
            CheckShop(message);
        }

        if (IsThingsShop)
        {
            CheckThingsShop(message);
        }

        if(!IsThingsShop && !IsShop)
        {
            if (message == "stability" || message == "stab")
            {
                _shower.WriteString("Current : " + PlayerPrefs.GetInt("Stability"));
            }

            if (message == "stabilitytime" || message == "stabt")
            {
                _shower.WriteString("Current : " + PlayerPrefs.GetInt("StabilityTime"));
            }
        }

        _memeticText.text = "Memetic points: " + SaverManger.Instance.GetMoney();
    }

    private void CheckThingsShop(string message)
    {
        if (message == "exit" || message == "start")
        {
            Exit();
            return;
        }

        if (message == "products" || message == "prod")
        {
            foreach (TakingObject obj in Resources.LoadAll<TakingObject>("ShopObjects"))
            {
                _shower.WriteString(obj.GetName() + ". Price: " + obj.Price);
            }

            return;
        }

        string str = message.Substring(message.IndexOf(' ') + 1);
        string name = message.Replace(" " + str, "");
        Debug.Log("CountStr = " + str);
        int count = 1;
        int.TryParse(str, out int newCount);
        if(newCount > 0)
        {
            count = newCount;
        }

        Debug.Log(name + " = " + count);

        if (SaverManger.Instance.GetMoney() >= _spawner.GetPrice(name) * count)
        {
            for (int i = 0; i < count; i++)
            {
                _spawner.SpawnObject(name);
            }
        }
        else
        {
            _shower.WriteString("Not enought points");
        }
    }

    private void CheckShop(string message)
    {
            if (message == "exit" || message == "start")
            {
                Exit();
            }

            if (message == "products" || message == "prod")
            {
                _shower.WriteString("Stability (stab). Price: " + Price("Stability"));
                _shower.WriteString("Stability time (stabt). Price: " + Price("StabilityTime"));
            }

            if (message == "stability" || message == "stab")
            {
                BuyValue("Stability");
            }

            if (message == "stabilitytime" || message == "stabt")
            {
                BuyValue("StabilityTime");
            }
    }

    private void Update()
    {
        if ((IsShop || IsThingsShop) && _timer < 1)
        {
            _timer += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && _timer > 0.5f && (IsShop || IsThingsShop))
        {
            Exit();
        }
    }

    private void BuyValue(string valueName)
    {
        if (PlayerPrefs.GetInt("Money") >= Price(valueName))
        {
            SaverManger.Instance.AddMoney(-Price(valueName));
            PlayerPrefs.SetInt(valueName, PlayerPrefs.GetInt(valueName) + 1);
            _shower.WriteString(valueName + " added.");
            Server.Instance.InitNewSettings();
        }
        else
        {
            _shower.WriteString("Not enought money.");
        }
        _shower.WriteString("Current : " + PlayerPrefs.GetInt(valueName));
    }

    private int Price(string name)
    {
        return  (int)(PlayerPrefs.GetInt(name) * Mathf.Sqrt(2));
    }

    private int StabilityPrice()
    {
        return (int)(PlayerPrefs.GetInt("Stability") * Mathf.Sqrt(2));
    }
    private int StabilityTimePrice()
    {
        return (int)(PlayerPrefs.GetInt("StabilityTime") * Mathf.Sqrt(2));
    }

    private void Exit()
    {
        IsShop = false;
        IsThingsShop = false;
        _shower.WriteString("Bye!");
    }
}
