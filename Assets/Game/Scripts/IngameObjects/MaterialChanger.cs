using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    [SerializeField] private Material _activeMaterial;
    [SerializeField] private Material _deactiveMaterial;
    [SerializeField] private MeshRenderer _meshRenderer;

    public bool IsActive;

    private void Awake()
    {
        if (_meshRenderer == null)
        {
            if (GetComponent<MeshRenderer>() != null)
                _meshRenderer = GetComponent<MeshRenderer>();
        }
    }

    private void Start()
    {
        if (IsActive)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }

    public void Activate()
    {
        _meshRenderer.material = _activeMaterial;
    }

    public void Deactivate()
    {
        _meshRenderer.material = _deactiveMaterial;
    }
}
