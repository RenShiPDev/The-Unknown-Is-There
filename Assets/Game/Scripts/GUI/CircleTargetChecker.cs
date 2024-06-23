using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTargetChecker : MonoBehaviour
{

    [SerializeField] private GameObject _target;

    private void Update()
    {
        RaycastHit hit;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            if (hit.distance < 3f)
            {
                if (hit.collider.gameObject.GetComponent<TakingObject>()
                    || hit.collider.gameObject.GetComponent<InGameButton>())
                {
                    _target.SetActive(true);
                }
                else
                {
                    _target.SetActive(false);
                }
            }
            else
            {
                _target.SetActive(false);
            }
        }
        else
        {
            _target.SetActive(false);
        }
    }

}
