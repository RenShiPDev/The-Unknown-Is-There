using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PCInternetManager : MonoBehaviour
{
    [SerializeField] private PCTextShower _shower;
    [SerializeField] private TMP_Text _memeticText;

    [SerializeField] private GameObject _internetPanel;

    [SerializeField] private List<NetPage> _pages = new List<NetPage>();
    [SerializeField] private NetPage _currentPage;

    private float _timer;

    public bool IsInternet = false;

    public void CheckMessage(string message)
    {
        if ((message == "internet" || message == "net") && !_shower.IsTask)
        {
            _shower.ResetText();
            
            IsInternet = true;
            _internetPanel.SetActive(IsInternet);


            _shower.WriteString("w");
            _shower.WriteString("h");
            _shower.WriteString("e");
            _shower.WriteString("r");
            _shower.WriteString("e");
            _shower.WriteString("i");
            _shower.WriteString("s");
            _shower.WriteString("t");
            _shower.WriteString("h");
            _shower.WriteString("e");
            _shower.WriteString("r");
            _shower.WriteString("e");
            _shower.WriteString("Welcome to the INTERNET!.");
        }

        if(message == "exit" || message == "start" || message == "shop" || message == "tshop" || message == "thingsshop")
        {
            if(IsInternet)
            {
                Exit();
            }
        }

        if(IsInternet)
        {
            Debug.Log(message);
            foreach(NetPage page in _pages)
            {
                Debug.Log(page.GetPageName() + "    a");
                if (message == page.GetPageName())
                {
                    _currentPage.gameObject.SetActive(false);
                    _currentPage = page;
                    _currentPage.gameObject.SetActive(true);
                }
            }
        }
    }

    private void Update()
    {
        if (IsInternet && _timer < 1)
        {
            _timer += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && _timer > 0.5f && IsInternet)
        {
            Exit();
        }
    }

    private void Exit()
    {
        IsInternet = false;
        _internetPanel.SetActive(IsInternet);
        _shower.WriteString("Goodbye!");
    }
}
