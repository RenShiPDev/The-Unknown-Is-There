using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class PCCameraMover : MonoBehaviour
{
    public UnityEvent OnMove;

    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private ObjectHider _cameraHider;
    [SerializeField] private InGameButton _button;
    [SerializeField] private GameObject _light;

    [SerializeField] private GameObject _target1;
    [SerializeField] private GameObject _target2;
    [SerializeField] private CircleTargetChecker _targetChecker;

    public bool IsPos = true;

    [ContextMenu("ChangeCamera")]
    public void ChangeCamera()
    {
        SaverManger.Instance.SaveGame();

        IsPos = !IsPos;

        _button.IsDisabled = !IsPos;

        _light.SetActive(IsPos);
        _target1.SetActive(IsPos);
        _targetChecker.enabled = IsPos;
        _target2.SetActive(IsPos);

        PlayerMover.Instance.IsEnabled = IsPos;
        PlayerRotator.Instance.enabled = IsPos;
        PlayerRotator.Instance.RotEnabled = IsPos;
        _mainMenu.IsActive = IsPos;

        PlayerMover.Instance.gameObject.GetComponent<Collider>().enabled = IsPos;
        PlayerMover.Instance.gameObject.GetComponent<Rigidbody>().isKinematic = !IsPos;

        _cameraHider.ChangeVisibility();
        OnMove?.Invoke();
    }

    public bool CheckPos()
    {
        return _cameraHider.CheckHide();
    }
    public bool CheckIsPos()
    {
        return IsPos;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (!IsPos)
            {
                ChangeCamera();
            }
        }
    }

}
