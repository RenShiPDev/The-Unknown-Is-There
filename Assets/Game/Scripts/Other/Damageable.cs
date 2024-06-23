using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent OnDie;

    [SerializeField] private float _health;

    public void GetDamage(float damage)
    {
        _health -= damage;
        if(_health < 0)
        {
            OnDie?.Invoke();
        }
    }
}
