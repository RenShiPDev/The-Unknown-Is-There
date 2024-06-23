using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasseteRewriter : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private CasseteTaker _taker;

    public void Write()
    {
        _taker.Taked.SetName(_name);
    }
}
