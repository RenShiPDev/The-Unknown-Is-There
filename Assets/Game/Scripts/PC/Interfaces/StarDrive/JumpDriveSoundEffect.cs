using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JumpDriveSoundEffect : MonoBehaviour
{
    public UnityEvent OnComplete;

    [SerializeField] private ObjectSound _sound;
    [SerializeField] private ObjectSound _finishSound;

    [SerializeField] private float _time;

    private float _timer;
    public bool IsJump;

    private void Update()
    {
        if (IsJump)
        {
            _timer += Time.deltaTime;
            if(_timer > _time )
            {
                _sound.StopSound();
                _finishSound.PlaySound();

                OnComplete?.Invoke();

                IsJump = false;
                _timer = 0;
            }
        }
    }

    public void Jump()
    {
        IsJump = true;
        _sound.PlaySound();
    }
}
