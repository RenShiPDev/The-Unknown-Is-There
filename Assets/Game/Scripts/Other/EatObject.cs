using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatObject : MonoBehaviour
{
    [SerializeField] private float _foodValue;
    [SerializeField] private float _mindValue;

    public void EatMe()
    {
        if(PlayerHungryStats.Instance.AddValue(_foodValue) || PlayerMindStats.Instance.AddValue(_mindValue))
        {
            GetComponent<TakingObject>().Killed = true;
            gameObject.SetActive(false);
            SaverManger.Instance.SaveGame();
        }
    }
}
