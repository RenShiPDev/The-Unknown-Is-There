using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlanetLineGame : MonoBehaviour
{
    public UnityEvent OnComplete;

    public float MoveSpeed;
    public bool IsActive;

    [SerializeField] private StarInfoGUI _infoGUI;
    [SerializeField] private TMP_Text _percentText;
    [SerializeField] private GameObject _cursor;
    [SerializeField] private Transform _left;
    [SerializeField] private Transform _right;

    [SerializeField] private float _hideTime;
    
    private float _checkSpaceTime = 0.1f;
    private float _timer;

    private bool _inLeft;

    public void Active()
    {
        if (!IsActive)
        {
            float distance = (_right.transform.localPosition.x - _left.transform.localPosition.x) / 2;
            distance = Mathf.Abs(distance);
            float result = Random.Range(-distance, distance);

            var pos = _cursor.transform.localPosition;
            pos.x = result;
            _cursor.transform.localPosition = pos;
        }

        IsActive = true;
        _inLeft = true;
        _timer = 0;
        _percentText.text = 0.ToString("0.00") + "%";
    }

    private void Update()
    {
        if (IsActive)
        {
            if (_inLeft)
            {
                if (_cursor.transform.localPosition.x < _right.transform.localPosition.x)
                {
                    _cursor.transform.localPosition += new Vector3(1, 0, 0) * MoveSpeed * Time.deltaTime;
                }
                else
                {
                    _cursor.transform.localPosition = _right.transform.localPosition;
                    _inLeft = false;
                }
            }
            else
            {
                if (_cursor.transform.localPosition.x > _left.transform.localPosition.x)
                {
                    _cursor.transform.localPosition -= new Vector3(1, 0, 0) * MoveSpeed * Time.deltaTime;
                }
                else
                {
                    _cursor.transform.localPosition = _left.transform.localPosition;
                    _inLeft = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_timer >= _checkSpaceTime)
                {
                    IsActive = false;

                    float distance = (_right.transform.localPosition.x - _left.transform.localPosition.x) / 2;
                    float result = _cursor.transform.localPosition.x / distance;
                    result = Mathf.Abs(result);
                    result = 1 - result;
                    result *= 100f;
                    _percentText.text = result.ToString("0.00") + "%";

                    int money = (int)((result/10f)* _infoGUI.SelectedPlanet.GetPrice());
                    MoneyManager.Instance.AddMoney(money);

                    _timer = 0;
                }
            }
            _timer += Time.deltaTime;

        }
        else
        {
            _timer += Time.deltaTime;
            if(_timer >= _hideTime)
            {
                _timer = 0;
                OnComplete?.Invoke();
            }
        }
    }

}
