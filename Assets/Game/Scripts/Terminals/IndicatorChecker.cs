using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IndicatorChecker : MonoBehaviour
{
    public UnityEvent OnActive;
    public UnityEvent OnDeactive;

    [SerializeField] private List<Indicator> _indicators = new List<Indicator>();

    public bool IsActive;

    private void Start()
    {
        foreach (var indicator in _indicators)
        {
            indicator.OnChange.AddListener(CheckActive);
        }
    }

    public void CheckActive()
    {
        foreach (var indicator in _indicators)
        {
            if (!indicator.IsActive)
            {
                IsActive = false;
                OnDeactive?.Invoke();
                return;
            }
        }

        IsActive = true;
        OnActive?.Invoke();
    }


    [ContextMenu("ActiveC")]
    public void ActiveC()
    {
        IsActive = true;
        OnActive?.Invoke();
    }
}
