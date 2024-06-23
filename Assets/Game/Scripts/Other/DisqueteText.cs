using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisqueteText : MonoBehaviour
{
    [SerializeField] private TakingObject _takingObject;
    [SerializeField] private TMP_Text _text;

    private void Start()
    {
        _text.text = _takingObject.Price.ToString();
    }
}
