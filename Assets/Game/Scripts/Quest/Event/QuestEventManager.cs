using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEventManager : MonoBehaviour
{
    [SerializeField] private List<QuestEvent> _events = new List<QuestEvent>();

    private List<QuestEvent> _acceptedEvents = new List<QuestEvent>();

    [SerializeField] private float _nextEventTime;

    public QuestEvent _current;

    private float _timer;

    private void Start()
    {
        UpdateAccepted();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer > _nextEventTime)
        {
            UpdateAccepted();
            if(_acceptedEvents.Count > 0)
            {
                _current = _acceptedEvents[Random.Range(0, _acceptedEvents.Count - 1)];
                _current.TryActive();
            }
            _timer = 0;
        }
    }

    public void UpdateAccepted()
    {
        foreach (var e in _events)
        {
            if (e.CheckAccepted() && !e.OnComplete)
            {
                _acceptedEvents.Add(e);
            }
        }
    }
}
