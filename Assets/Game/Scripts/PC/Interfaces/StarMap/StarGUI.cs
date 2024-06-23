using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarGUI : MonoBehaviour
{
    [SerializeField] public string SName;
    [SerializeField] public string SType;
    [SerializeField] public int Difficulity;

    [SerializeField] private StarInfoGUI _infoGUI;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _name;

    [SerializeField] private Sprite _visited;
    [SerializeField] private Sprite _inactive;
    [SerializeField] private Sprite _current;

    [SerializeField] public int Seed;

    public bool IsCurrent;
    public bool IsVisited;

    private void Start()
    {
        //Init();
    }

    public void Init(int seed, StarInfoGUI gui)
    {
        _infoGUI = gui;

        Seed = seed;
        Random.InitState(Seed / 7);
        SName = System.Convert.ToChar((Random.Range(65, 90))).ToString();
        Random.InitState(Seed);
        SName += System.Convert.ToChar((Random.Range(97, 122))).ToString() + " ";
        SName += System.Convert.ToChar((Random.Range(97, 122))).ToString() + " ";
        SName += (Random.Range(1, 100)).ToString() + " ";
        SName += System.Convert.ToChar((Random.Range(97, 122))).ToString() + " ";
        SName += ((int)Mathf.Sqrt(Seed * 100000)).ToString();
        _name.text = SName;

        if (IsVisited)
        {
            foreach (var star in SaverManger.Instance.SavedStars)
            {
                if (star.Value.starSeed == Seed)
                {
                    IsCurrent = true;
                    CheckVisit();
                    return;
                }
            }
            IsCurrent = false;
            CheckVisit();
        }
    }

    public void Jump()
    {
        _infoGUI.Drive.Jump(this);
    }

    public void CheckVisit()
    {
        if (IsVisited)
        {
            if(IsCurrent)
            {
                _image.sprite = _current;
                _infoGUI.Drive.CurrentStar = this;
            }
            else
            {
                _image.sprite = _visited;
            }
        }
        else
        {
            _image.sprite = _inactive;
        }
    }

    public void ShowInfo()
    {
        _infoGUI.SetStartText(this);
    }

    public void SpawnPlanets()
    {
        
    }
}
