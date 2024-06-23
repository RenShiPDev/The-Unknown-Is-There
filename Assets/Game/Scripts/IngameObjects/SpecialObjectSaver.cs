using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialObjectSaver : MonoBehaviour
{
    [SerializeField] public string Name;
    [SerializeField] public string ID;

    private void Start()
    {
        if(PlayerPrefs.GetInt(Name + ID + "Killed") == 0)
        {
            Vector3 pos = Vector3.zero;
            pos.x = PlayerPrefs.GetFloat(Name + ID + "PosX");
            pos.y = PlayerPrefs.GetFloat(Name + ID + "PosY");
            pos.z = PlayerPrefs.GetFloat(Name + ID + "PosZ");

            if (pos.magnitude != 0)
            {
                Vector3 rot = Vector3.zero;
                rot.x = PlayerPrefs.GetFloat(Name + ID + "RotX");
                rot.y = PlayerPrefs.GetFloat(Name + ID + "RotY");
                rot.z = PlayerPrefs.GetFloat(Name + ID + "RotZ");

                transform.position = pos;
                transform.rotation = Quaternion.Euler(rot);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }

    }

    public void Save()
    {
        SaverManger.Instance.SpecialSave(this);
    }

    public void Kill()
    {
        PlayerPrefs.SetInt(Name + ID + "Killed", 1);
        gameObject.SetActive(false);
    }

    [ContextMenu("MakeAlive")]
    public void MakeAlive()
    {
        PlayerPrefs.SetInt(Name + ID + "Killed", 0);
        gameObject.SetActive(true);
        Save();
    }
}
