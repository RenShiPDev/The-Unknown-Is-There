using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WhisperEvent : MonoBehaviour
{
    public UnityEvent OnComplete;

    [SerializeField] private float _timeToWhisper;

    private float _timer;

    private bool _isActive;

    private void Update()
    {
        if (_isActive)
        {
            _timer += Time.deltaTime;
            if(_timer > _timeToWhisper)
            {
                OnComplete?.Invoke();
                _isActive = false;
                _timer = 0;
            }
        }
    }

    public void Activate()
    {
        _isActive = true;
    }
}
