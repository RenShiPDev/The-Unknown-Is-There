using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class StarMapMover : MonoBehaviour
{
    public UnityEvent OnSpaceKey;

    [SerializeField] private List<GameObject> _spawned = new List<GameObject>();

    [SerializeField] private PCCameraMover _pCCameraMover;
    [SerializeField] private StarInfoGUI _infoGUI;
    [SerializeField] private StarSureQuestion _starSureQuestion;

    [SerializeField] private GameObject _map;
    [SerializeField] private GameObject _starPrefab;
    [SerializeField] private Transform _spawnPos;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _starCount;
    [SerializeField] private int _seed;

    public bool IsActive;

    private void Start()
    {
        IsActive = true;
        SaverManger.Instance.LoadGame();

        //InitFirstStar();
        SpawnStars();
    }
    
    public void InitFirstStar()
    {
        if (_infoGUI.Drive.CurrentStar == null)
        {
            int id = _seed + _starCount / 2;

            foreach (var star in SaverManger.Instance.SavedStars)
            {
                if (star.Value.IsCurrent == true)
                {
                    _seed = star.Value.starSeed;

                    id = _seed;
                    if (_seed <= 0)
                    {
                        id = _seed + _starCount / 2;
                    }
                }
            }

            var clone = Instantiate(_starPrefab, _spawnPos);
            clone.GetComponent<StarGUI>().Init(id+10, _infoGUI);
            clone.transform.localPosition = RandomVector().normalized * (_starCount/2) * 75f;

            _spawned.Add(clone.gameObject);

            clone.GetComponent<StarGUI>().IsCurrent = true;
            clone.GetComponent<StarGUI>().IsVisited = true;
            clone.GetComponent<StarGUI>().CheckVisit();
            _infoGUI.Drive.CurrentStar = clone.GetComponent<StarGUI>();

            var savestar = new SaveStar();
            savestar.Init(_infoGUI.Drive.CurrentStar);

            bool staradded = false;
            foreach (var star in SaverManger.Instance.SavedStars)
            {
                if (star.Value.starSeed == _infoGUI.Drive.CurrentStar.Seed)
                {
                    staradded = true;
                }
            }
            if (!staradded)
            {
                SaverManger.Instance.SavedStars.Add(_infoGUI.Drive.CurrentStar.Seed, savestar);
            }
        }
    }

    [ContextMenu("SpawnStars")]
    public void SpawnStars()
    {
        SaverManger.Instance.LoadGame();


        InitFirstStar();

        foreach (var obj in _spawned)
        {
            Destroy(obj);
        }
        _seed = _infoGUI.Drive.CurrentStar.Seed;


        for (int i = -_starCount / 2; i < _starCount / 2; i++)
        {
            int id = _seed + i;

            if (id < 0)
            {
                continue;
            }

            var clone = Instantiate(_starPrefab, _spawnPos);
            clone.GetComponent<StarGUI>().Init(id, _infoGUI);

            clone.transform.localPosition = RandomVector().normalized * (i- _starCount / 2) * 75f;
            if (SaverManger.Instance.SavedStars.TryGetValue(id, out SaveStar savedStar))
            {
                clone.GetComponent<StarGUI>().IsCurrent = savedStar.IsCurrent;
                clone.GetComponent<StarGUI>().IsVisited = savedStar.IsVisited;
                clone.GetComponent<StarGUI>().CheckVisit();
            }
            _spawned.Add(clone.gameObject);
        }
    }

    public int GetStarCount()
    {
        return _starCount;
    }

    private void Update()
    {
        if (!_pCCameraMover.IsPos)
        {
            if (IsActive)
            {
                float localSpeed = _moveSpeed;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    localSpeed *= 3f;
                }

                if (Input.GetKey(KeyCode.S))
                {
                    _map.transform.localPosition += new Vector3(0, 1, 0) * localSpeed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    _map.transform.localPosition += new Vector3(0, -1, 0) * localSpeed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    _map.transform.localPosition += new Vector3(-1, 0, 0) * localSpeed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    _map.transform.localPosition += new Vector3(1, 0, 0) * localSpeed * Time.deltaTime;
                }

                if (Input.GetKey(KeyCode.Space))
                {
                    _starSureQuestion.gameObject.SetActive(true);
                    _starSureQuestion.SetActive(true);
                    OnSpaceKey?.Invoke();
                }
            }
        }
    }

    private Vector3 RandomVector()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
    }
}
