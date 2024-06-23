using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPlanetObject : MonoBehaviour
{
    [SerializeField] private Sprite _image;

    public Sprite GetImage() { return _image; }
}
