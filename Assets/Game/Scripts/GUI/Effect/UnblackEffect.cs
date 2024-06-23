using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UnblackEffect : MonoBehaviour
{
    public UnityEvent OnEffect;
    public UnityEvent OnEffectDone;

    [SerializeField] private Image _img;
    [SerializeField] private float _effectSpeed;

    private bool _isEffect;
    private bool _isBlack;

    private void Start()
    {
        _img.color = Color.black;
    }

    [ContextMenu("ShowEffect")]
    public void ShowEffect()
    {
        _isEffect = true;
        _isBlack = false;
        _img.color = Color.black;

        OnEffect?.Invoke();
    }

    public void UnBlack()
    {
        _isEffect = true;
        _isBlack = false;
        if(_img.color.a > 0.1f)
        {
            OnEffect?.Invoke();
        }
    }

    public bool IsEffect()
    {
        return _isEffect && !_isBlack;
    }

    private void Update()
    {
        if (_isEffect)
        {
            if (!_isBlack)
            {
                _img.color = new Color(_img.color.r, _img.color.g, _img.color.b, _img.color.a - Time.deltaTime * _effectSpeed);

                if (_img.color.a <= 0.1f)
                {
                    _isBlack = true;
                    _isEffect = false;
                    gameObject.SetActive(false);
                }
            }
        }
    }

    private Vector3 CToV(Color c)
    {
        return new Vector3(c.r, c.g, c.b);
    }

    private Color VToC(Vector3 v)
    {
        return new Color(v.x, v.y, v.z);
    }
}
