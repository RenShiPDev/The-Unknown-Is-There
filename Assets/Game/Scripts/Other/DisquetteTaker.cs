using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DisquetteTaker : MonoBehaviour
{
    public UnityEvent OnTake;
    public UnityEvent<GameObject> HideObj;

    private SpecialObjectSaver _curSpecObj;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out TakingObject obj))
        {
            if(obj.GetName() == "disquette")
            {
                if (obj.gameObject.TryGetComponent(out SpecialObjectSaver specObj))
                {
                    SaverManger.Instance.AddMoney(obj.Price);
                    _curSpecObj = specObj;

                    obj.GetComponent<Collider>().enabled = false;
                    obj.GetComponent<Rigidbody>().isKinematic = true;

                    HideObj?.Invoke(specObj.gameObject);
                    OnTake?.Invoke();
                }
            }
        }
    }

    public void KillDisk()
    {
        _curSpecObj.Kill();
    }

}
