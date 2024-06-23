using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkOpener : MonoBehaviour
{
    [SerializeField] private string _url;

    public void OpenURL()
    {
        Application.OpenURL(_url);
    }
}
