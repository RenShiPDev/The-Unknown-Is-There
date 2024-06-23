using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

public class RotatingButton : MonoBehaviour
{
    public UnityEvent OnActive;
    public UnityEvent OnDeactive;
    public UnityEvent<float> OnValue;

    public float Value;

    [SerializeField] private Vector3 _rotatingDirection;
    [SerializeField] private float _rotatingAngle;

    [SerializeField] private Vector3 _targetRotarion;
    [SerializeField] private List<ObjectSound> _sounds = new List<ObjectSound>();

    [SerializeField] private bool _noRandom;

    private int _soundId;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _rotatingDirection = _rotatingDirection.normalized;
        _targetRotarion = transform.rotation.eulerAngles + _targetRotarion;

        if (!_noRandom)
        {
            _targetRotarion = transform.rotation.eulerAngles + _rotatingDirection * (_rotatingAngle * (Random.Range(0, (int)(360 / _rotatingAngle))));
            transform.rotation *= Quaternion.Euler(_rotatingDirection * _rotatingAngle * Random.Range(0, (int)(360 / _rotatingAngle)));

        }
        CheckActive();
    }

    public void RotateUp()
    {
        transform.rotation *= Quaternion.Euler(_rotatingDirection*_rotatingAngle);
        CheckActive();
        PlaySound();
    }

    public void RotateDown()
    {
        transform.rotation *= Quaternion.Euler(-_rotatingDirection * _rotatingAngle);
        CheckActive();
        PlaySound();
    }

    private void CheckActive()
    {
        CalcValue();

        OnValue?.Invoke(Value);

        if (_soundId == _sounds.Count - 1)
        {
            OnActive?.Invoke();
        }
        else
        {
            OnDeactive?.Invoke();
        }

    }

    public void CalcValue()
    {
        Vector3 rotationVal = transform.rotation.eulerAngles;

        float angle = Quaternion.Angle(Quaternion.Euler(rotationVal), Quaternion.Euler(_targetRotarion));
        //Debug.Log(rotationVal + " " + _targetRotarion + " = " + angle);

        Value = (180f - (float)angle) / 180f;
        _soundId = (int)((_sounds.Count - 1) * Value);
    }

    private void PlaySound()
    {
        _sounds[_soundId].PlaySound();
    }
}
