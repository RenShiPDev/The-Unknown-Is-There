using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainMenu : MonoBehaviour
{
    public UnityEvent<bool> OnEsc;

    public bool IsActive;

    [SerializeField] private GameObject _panel;
    [SerializeField] public UnblackEffect UnblackEffect;
    [SerializeField] private PCCameraMover _mover;

    private void Start()
    {
        IsActive = true;
        CheckMenu();
    }

    public void StartGame()
    {
        IsActive = false;
        _panel.SetActive(false);
        CheckMenu();
        UnblackEffect.UnBlack();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_mover.CheckPos())
        {
            OnEscape();
        }
    }

    public void OnEscape()
    {
        if (_panel.activeSelf)
        {
            UnblackEffect.UnBlack();
        }

        _panel.SetActive(!_panel.activeSelf);
        IsActive = !_panel.activeSelf;
        CheckMenu();
    }

    private void CheckMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        OnEsc?.Invoke(IsActive);

        if (IsActive)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        PlayerRotator.Instance.RotEnabled = !IsActive;
        Cursor.visible = IsActive;
        PlayerMover.Instance.gameObject.GetComponent<Rigidbody>().isKinematic = IsActive;
    }

}
