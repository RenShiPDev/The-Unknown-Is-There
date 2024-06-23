using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarSureQuestion : MonoBehaviour
{
    [SerializeField] private Image _yesButton;
    [SerializeField] private Image _noButton;

    [SerializeField] private StarInfoGUI _infoGUI;

    [SerializeField] private PCMenuChanger _menuChanger;
    [SerializeField] private float _speed;

    public bool IsActive;

    private void Update()
    {
        if (IsActive)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                _yesButton.fillAmount = 0;
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _noButton.fillAmount = 0;
            }

            if (Input.GetKey(KeyCode.E))
            {
                _yesButton.fillAmount += _speed * Time.deltaTime;
                _noButton.fillAmount = 0;

                if(_yesButton.fillAmount >= 1)
                {
                    if(_infoGUI.SelectedStar != null)
                    {
                        _infoGUI.SelectedStar.Jump();
                    }
                    _yesButton.fillAmount = 0;
                    SetActive(false);
                    gameObject.SetActive(false);
                }
            }
            if (Input.GetKey(KeyCode.Q))
            {
                _noButton.fillAmount += _speed * Time.deltaTime;
                _yesButton.fillAmount = 0;

                if (_noButton.fillAmount >= 1)
                {
                    _noButton.fillAmount = 0;
                    SetActive(false);
                    gameObject.SetActive(false);
                }
            }
        }
    }

    public void SetActive(bool active)
    {
        IsActive = active;
        _menuChanger.IsActive = !active;
    }
}
