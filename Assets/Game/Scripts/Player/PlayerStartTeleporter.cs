using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartTeleporter : MonoBehaviour
{
    [SerializeField] private ObjectHider _hider;

    private bool _isTeleported;

    [ContextMenu("Teleport")]
    public void Teleport()
    {
        if(!_isTeleported)
        {
            _isTeleported = true;
            _hider.Hide();
        }
    }
}
