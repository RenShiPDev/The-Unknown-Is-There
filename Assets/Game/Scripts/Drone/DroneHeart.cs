using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneHeart : MonoBehaviour
{
    [SerializeField] private GameObject _heart;
    [SerializeField] private float _showTime;

    private float _timer;

    public void ChangeVisible()
    {
        if(Random.Range(0,1000) == 1)
        {
            Show();
        }

        if(_heart.activeSelf)
        {
            _timer += Time.deltaTime;
            if(_timer > _showTime)
            {
                Hide();
                _timer = 0;
            }
        }
    }

    public void Hide()
    {
        _heart.SetActive(false);
    }
    public void Show()
    {
        _heart.SetActive(true);
    }
}
