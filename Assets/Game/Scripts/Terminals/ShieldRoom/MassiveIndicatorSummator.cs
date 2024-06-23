using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassiveIndicatorSummator : MonoBehaviour
{
    [SerializeField] private MassiveIndicatorRandomiser _randomiser1;
    [SerializeField] private MassiveIndicatorRandomiser _randomiser2;

    [SerializeField] private List<Indicator> _indicators = new List<Indicator>();
    [SerializeField] private Indicator _thisIndicator;

    private int[,] _targetMass;

    [ContextMenu("Randomise")]
    public void Randomise()
    {
        _randomiser1.Randomise();
        _randomiser2.Randomise();
    }

    [ContextMenu("MakeSum")]
    public void MakeSum()
    {
        int masLength = (int)Mathf.Sqrt(_randomiser1.GetIndicators().Count);
        _targetMass = new int[masLength,masLength];
        int id = 0;
        for (int i = 0; i < masLength; i++)
        {
            var str = "";
            for (int j = 0; j < masLength; j++)
            {
                if (_randomiser1.GetIndicators()[id].IsActive ^ _randomiser2.GetIndicators()[id].IsActive)
                {
                    _targetMass[i, j] = 1;
                }
                else
                {
                    _targetMass[i, j] = 0;
                }
                str += _targetMass[i, j] + " ";
                id++;
            }
        }
        CheckMass();
    }

    public void CheckMass()
    {
        if(_targetMass != null && _randomiser1.IsActive && _randomiser2.IsActive)
        {
            int masLength = (int)Mathf.Sqrt(_randomiser1.GetIndicators().Count);

            int id = 0;
            for (int i = 0; i < masLength; i++)
            {
                for (int j = 0; j < masLength; j++)
                {
                    if(( (_indicators[id].IsActive && _targetMass[i,j] == 0)
                        || ( (!_indicators[id].IsActive) && _targetMass[i, j] == 1)
                        ))
                    {
                        return;
                    }
                    else
                    {
                        Debug.Log(id);
                    }
                    id++;
                }
            }
        }
        else
        {
            _thisIndicator.SetActive(false);
            return;
        }

        _thisIndicator.SetActive(true);
    }

    [ContextMenu("CheckMassDel")]
    public void CheckMassDel()
    {
        if (_targetMass != null && _randomiser1.IsActive && _randomiser2.IsActive)
        {
            int masLength = (int)Mathf.Sqrt(_randomiser1.GetIndicators().Count);

            for (int i = 0; i < masLength; i++)
            {
                //i3 j2 s3
                //(i*j)+j
                //1*2+2=4
                for (int j = 0; j < masLength; j++)
                {
                    int id = i * j + i;
                    if (!(_indicators[id].IsActive && (_targetMass[i, j] == 1)
                          || !_indicators[id].IsActive && (_targetMass[i, j] == 0)))
                    {
                        _thisIndicator.SetActive(false);
                        _randomiser1.GetIndicators()[id].SetActive(false);
                        _randomiser2.GetIndicators()[id].SetActive(false);
                        _indicators[id].gameObject.SetActive(false);
                        Debug.Log(i + " " + j);
                        return;
                    }
                    else
                    {
                        _indicators[id].gameObject.SetActive(true);
                        _randomiser1.GetIndicators()[id].SetActive(true);
                        _randomiser2.GetIndicators()[id].SetActive(true);
                        Debug.Log(_indicators[i + j].IsActive + " && " + (_targetMass[i, j]));
                    }
                }
            }
        }
        else
        {
            _thisIndicator.SetActive(false);
            return;
        }

        _thisIndicator.SetActive(true);
    }
}
