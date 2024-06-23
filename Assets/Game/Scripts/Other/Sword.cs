using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sword : MonoBehaviour
{
    public UnityEvent OnHit;
    public UnityEvent OnNoHit;

    [SerializeField] private GameObject _swordPos;
    [SerializeField] private GameObject _swordPos2;

    [SerializeField] private float _damage;

    public bool _isTaken;

    private bool _isAttack;
    private bool _isPos = true;

    public void TakeSword()
    {
        _isTaken = true;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;
    }

    public void DropSword()
    {
        _isTaken = false;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Collider>().enabled = true;
    }

    public void Attack()
    {

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.distance < 4f && _isPos)
            {
                if (hit.collider.TryGetComponent<Damageable>(out Damageable obj))
                {
                    obj.GetDamage(_damage);
                }
                if (hit.collider.TryGetComponent<Rigidbody>(out Rigidbody rb))
                {
                    rb.AddForce((rb.transform.position - Camera.main.transform.position).normalized * 15f);
                }
                OnHit?.Invoke();
            }
            else
            {
                OnNoHit?.Invoke();
            }
        }

        _isAttack = true;
        if (_isPos)
        {
            _isPos = false;
        }
    }

    private void LateUpdate()
    {
        if (_isTaken)
        {
            gameObject.transform.position = _swordPos.transform.position;

            if(_isAttack)
            {
                if (_isPos)
                {
                    if (Quaternion.Angle(transform.rotation, _swordPos.transform.rotation) >= 1f)
                    {
                        transform.rotation = Quaternion.Slerp(transform.rotation, _swordPos.transform.rotation, 33f * Time.deltaTime);
                    }
                    else
                    {
                        _isAttack = false;
                    }
                    
                }
                else
                {
                    if (Quaternion.Angle(transform.rotation, _swordPos2.transform.rotation) >= 1f)
                    {
                        transform.rotation = Quaternion.Slerp(transform.rotation, _swordPos2.transform.rotation, 33f * Time.deltaTime);
                    }
                    else
                    {
                        _isPos = true;
                    }
                }
            }
            else
            {
                gameObject.transform.rotation = _swordPos.transform.rotation;
            }
        }
    }
}
