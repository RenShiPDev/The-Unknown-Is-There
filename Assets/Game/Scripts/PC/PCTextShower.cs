using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PCTextShower : MonoBehaviour
{
    public UnityEvent<char> OnKey;
    public UnityEvent<string> OnText;

    public UnityEvent OnKeySound;

    [SerializeField] private TMP_Text _text;
    [SerializeField] private PCCameraMover _cameraMover;
    [SerializeField] private ScrollRect _scrollRect;

    [SerializeField] private string _acceptedChars;

    [SerializeField] private float _timeout;
    [SerializeField] private float _charTimeout;
    [SerializeField] private float _printTimeout;

    [SerializeField] private float _stickTimeout;

    public string CurChars;

    private string _currentText;
    private string _currentString;

    private float _timer;
    private float _charTimer;
    private float _stickTimer;

    public bool IsTask;
    public bool AcceptWriting = true;

    private void Start()
    {
        _currentString = "> ";

        _text.text = _currentString;
    }

    private void Update()
    {
        if (!_cameraMover.IsPos && _cameraMover.CheckPos())
        {
            _scrollRect.verticalNormalizedPosition += Input.mouseScrollDelta.y/100f;

            if (Input.anyKeyDown)
            {
                OnKeySound?.Invoke();
            }

            if (Input.anyKey)
            {
                if (_currentString.Length > 0)
                {
                    if (_currentString[_currentString.Length - 1] == '|')
                    {
                        _currentString = _currentString.Substring(0, (_currentString.Length - 1));
                        _text.text = _currentText + _currentString;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    Debug.Log(_currentString);
                    if (_currentString.Length > 2)
                    {
                        _currentString = _currentString.Substring(0, (_currentString.Length - 1));
                        _text.text = _currentText + _currentString;
                    }
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    string str = _currentString.Substring(_currentString.IndexOf("> ") + 2);

                    _currentText += _currentString + "\n";
                    _currentString = "> ";

                    _text.text = _currentText + _currentString;

                    OnText?.Invoke(str);
                }

                if (Input.anyKeyDown)
                {
                    WriteChar();
                    _timer = 0;
                    _charTimer = 0;
                }

                _timer += Time.deltaTime;
                if (_timer > _timeout)
                {
                    _charTimer += Time.deltaTime;
                    if (_charTimer > _charTimeout)
                    {
                        WriteChar();
                    }
                }
            }
            else
            {
                _timer = 0;
                _charTimer = 0;
                CurChars = "";

                _stickTimer += Time.deltaTime;
                if (_stickTimer > _stickTimeout)
                {
                    if(_currentString.Length > 0)
                    {
                        if (_currentString[_currentString.Length - 1] == '|')
                        {
                            _currentString = _currentString.Substring(0, (_currentString.Length - 1));
                        }
                        else
                        {
                            _currentString += "|";
                        }
                    }
                    _text.text = _currentText + _currentString;
                }

            }

        }

        if (IsTask)
        {
            if (Input.anyKey)
            {
                foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(kcode))
                    {
                        foreach (char c in _acceptedChars)
                        {
                            if (c == (char)kcode)
                            {
                                OnKey?.Invoke(c);
                            }
                        }
                    }
                }
            }
        }

    }

    public string GetChars()
    {
        return _acceptedChars;
    }

    public void ResetText()
    {
        _text.text = "";
        _currentText = "";
        _currentString = "";
    }

    public void WriteString(string str)
    {
        _currentString = "> " + str;
        _currentText += _currentString + "\n";
        _currentString = "> ";

        _text.text = _currentText + _currentString;
    }

    public void ChangeString(string str)
    {
        _currentString = "> " + str;
        _text.text = _currentText + _currentString;
    }

    private void WriteChar()
    {
        CurChars = "";
        if(_currentString.Length > 20)
        {
            return;
        }

        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                foreach (char c in _acceptedChars)
                {
                    if (c == (char)kcode)
                    {
                        if (AcceptWriting)
                        {
                            _currentString += c;
                        }
                        CurChars += c;
                    }
                }
            }
            if (Input.GetKey(kcode))
            {
                if(_charTimer > _charTimeout)
                {
                    foreach (char c in _acceptedChars)
                    {
                        if (c == (char)kcode)
                        {
                            Debug.Log(c);
                            if(AcceptWriting)
                            {
                                _currentString += c;
                            }
                            CurChars += c;
                        }
                    }
                }
            }
        }

        if (_charTimer > _charTimeout)
        {
            _charTimer = 0;
        }

        _text.text = _currentText + _currentString;
    }
}
