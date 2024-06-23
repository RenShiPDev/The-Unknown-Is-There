using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetAll : MonoBehaviour
{
    public void ResetAllData()
    {
        SaverManger.Instance.ResetData();
        SceneManager.LoadScene(0);
    }
}
