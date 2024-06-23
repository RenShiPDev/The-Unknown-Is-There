using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepBed : MonoBehaviour
{
    [SerializeField] private MainTime _time;
    [SerializeField] private PlayerMindStats _mindStats;

    public void Sleep()
    {
        _time.Init();
        _mindStats.Init();
    }

}
