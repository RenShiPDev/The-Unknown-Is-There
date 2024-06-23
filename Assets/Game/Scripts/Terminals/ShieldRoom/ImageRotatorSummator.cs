using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageRotatorSummator : MonoBehaviour
{
    [SerializeField] private Indicator _indicator;

    [SerializeField] private GameObject _image1;
    [SerializeField] private GameObject _image2;
    [SerializeField] private GameObject _image3;

    [SerializeField] private Image _image1Rot;
    [SerializeField] private Image _image2Rot;
    [SerializeField] private Image _image3Rot;

    [SerializeField] private RotatingButton _rotatingButton;

    [SerializeField] private Vector3 _rotatingDirection;

    private Vector3 _targetRotarion;

    private void Start()
    {
        Randomize();
    }

    private void Randomize()
    {
        //_rotatingDirection = _rotatingDirection.normalized;
        _targetRotarion = _rotatingDirection * Random.Range(0,36);
        _image1.transform.localRotation = Quaternion.Euler(-_targetRotarion);
        _image1Rot.fillAmount = _targetRotarion.z/360f;

        var rot = _rotatingDirection * Random.Range(0, 36);
        _targetRotarion += rot;
        _image2.transform.localRotation = Quaternion.Euler(-rot);
        _image2Rot.fillAmount = rot.z / 360f;
    }

    public void UpdateIndicatorRotation(float value)
    {
        _image3.transform.localRotation = Quaternion.Euler(new Vector3(0,0, -value * 360f));
        _image3Rot.fillAmount = value;

        float angle = (Mathf.Abs(_targetRotarion.z%360f) - Mathf.Abs(new Vector3(0, 0, value * 360f).z)%360f) % 360f;
        
        angle = Mathf.Abs(angle);
        Debug.Log(_targetRotarion.z % 360f + " " + _image3.transform.localRotation.eulerAngles.z % 360f + " " + angle);

        if (angle < 9f)
        {
            _indicator.SetActive(true);
        }
        else
        {
            _indicator.SetActive(false);
        }
    }

    private Vector3 AbsVec(Vector3 value)
    {
        return new Vector3(Mathf.Abs(value.x), Mathf.Abs(value.y), Mathf.Abs(value.z));
    }
}
