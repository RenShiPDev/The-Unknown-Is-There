using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPos;

    public int GetPrice(string name)
    {
        foreach (TakingObject obj in Resources.LoadAll<TakingObject>("ShopObjects"))
        {
            if (obj.GetName() == name)
            {
                return obj.Price;
            }
        }

        return 0;
    }

    public void SpawnObject(string name)
    {
        foreach (TakingObject obj in Resources.LoadAll<TakingObject>("ShopObjects"))
        {
            if (obj.GetName() == name)
            {
                var clone = Instantiate(obj);
                clone.transform.position = _spawnPos.position;
            }
        }
    }

    public void SpawnSpecialObject(GameObject obj)
    {
        var clone = Instantiate(obj);
        clone.transform.position = _spawnPos.position;
    }
}
