using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AntennChecker : MonoBehaviour
{
    public UnityEvent OnActive;
    public UnityEvent OnDeactive;

    [SerializeField] private GameObject _targetObj;
    [SerializeField] private AntennaRotMov _antennaRotMov;

    [SerializeField] private GameObject _checkAntenn1;
    [SerializeField] private GameObject _checkAntenn2;

    [SerializeField] private Antenna1DirMover _antenna1DirMoverX;
    [SerializeField] private Antenna1DirMover _antenna1DirMoverY;
    [SerializeField] private Antenna1DirMover _antenna1DirMoverZ;

    private void Start()
    {
        _targetObj.transform.localPosition = _antennaRotMov.GetMove(RandomVector()/10f);
        _targetObj.transform.rotation = Quaternion.Euler(RandomVector() * 18);


        var pos = _antenna1DirMoverX.GetRandTarget().transform.localPosition;
        pos.x = _targetObj.transform.localPosition.x;
        _antenna1DirMoverX.GetRandTarget().transform.localPosition = pos;

        pos = _antenna1DirMoverY.GetRandTarget().transform.localPosition;
        pos.y = _targetObj.transform.localPosition.y;
        _antenna1DirMoverY.GetRandTarget().transform.localPosition = pos;

        pos = _antenna1DirMoverZ.GetRandTarget().transform.localPosition;
        pos.z = _targetObj.transform.localPosition.z;
        _antenna1DirMoverZ.GetRandTarget().transform.localPosition = pos;
        //_antenna1DirMoverX.GetRandTarget().transform.localPosition = _targetObj.transform.localPosition;
    }

    private Vector3 RandomVector()
    {
        Random.InitState(1);
        return new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10));
    }

    public void CheckAnglePos()
    {
        if((_checkAntenn1.transform.position - _checkAntenn2.transform.position).magnitude <= 0.01f)
        {
            Vector3 r1 = _antennaRotMov.GetObj().transform.localRotation.eulerAngles.normalized;
            Vector3 r2 = _targetObj.transform.localRotation.eulerAngles.normalized;

            float angle = Quaternion.Angle(_checkAntenn1.transform.localRotation, _checkAntenn2.transform.localRotation);
            //float angle = (r1 - r2).magnitude;
            if(angle <= 90f)
            {
            }
            else
            {
                angle = 180-angle;
            }


            if(angle < 21f)
            {
                OnActive?.Invoke();
                return;
            }
        }

        OnDeactive?.Invoke();
    }
}
