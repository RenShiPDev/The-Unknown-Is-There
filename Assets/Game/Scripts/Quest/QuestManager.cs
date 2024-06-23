using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    [SerializeField] private List<QuestStage> _stages = new List<QuestStage>();

    [SerializeField] private int _questID;

    private void Start()
    {
        _questID = PlayerPrefs.GetInt("QuestID");

        for(int i = 0; (i < _questID-1 && i < _stages.Count); i++)
        {
            _stages[i].OnComplete?.Invoke();
        }

        ActivateQuest(_questID);
    }

    [ContextMenu("NextQuest")]
    public void NextQuest()
    {
        _questID++;
        PlayerPrefs.SetInt("QuestID", _questID);
        ActivateQuest(_questID);
    }

    public void ActivateCurrent()
    {
        ActivateQuest(_questID);
        _questID++;
        PlayerPrefs.SetInt("QuestID", _questID);
    }

    private void ActivateQuest(int id)
    {
        if(id > 0 && id < _stages.Count)
        {
            Debug.Log(id + " < " + _stages.Count);
            _stages[id - 1].Deactivate();
            _stages[id].Activate();
        }
    }

}
