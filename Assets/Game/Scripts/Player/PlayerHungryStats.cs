using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHungryStats : MonoBehaviour
{
    public static PlayerHungryStats Instance = null;

    [SerializeField] private Image _mindImage;
    [SerializeField] private float _maxPoints;
    [SerializeField] private float _tiredSpeed;
    [SerializeField] private float _speedMult;

    private float _mindPoints;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        _mindPoints -= Time.deltaTime * _tiredSpeed * _speedMult;
        _mindImage.fillAmount = _mindPoints / _maxPoints;
    }

    public bool AddValue(float val)
    {
        if((_mindPoints+val) < _maxPoints)
        {
            _mindPoints += val;
            return true;
        }
        else
        {
            _mindPoints = _maxPoints;
            return false;
        }
    }

    public void UpdateMult(float mult)
    {
        mult *= 2;
        _speedMult = mult * mult + 1;
    }
    public float GetValue()
    {
        return _mindPoints / _maxPoints;
    }

    public void Init()
    {
        _speedMult = 1;
        _mindPoints = _maxPoints;
        _mindImage.fillAmount = _mindPoints / _maxPoints;
    }
}
