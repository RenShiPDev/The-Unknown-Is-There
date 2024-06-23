using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainTime : MonoBehaviour
{
    public UnityEvent<string> OnTime;
    public UnityEvent<float> OnTimeFloat;

    public float CurTime;
    public bool IsTimer;

    [SerializeField] private float _timeSpeed;
    
    private float _timer;

    public void Init()
    {
        _timer = 0;
    }

    private void Update()
    {
        if (IsTimer)
        {
            _timer += Time.deltaTime*_timeSpeed;

            if(_timer >= 24 * 60)
            {
                _timer = 0;
            }
            else
            {
                CurTime = _timer;
            }

            string timeStr = (Mathf.Floor(CurTime / 60)).ToString("00") + ":" + (CurTime % 60).ToString("00");
            OnTime?.Invoke(timeStr);
            OnTimeFloat?.Invoke(CurTime / (24 * 60));
        }
    }
}
