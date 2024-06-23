using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class NumbersTerminal : MonoBehaviour
{
    public UnityEvent OnActive;
    public UnityEvent OnDeactive;

    [SerializeField] private TMP_Text _numTextA;
    [SerializeField] private TMP_Text _numTextB;

    [SerializeField] private TMP_Text _numText;
    [SerializeField] private List<RotatingButton> _buttons;

    [SerializeField] private bool _isNegative;

    private int _result;

    private void Start()
    {
        _numText.text = "";
        foreach (var button in _buttons)
        {
            button.OnValue.AddListener(CheckButtons);
            _numText.text += "0";
        }
    }

    public void MakeSum()
    {
        int valA = (int)Random.Range(0f, (float)Mathf.Pow(9, _buttons.Count-1));
        int valB = (int)Random.Range(0f, (float)Mathf.Pow(9, _buttons.Count-1));

        _numTextA.text = valA.ToString();
        _numTextB.text = valB.ToString();

        if (_isNegative)
        {
            _result = valA - valB;
        }
        else
        {
            _result = valA + valB;
        }
        CheckButtons();
    }

    public void CheckButtons(float num = 0)
    {
        _numText.text = "";
        foreach (var button in _buttons)
        {
            button.CalcValue();
            int val = (int)(button.Value*9f);
            _numText.text += val;
        }

        if(int.TryParse(_numText.text, out int res))
        {
            if (Mathf.Abs(res) == Mathf.Abs(_result))
            {
                Debug.Log(name + " " + _result);
                OnActive?.Invoke();
            }
            else
            {
                OnDeactive?.Invoke();
            }
        }
        else
        {
            OnDeactive?.Invoke();
        }
    }
}
