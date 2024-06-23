using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTeleporter : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private Transform _target;

    public void Teleport()
    {
        if(_object != null)
        {
            _object.transform.position = _target.position;
            _object.transform.rotation = _target.rotation;
        }
        else
        {
            Debug.Log("None to teleport");
        }
    }

    public void Teleport(GameObject obj)
    {
        obj.transform.position = _target.position;
        obj.transform.rotation = _target.rotation;
    }

    public void  SetObject(GameObject obj)
    {
        _object = obj;
    }
}  
