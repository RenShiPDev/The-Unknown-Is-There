using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPanel : MonoBehaviour
{
    [SerializeField] private GameObject _helpPanel;
    [SerializeField] private PCMenuChanger _pcMenu;
    [SerializeField] private PCCameraMover _pcCamera;
    [SerializeField] private MainMenu _menu;

    private bool _first;

    private void Start()
    {
        if (_menu.IsActive)
        {
            _helpPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (!_menu.IsActive && !_pcCamera.CheckPos())
            {
                _helpPanel.SetActive(!_helpPanel.activeSelf);
                if (!_helpPanel.activeSelf)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                }

                Cursor.visible = _helpPanel.activeSelf;
                PlayerRotator.Instance.RotEnabled = !_helpPanel.activeSelf;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !_menu.IsActive && !_pcCamera.CheckPos())
        {
            _helpPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.None;

            Cursor.visible = true;
            PlayerRotator.Instance.RotEnabled = false;
        }
    }

    public void Show()
    {
        if (!_first)
        {
            Debug.Log("_first_first_first");
            _first = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PlayerRotator.Instance.RotEnabled = false;
            //PlayerMover.Instance.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            _helpPanel.SetActive(true);
        }
    }
}
