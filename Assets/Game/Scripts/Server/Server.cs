using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Server : MonoBehaviour
{
    public static Server Instance = null;

    public UnityEvent OnActive;
    public UnityEvent OnDeactive;

    [SerializeField] private TMP_Text _text;
    [SerializeField] private List<Indicator> _indicators = new List<Indicator>();

    private int _stability;

    private float _stabilityTime;
    private float _timer;

    public bool IsActive;

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

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        PlayerPrefs.SetInt("Stability", 10);
        PlayerPrefs.SetInt("StabilityTime", 120);
        InitNewSettings();
    }

    private void Start()
    {
        InitNewSettings();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _stabilityTime)
        {
            if (Random.Range(0, _stability) == 1)
            {
                RandomizeIndicators();
                Debug.Log(_stability + "   " + _stabilityTime + "  " + _timer);
            }
            _timer = 0;
        }
    }

    public void InitNewSettings()
    {
        _stability = PlayerPrefs.GetInt("Stability");
        _stabilityTime = PlayerPrefs.GetInt("StabilityTime");
        CheckIndicators();
    }

    public void CheckIndicators()
    {
        foreach (var indicator in _indicators)
        {
            if (!indicator.IsActive)
            {
                IsActive = false;
                _text.text = "Deactivated";
                OnDeactive.Invoke();
                return;
            }
        }
        IsActive = true;
        OnActive.Invoke();
        _text.text = "Activated";
    }

    [ContextMenu("Randomize")]
    public void RandomizeIndicators()
    {
        foreach(var indicator in _indicators)
        {
            if(Random.Range(0, 2) == 1)
            {
                indicator.ChangeHide();
            }
        }
        CheckIndicators();
    }
}
