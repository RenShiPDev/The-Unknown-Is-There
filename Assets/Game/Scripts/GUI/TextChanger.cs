using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextChanger : MonoBehaviour
{
    public void Change(string str)
    {
        GetComponent<TMP_Text>().text = str;
    }
}
