using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestEvent : MonoBehaviour
{
    public UnityEvent OnActive;
    public bool OnComplete;

    [SerializeField] public string QuestNameID;
    [SerializeField] private List<QuestEvent> _needToActive = new List<QuestEvent>();

    [SerializeField] public bool IsOnce;

    private void Start()
    {
        if(IsOnce && PlayerPrefs.GetInt(QuestNameID) == 1)
        {
            OnComplete = true;
        }
    }

    public bool TryActive()
    {
        if (!IsOnce)
        {
            OnActive?.Invoke();
            return true;
        }
        else
        {
            if(PlayerPrefs.GetInt(QuestNameID) == 0)
            {
                OnActive?.Invoke();
                return true;
            }
        }

        return false;
    }

    public bool CheckAccepted()
    {
        if (_needToActive.Count > 0)
        {
            foreach(QuestEvent e in _needToActive)
            {
                if(!e.OnComplete)
                {
                    return false;
                }
            }
        }

        return ((PlayerPrefs.GetInt(QuestNameID) == 0 && IsOnce) || !IsOnce);
    }

    public void Complete()
    {
        OnComplete = true;
        PlayerPrefs.SetInt(QuestNameID, 1);
    }
}
