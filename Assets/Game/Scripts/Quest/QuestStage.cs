using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestStage : MonoBehaviour
{

    public UnityEvent OnActive;
    public UnityEvent OnComplete;
    public UnityEvent OnDeactive;

    public void Activate()
    {
        OnActive?.Invoke();
        OnComplete?.Invoke();
    }

    public void Deactivate()
    {
        OnDeactive?.Invoke();
    }

}
