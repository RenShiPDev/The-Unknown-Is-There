using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class PlanetMapMover : MonoBehaviour
{
    public UnityEvent OnSpaceKey;

    [SerializeField] private List<SpecialPlanetObject> _normalObjects = new List<SpecialPlanetObject>();
    [SerializeField] private List<SpecialPlanetObject> _uncommonObjects = new List<SpecialPlanetObject>();
    [SerializeField] private int _normalVariety;
    [SerializeField] private int _uncommonVariety;

    [SerializeField] private ScanGames _scanGames;
    [SerializeField] private PCCameraMover _pCCameraMover;

    [SerializeField] private StarMapMover _starMapMover;

    [SerializeField] private StarInfoGUI _infoGUI;
    [SerializeField] private GameObject _map;

    [SerializeField] private StarGUI _starGUI;

    [SerializeField] private GameObject _planetPrefab;
    [SerializeField] private GameObject _starPrefab;
    [SerializeField] private Transform _spawnPos;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _seed;

    private List<GameObject> _spawned = new List<GameObject>();

    private void Start()
    {
        //SpawnPlanets();
    }

    public void SpawnPlanets()
    {
        SpawnStar();

        int planetRand1 = Random.Range(0, 4);
        int planetRand2 = Random.Range(0, 4);

        int idSpawn = 0;

        GameObject prefab = _planetPrefab;

        int starCount = _starMapMover.GetStarCount();

        bool normal = true;
        bool uncommon = true;

        for (int i = (-(starCount/2) + planetRand1); i < ((starCount/2) - planetRand2); i++)
        {
            prefab = _planetPrefab;
            if (Random.Range(0, _normalVariety) == 1 && normal)
            {
                prefab = _normalObjects[Random.Range(0, _normalObjects.Count - 1)].gameObject;
                normal = false;
            }
            if (Random.Range(0, _uncommonVariety) == (int)(_uncommonVariety/2) && uncommon)
            {
                prefab = _uncommonObjects[Random.Range(0, _uncommonObjects.Count - 1)].gameObject;
                uncommon = false;
            }

            var clone = Instantiate(prefab, _spawnPos);

            int id = _infoGUI.Drive.CurrentStar.Seed*1000 + _seed + starCount + idSpawn;
            clone.GetComponent<PlanetGui>().Init(id, _infoGUI);
            //clone.GetComponent<PlanetGui>().SetDescription

            clone.transform.localPosition = RandomVector().normalized * (idSpawn+1) * 75f;


            Vector3 direction = _spawnPos.localPosition - clone.transform.localPosition;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            clone.GetComponent<PlanetGui>().GetImage().gameObject.transform.localRotation = Quaternion.Euler(0, 0, angle+45f+180f);


            _spawned.Add(clone.gameObject);

            idSpawn++;
        }
    }

    private void SpawnStar()
    {
        SaverManger.Instance.LoadGame();
        foreach (var obj in _spawned)
        {
            Destroy(obj);
        }

        if (_infoGUI.Drive.CurrentStar == null)
        {
            _starMapMover.InitFirstStar();
        }

        var starClone = Instantiate(_infoGUI.Drive.CurrentStar, _spawnPos);

        starClone.GetComponent<StarGUI>().Init(_infoGUI.Drive.CurrentStar.Seed, _infoGUI);
        starClone.transform.position = _spawnPos.transform.position;
        _seed = starClone.Seed;

        _starGUI = starClone.GetComponent<StarGUI>();
        _starGUI.enabled = true;
        _starGUI.GetComponent<Image>().enabled = true;
        _starGUI.CheckVisit();

        _spawned.Add(starClone.gameObject);
    }

    private void Update()
    {
        if (!_pCCameraMover.IsPos)
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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (gameObject.activeSelf && !_scanGames.IsActive && !_infoGUI.SelectedPlanet.IsVisited)
                {
                    _scanGames.gameObject.SetActive(true);
                    _scanGames.Activate();
                }
                OnSpaceKey?.Invoke();
            }
        }
    }

    public void Activate()
    {
        _infoGUI.SelectedPlanet.ActivatePlanet();
        _infoGUI.SetImage();
    }

    private Vector3 RandomVector()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
    }

}
