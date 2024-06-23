using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class StarInfoGUI : MonoBehaviour
{
    public StarDrive Drive;
    public StarGUI SelectedStar;
    public PlanetGui SelectedPlanet;

    [SerializeField] private Image _planetImage;
    [SerializeField] private Sprite _noneSprite;

    [SerializeField] private GameObject _targetObj;

    [SerializeField] private TMP_Text _text;

    private StarGUI _playerStar;

    public void SetStartText(StarGUI starGUI)
    {
        _playerStar = FindObjectOfType<StarGUI>();

        foreach (var star in FindObjectsOfType<StarGUI>())
        {
            if(star.IsCurrent)
            {
                _playerStar = star;
            }
        }

        float distance = (_playerStar.transform.position-starGUI.transform.position).magnitude;

        SelectedStar = starGUI;

        _text.text = starGUI.SName;
        _text.text += "\n\n" + "Type: " + starGUI.SType;
        _text.text += "\n\n" + "Distance: " + (distance*10).ToString("0.00");
        _text.text += "\n\n" + "Start distance: " + (starGUI.Seed*3f * 10f).ToString("0.00");
        _text.text += "\n\n" + "Difficulity: " + starGUI.Difficulity;
    }

    public void SetPlanetText(PlanetGui planetGUI)
    {
        foreach (var star in FindObjectsOfType<StarGUI>())
        {
            if (star.IsCurrent)
            {
                _playerStar = star;
            }
        }
        float distance = (_playerStar.transform.position - planetGUI.transform.position).magnitude;

        SelectedPlanet = planetGUI;
        _text.text = _playerStar.SName + " - " + SelectedPlanet.PName;
        _text.text += "\n\n" + "Type: " + SelectedPlanet.PType;
        _text.text += "\n\n" + "Distance: " + (distance * 10).ToString("0.00");
        _text.text += "\n\n" + "Mass: " + SelectedPlanet.PMass + " t.";
        _text.text += "\n\n" + "Description: " + SelectedPlanet.PDescription;

        if(SelectedPlanet.IsVisited)
        {
            SetImage();
        }
        else
        {
            _planetImage.sprite = _noneSprite;
        }
    }

    public void SetImage()
    {
        _planetImage.sprite = SelectedPlanet.GetSprite();
        int randNum = SelectedStar.Seed * 111;
        _planetImage.color = new Color(GetNum(randNum,0), GetNum(randNum, 1), GetNum(randNum, 2));
    }

    private float GetNum(int num, int id)
    {
        return (num.ToString()[id])/5f + 0.5f;
    }
}
