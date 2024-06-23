using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTargetCol : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out StarGUI star))
        {
            star.ShowInfo();
        }
        if (other.gameObject.TryGetComponent(out PlanetGui planet))
        {
            planet.ShowInfo();
        }
    }
}
