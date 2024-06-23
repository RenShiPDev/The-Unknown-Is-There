using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierPC : MonoBehaviour
{
    [SerializeField] private InGameButton _button;
    [SerializeField] private string _pcName;
    [SerializeField] private int _idactive;

    private void Start()
    {
        if(GetActive() == 1)
        {
            PlayerPrefs.SetInt(_pcName, 0);
            _button.OnPressE?.Invoke();
        }
        _idactive = GetActive();
    }

    public void Activate()
    {
        PlayerPrefs.SetInt(_pcName, 1);
    }

    public void Deactivate()
    {
        PlayerPrefs.SetInt(_pcName, 0);
    }

    public int GetActive()
    {
        return PlayerPrefs.GetInt(_pcName);
    }

    public void ChangeActive()
    {
        PlayerPrefs.SetInt(_pcName, Mathf.Abs(GetActive()-1) ); //0_o
    }
}
