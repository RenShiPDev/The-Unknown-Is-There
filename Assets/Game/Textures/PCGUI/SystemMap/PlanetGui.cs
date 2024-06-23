using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetGui : MonoBehaviour
{
    [SerializeField] public string PName;
    [SerializeField] public string PType;
    [SerializeField] public string PDescription;
    [SerializeField] public float PMass;

    [SerializeField] public string PSpecialText;

    [SerializeField] private List<Sprite> _defaultPlanetImages = new List<Sprite>();
    [SerializeField] private Sprite _planetImage;

    [SerializeField] private StarInfoGUI _infoGUI;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _name;

    [SerializeField] private int _nameSeed;
    [SerializeField] private int _price;

    [SerializeField] private Sprite _visited;
    [SerializeField] private Sprite _inactive;

    public bool IsVisited;

    private void Start()
    {
        _name.text = PName;
    }

    public void Init(int seed, StarInfoGUI infoGui)
    {
        _infoGUI = infoGui;
        if(PlayerPrefs.GetInt(seed.ToString()) == 1)
        {
            IsVisited = true;
            CheckVisit();
        }

        _nameSeed = seed;
        Random.InitState(_nameSeed);

        string newName = System.Convert.ToChar((Random.Range(65, 90))).ToString();
        newName += System.Convert.ToChar((Random.Range(97, 122))).ToString();
        newName += System.Convert.ToChar((Random.Range(97, 122))).ToString();
        newName += System.Convert.ToChar((Random.Range(97, 122))).ToString();
        newName += System.Convert.ToChar((Random.Range(97, 122))).ToString();

        if (PName == "0")
        {
            PName = newName;
        }
        else
        {
            PName += "-" + newName;
        }

        _name.text = PName;

        if(PType == "0")
        {
            List<string> planetTypesList = new List<string>()
            {"Rock", "Ice", "Metal", "Lava", "Gas", "Water", "Earchlike"};

            int id = 0;
            int mult = Random.Range(0, planetTypesList.Count);
            int checkRand = Random.Range(0, (int)Mathf.Pow(2, mult));

            if (checkRand == 1)
            {
                id = mult;
                _price = (int)Mathf.Pow(2, id-1) * (checkRand.ToString().Length+1)+1;
            }
            else
            {
                _price = (int)Mathf.Pow(2, id - 1) + 1;
            }

            //Debug.Log(planetTypesList[id] + " " + mult + " " + checkRand);
            PType = planetTypesList[id];
            PMass = id * Random.Range(10f, 20f);

            if (TryGetComponent(out SpecialPlanetObject planetObject))
            {
                _planetImage = planetObject.GetImage();
            }
            else
            {
                if (id < _defaultPlanetImages.Count)
                {
                    _planetImage = _defaultPlanetImages[id];
                }
            }
        }

        CheckVisit();
    }

    public int GetPrice()
    {
        return _price;
    }

    public int GetSeed()
    {
        return _nameSeed;
    }

    public Sprite GetSprite()
    {
        return _planetImage;
    }

    public Image GetImage()
    {
        return _image;
    }

    public void SetDescription(string text)
    {
        PDescription = text;
        ShowInfo();
    }

    public void ShowInfo()
    {
        _infoGUI.SetPlanetText(this);
    }

    public void ActivatePlanet()
    {
        _infoGUI.Drive.Select(this);
    }

    public void CheckVisit()
    {
        if (IsVisited)
        {
            _image.sprite = _planetImage;
            PlayerPrefs.SetInt(_nameSeed.ToString(), 1);
        }
        else
        {
            _image.sprite = _inactive;
        }
    }
}
