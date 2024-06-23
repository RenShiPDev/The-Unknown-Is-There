using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antenna1DirMover : MonoBehaviour
{
    [SerializeField] private Transform _pos0;
    [SerializeField] private Transform _posTarget;

    [SerializeField] private GameObject _targetObj;
    [SerializeField] private GameObject _targetRandomObj;

    public void Move(float val)
    {
        _targetObj.transform.position = _pos0.position + val * (_posTarget.position - _pos0.position).normalized*(_posTarget.position - _pos0.position).magnitude;
    }

    public GameObject GetRandTarget()
    {
        return _targetRandomObj;
    }
}
