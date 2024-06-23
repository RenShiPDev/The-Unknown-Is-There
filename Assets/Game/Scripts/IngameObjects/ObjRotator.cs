using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotator : MonoBehaviour
{
    [SerializeField] private Vector3 _speed;

    [SerializeField] private bool _enabled = false;

    private void Update()
    {
        if (_enabled)
        {
            transform.rotation *= Quaternion.Euler(_speed);
        }
    }

    public void ChangeEnabled()
    {
        _enabled = !_enabled;
    }
}
