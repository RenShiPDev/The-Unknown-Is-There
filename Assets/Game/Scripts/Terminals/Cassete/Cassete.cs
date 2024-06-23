using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Cassete : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public UnityEvent OnActive;
    public UnityEvent OnDeactive;

    public string CasseteName;

    public bool IsActivated;

    public void SetName(string name)
    {
        _text.text = name;
        CasseteName = name;
    }

    public void Activate()
    {
        IsActivated = true;
        OnActive?.Invoke();
    }

    public void Deactivate()
    {
        IsActivated = false;
        OnDeactive?.Invoke();
    }
}
