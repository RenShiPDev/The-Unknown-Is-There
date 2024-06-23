using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetPage : MonoBehaviour
{
    [SerializeField] private string _pageName;

    public string GetPageName()
    {
        return _pageName;
    }
}
