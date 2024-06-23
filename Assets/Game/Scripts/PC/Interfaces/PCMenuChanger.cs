using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PCMenuChanger : MonoBehaviour
{
    public UnityEvent OnChange;

    [SerializeField] private List<GameObject> _menus = new List<GameObject>();

    [SerializeField] private PCCameraMover _pCCameraMover;
    [SerializeField] private GameObject _currentMenu;

    [SerializeField] private PlanetMapMover _planetMapMover;
    [SerializeField] private StarMapMover _starMapMover;

    private int _currentID;

    public bool IsActive;

    private void Start()
    {
        _currentID = 0;
        IsActive = true;

        if (_currentMenu != null)
        {
            _currentMenu.SetActive(false);
        }

        foreach(var menu in _menus) 
        {
            menu.SetActive(false);
        }

        _currentMenu = _menus[_currentID];
        _currentMenu.SetActive(true);
    }

    private void ChangeMenu()
    {
        if(_planetMapMover.gameObject.activeSelf)
        {
            _planetMapMover.SpawnPlanets();
        }
        if (_starMapMover.gameObject.activeSelf)
        {
            _starMapMover.SpawnStars();
        }
        OnChange?.Invoke();
    }

    public void LateUpdate()
    {
        if (_pCCameraMover.CheckPos() && IsActive)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (_pCCameraMover.CheckPos())
                {
                    _currentID--;
                    if (_currentID < 0)
                    {
                        _currentID = _currentID = _menus.Count - 1;
                    }
                    if (_currentMenu != null)
                    {
                        _currentMenu.SetActive(false);
                    }
                    _currentMenu = _menus[_currentID];
                    _currentMenu.SetActive(true);
                    ChangeMenu();
                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_pCCameraMover.CheckPos())
                {
                    _currentID++;
                    if (_currentID >= _menus.Count)
                    {
                        _currentID = 0;
                    }
                    if (_currentMenu != null)
                    {
                        _currentMenu.SetActive(false);
                    }
                    _currentMenu = _menus[_currentID];
                    _currentMenu.SetActive(true);
                    ChangeMenu();
                }
            }
        }
    }

}
