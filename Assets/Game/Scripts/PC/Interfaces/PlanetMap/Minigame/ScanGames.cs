using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScanGames : MonoBehaviour
{
    public UnityEvent OnActive;

    public bool IsActive;

    [SerializeField] private PlanetMapMover _planetMapMover;

    public void Activate()
    {
        //_scanGames.
        IsActive = true;
        OnActive?.Invoke();
    }

    public void Complete()
    {
        IsActive = false;
        _planetMapMover.Activate();
        gameObject.SetActive(false);
    }
}
