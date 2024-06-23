using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonMenu : MonoBehaviour
{
    public UnityEvent<ObjectHider> OnPress;

    [SerializeField] private ObjectHider _menu;

    public void Press()
    {
        OnPress?.Invoke(_menu);
    }
}
