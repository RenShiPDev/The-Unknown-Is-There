using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Indicator : MonoBehaviour
{
    public UnityEvent OnChange;

    public UnityEvent OnActive;
    public UnityEvent OnDeactive;

    public bool IsActive;

    private void Start()
    {
        Init();
    }

    public void Init()
    {

    }

    public void ChangeHide()
    {
        if(GetComponent<ObjectHider>() != null)
        {
            GetComponent<ObjectHider>().ChangeVisibility();
        }
    }

    public void ChangeActive()
    {
        IsActive = !IsActive;
        if(IsActive)
        {
            OnActive?.Invoke();
        }
        else
        {
            OnDeactive?.Invoke();
        }

        OnChange?.Invoke();

        Init();
    }
    public void SetActive(bool act)
    {
        IsActive = act;
        if (IsActive)
        {
            OnActive?.Invoke();
        }
        else
        {
            OnDeactive?.Invoke();
        }

        OnChange?.Invoke();

        Init();
    }
}
