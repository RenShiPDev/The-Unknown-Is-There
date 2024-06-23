using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerTo : MonoBehaviour
{
    public UnityEvent OnActive;

    public bool IsActive;

    [SerializeField] private float _time;

    private float _timer;

    private void Update()
    {
        if (IsActive)
        {
            _timer += Time.deltaTime;
            if(_timer > _time)
            {
                OnActive?.Invoke();
                _timer = 0;
                IsActive = false;
            }
        }
    }

    public void Activate()
    {
        IsActive = true;
    }
}
