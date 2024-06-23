using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarDrive : MonoBehaviour
{
    public UnityEvent OnJump;

    public StarGUI CurrentStar;

    [SerializeField] private GameObject _pcGui;
    [SerializeField] private Image _tryImage;
    [SerializeField] private int _jumpCount;

    [SerializeField] private List<CasseteTaker> _takers = new List<CasseteTaker>();

    public float OffVariety;
    public float TimeToTryOff;

    private float _timer;
    private float _jumpID;

    public void Select(PlanetGui planet)
    {
        planet.IsVisited = true;
        planet.CheckVisit();
    }

    public void CheckJump()
    {
        if (_jumpID >= _jumpCount)
        {
            _jumpID = 0;
            ResetShip();
        }
        _tryImage.fillAmount = (float)_jumpID / (float)_jumpCount;
    }

    public void ActivatePC()
    {
        _pcGui.SetActive(true);
    }
    public void DectivatePC()
    {
        _pcGui.SetActive(false);
    }

    public void Jump(StarGUI currentStar)
    {
        SaveStar saveStar = new SaveStar();
        if (CurrentStar != null) 
        {
            CurrentStar.IsCurrent = false;
            CurrentStar.IsVisited = true;
            CurrentStar.CheckVisit();
            saveStar = new SaveStar();
            if(SaverManger.Instance.SavedStars == null)
            {
                SaverManger.Instance.SavedStars = new Dictionary<int, SaveStar>();
            }

            if (SaverManger.Instance.SavedStars.TryGetValue(CurrentStar.Seed, out SaveStar savedStar1))
            {
                SaverManger.Instance.SavedStars[savedStar1.starSeed].IsCurrent = false;
            }
            else
            {
                saveStar.IsCurrent = false;
                saveStar.IsVisited = true;
                saveStar.starSeed = CurrentStar.Seed;
                SaverManger.Instance.SavedStars.Add(CurrentStar.Seed, saveStar);
            }
        }

        saveStar = new SaveStar();
        if (SaverManger.Instance.SavedStars.TryGetValue(currentStar.Seed, out SaveStar savedStar))
        {
            SaverManger.Instance.SavedStars[savedStar.starSeed].IsCurrent = true;
        }
        else
        {
            saveStar.IsCurrent = true;
            saveStar.IsVisited = true;
            saveStar.starSeed = currentStar.Seed;
            SaverManger.Instance.SavedStars.Add(currentStar.Seed, saveStar);
        }

        SaverManger.Instance.SaveGame();

        CurrentStar = currentStar;

        CurrentStar.IsCurrent = true;
        CurrentStar.IsVisited = true;
        CurrentStar.CheckVisit();

        _jumpID += Random.Range(0.1f, 1f);
        OnJump?.Invoke();
    }

    [ContextMenu("DebugJump")]
    public void DebugJump()
    {
        OnJump?.Invoke();
    }

    public void ResetShip()
    {
        foreach(var obj in FindObjectsByType<StickButton>(FindObjectsSortMode.None))
        {
            obj.Init();
        }
        foreach (var obj in FindObjectsByType<RotatingButton>(FindObjectsSortMode.None))
        {
            obj.Init();
        }

        foreach(var obj in _takers)
        {
            obj.EraseCasset();
            obj.Drop();
        }
        _pcGui.SetActive(false);
        //SceneManager.LoadScene(0);
    }
}
