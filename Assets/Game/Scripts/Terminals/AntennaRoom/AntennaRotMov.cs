using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AntennaRotMov : MonoBehaviour
{
    public UnityEvent OnChange;

    public UnityEvent<Vector3> OnMove;
    public UnityEvent<Vector3> OnRotate;

    [SerializeField] private GameObject _antenna;
    [SerializeField] private GameObject _antennaStick;

    [SerializeField] private Transform _anPos0;
    [SerializeField] private Transform _anPosX;
    [SerializeField] private Transform _anPosY;
    [SerializeField] private Transform _anPosZ;

    public Vector3 CurrentPos;
    public Vector3 CurrentRot;

    private Vector3 LastPos;
    private Vector3 LastRot;

    private float _lastValue;

    private void Start()
    {
        CurrentPos = Vector3.zero;
    }

    public void RotateX(float val)
    {
        CurrentRot.x = val*360f;
        RotateToVector(CurrentRot);
    }
    public void RotateY(float val)
    {
        CurrentRot.y = val * 360f;
        RotateToVector(CurrentRot);
    }
    public void RotateZ(float val)
    {
        CurrentRot.z = val * 360f;
        RotateToVector(CurrentRot);
    }

    public void MoveX(float val)
    {
        CurrentPos.x = val;
        MoveToVector(CurrentPos);
    }
    public void MoveY(float val)
    {
        CurrentPos.y = val;
        MoveToVector(CurrentPos);
    }
    public void MoveZ(float val)
    {
        CurrentPos.z = val;
        MoveToVector(CurrentPos);
    }

    public void MoveToVector(Vector3 vec)
    {
        _antenna.transform.localPosition = _anPos0.localPosition + vec * (_anPos0.localPosition - _anPosY.localPosition).magnitude;
        _antennaStick.transform.localPosition = _antenna.transform.localPosition;
        OnMove?.Invoke(CurrentPos);

        DeployOnChange();
        LastPos = CurrentPos;
    }

    public void RotateToVector(Vector3 vec)
    {
        _antenna.transform.rotation = Quaternion.Euler(vec);
        OnRotate?.Invoke(CurrentRot);

        DeployOnChange();
        LastRot = CurrentRot;
    }

    public Vector3 GetPos0()
    {
        return _anPos0.position;
    }

    public Vector3 GetMove(Vector3 vec)
    {
        return _anPos0.localPosition + vec * (_anPosZ.localPosition-_anPos0.localPosition).magnitude;
    }

    public GameObject GetObj()
    {
        return _antenna.gameObject;
    }

    private void DeployOnChange()
    {
        if ((CurrentPos - LastPos).magnitude > 0 || (CurrentRot - LastRot).magnitude > 0)
        {
            OnChange?.Invoke();
        }
    }
}
