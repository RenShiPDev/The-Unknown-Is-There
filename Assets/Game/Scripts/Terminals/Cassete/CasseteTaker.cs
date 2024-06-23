using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CasseteTaker : MonoBehaviour
{
    public UnityEvent OnTake;
    public UnityEvent OnDrop;

    [SerializeField] private string _targetName;
    [SerializeField] private bool _onlyActive;

    [SerializeField] private ObjectHider _hider;
    [SerializeField] private ObjectTeleporter _teleporter;
    [SerializeField] private MaterialChanger _changer;

    public Cassete Taked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<TakingObject>(out TakingObject takingobj))
        {
            if (other.gameObject.TryGetComponent<Cassete>(out Cassete obj) && takingobj.IsTaked)
            {
                if (obj.CasseteName == _targetName || _targetName == "")
                {
                    if (_onlyActive && obj.IsActivated)
                    {
                        takingobj.DropMe();
                        Taked = obj;
                        _hider.HideObj(Taked.gameObject);
                        _teleporter.SetObject(Taked.gameObject);
                        OnTake?.Invoke();
                    }
                    if (!_onlyActive)
                    {
                        takingobj.DropMe();
                        Taked = obj;
                        _hider.HideObj(Taked.gameObject);
                        _teleporter.SetObject(Taked.gameObject);
                        OnTake?.Invoke();
                    }
                }
            }
        }
    }

    public void Activate()
    {
        if (Taked != null)
        {
            Taked.Activate();
        }
    }

    public void Deactivate()
    {
        if(Taked != null)
        {
            Taked.Deactivate();
            OnDrop?.Invoke();
        }

        if(_changer != null)
        {
            _changer.Deactivate();
        }

        OnDrop?.Invoke();
    }

    public void Drop()
    {
        _teleporter.Teleport();
        Deactivate();

        if (Taked != null)
        {
            if (Taked.TryGetComponent<Rigidbody>(out Rigidbody rbobj))
            {
                rbobj.isKinematic = false;
            }
            if (Taked.TryGetComponent<Collider>(out Collider colobj))
            {
                colobj.enabled = true;
            }
        }
        _teleporter.SetObject(null);
        Taked = null;
    }

    public void EraseCasset()
    {
        if (Taked != null)
        {
            Taked.Deactivate();
            Taked.SetName("null");
        }
    }

}
