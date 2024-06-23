using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassiveIndicatorRandomiser : MonoBehaviour
{
    [SerializeField] private List<Indicator> _indicators = new List<Indicator>();

    public bool IsActive;

    private void Start()
    {
        TurnOff();
    }

    [ContextMenu("Randomise")]
    public void Randomise()
    {
        foreach (var indicator in _indicators)
        {
            if (Random.Range(0, 2) == 1)
            {
                indicator.SetActive(false);
            }
            else
            {
                indicator.SetActive(true);
            }
        }
        IsActive = true;
    }

    [ContextMenu("TurnOff")]
    public void TurnOff()
    {
        foreach (var indicator in _indicators)
        {
            indicator.SetActive(false);
        }
        IsActive = false;
    }

    public List<Indicator> GetIndicators()
    {
        return _indicators;
    }
}
