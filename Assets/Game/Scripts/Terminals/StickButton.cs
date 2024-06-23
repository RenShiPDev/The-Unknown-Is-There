using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

public class StickButton : MonoBehaviour
{
    public UnityEvent<float> OnValue;
    public UnityEvent<string> OnValueText;

    [SerializeField] private GameObject _indicator;

    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private Transform _buttonParent;

    [SerializeField] private float _buttonSize;

    public float Value;

    private void Start()
    {
        for (int i = 0; i <= (1 / (_buttonSize) + 1); i++)
        {
            var clone = Instantiate(_buttonPrefab, _buttonParent);
            clone.transform.localPosition = new Vector3(0, 0, -0.5f + i * _buttonSize);
            clone.GetComponent<InGameButton>().OnPressEObj.AddListener(SetIndicator);
            clone.GetComponent<InGameButton>().Id = i;
        }
        Init();
    }

    public void Init()
    {
        Value = 0;
        OnValueText?.Invoke(Value.ToString("0.00"));
        OnValue?.Invoke(Value);
    }

    public void SetIndicator(GameObject obj)
    {
        _indicator.transform.position = obj.transform.position;
        float val = obj.GetComponent<InGameButton>().Id / (1 / (_buttonSize));
        Value = val;
        OnValueText?.Invoke(Value.ToString("0.00"));
        OnValue?.Invoke(Value);
    }
}
