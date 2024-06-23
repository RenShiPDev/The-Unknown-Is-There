using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DroneMover : MonoBehaviour
{
    public UnityEvent OnPos;
    public UnityEvent OnMove;

    [SerializeField] private float _droneSpeed;
    [SerializeField] private float _offDist;

    [SerializeField] private GameObject _target;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.LookAt(_target.transform.position);
    }

    private void FixedUpdate()
    {
        Vector3 target = _target.transform.position - transform.position;
        float speed = Random.Range(0.6f, 1f) * _droneSpeed;

        if (target.magnitude > _offDist)
        {
            _rb.AddForce(target * speed);
            OnMove?.Invoke();
        }
        else
        {
            OnPos?.Invoke();

            target.z = 0;
            target.x = 0;
            _rb.AddForce(target * speed);
        }
    }
}
