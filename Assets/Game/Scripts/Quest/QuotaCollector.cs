using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuotaCollector : MonoBehaviour
{
    [SerializeField] private TMP_Text _quotaText;
    [SerializeField] private int _totalDebt;

    public void WriteQuota()
    {
        _quotaText.text = "Debt: " + (_totalDebt - PlayerPrefs.GetInt("TotalMoney")).ToString();
    }
}
