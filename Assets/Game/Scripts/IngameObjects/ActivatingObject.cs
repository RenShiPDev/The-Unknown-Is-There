using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivatingObject : MonoBehaviour
{
    public UnityEvent OnChange;
    public UnityEvent OnActive;
    public UnityEvent OnDeactive;

    [SerializeField] private List<MonoBehaviour> _activatingComponents = new List<MonoBehaviour>();
    [SerializeField] private List<MonoBehaviour> _deactivatingComponents = new List<MonoBehaviour>();

    [SerializeField] private List<GameObject> _activatingGameObject = new List<GameObject>();
    [SerializeField] private List<GameObject> _deactivatingGameObject = new List<GameObject>();

    [SerializeField] private List<Collider> _activatingCollider = new List<Collider>();
    [SerializeField] private List<Collider> _deactivatingCollider = new List<Collider>();

    [SerializeField] private List<MeshRenderer> _activatingRenderer = new List<MeshRenderer>();
    [SerializeField] private List<MeshRenderer> _deactivatingRenderer = new List<MeshRenderer>();

    public bool IsActive; 

    public bool DontStart;

    private void Start()
    {
        if (!DontStart)
        {
            Activate(IsActive);
        }
    }

    public void Activate(bool state = true)
    {
        IsActive = state;

        foreach (var component in _activatingComponents)
        {
            component.enabled = state;
        }
        foreach (var component in _deactivatingComponents)
        {
            component.enabled = !state;
        }

        foreach (var obj in _activatingGameObject)
        {
            obj.SetActive(state);
        }
        foreach (var obj in _deactivatingGameObject)
        {
            obj.SetActive(!state);
        }

        foreach (var obj in _activatingCollider)
        {
            obj.enabled = state;
        }
        foreach (var obj in _deactivatingCollider)
        {
            obj.enabled = !state;
        }

        foreach (var obj in _activatingRenderer)
        {
            obj.enabled = state;
        }
        foreach (var obj in _deactivatingRenderer)
        {
            obj.enabled = !state;
        }

        if (IsActive)
        {
            OnActive?.Invoke();
        }
        else
        {
            OnDeactive?.Invoke();
        }
    }

    public void Deactivate()
    {
        Activate(false);
    }

    [ContextMenu("ChangeActive")]
    public void ChangeActive()
    {
        Activate(!IsActive);
        OnChange?.Invoke();
    }
}
