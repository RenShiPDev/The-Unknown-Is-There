using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class SaveStar
{
    public int starSeed;
    public bool IsCurrent;
    public bool IsVisited;

    public void Init(StarGUI star)
    {
        starSeed = star.Seed;
        IsCurrent = star.IsCurrent;
        IsVisited = star.IsVisited;
    }
}

public class SaverManger : MonoBehaviour
{
    public static SaverManger Instance = null;

    public Dictionary<int, SaveStar> SavedStars = new Dictionary<int, SaveStar>();

    public Vector3 savingPos;
    public Vector3 savingRotation;
    public string savingName;
    public int savingId;

    [SerializeField] private GameObject _player;
    [SerializeField] private float _timeSpeed;

    private float _timer;

    private List<TakingObject> _takingObjects = new List<TakingObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadGame();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer > _timeSpeed)
        {
            _timer = 0;
            SaveGame();
        }
    }

    [ContextMenu("LoadGame")]
    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath
          + "/ObjectsSaves.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
              File.Open(Application.persistentDataPath
              + "/ObjectsSaves.dat", FileMode.Open);
            List<SaveData> data = (List<SaveData>)bf.Deserialize(file);
            file.Close();

            var CurObjects = FindObjectsByType<TakingObject>(FindObjectsSortMode.None).ToList();

            for(int i = 0; i < data.Count; i++)
            {
                savingPos = new Vector3(data[i].savedPosX, data[i].savedPosY, data[i].savedPosZ);
                savingRotation = new Vector3(data[i].savedRotationX, data[i].savedRotationY, data[i].savedRotationZ);
                savingName = data[i].savedName;
                savingId = data[i].savedId;
                
                foreach (TakingObject obj in Resources.LoadAll<TakingObject>("ShopObjects"))
                {
                    bool founded = false;
                    foreach(var sceneObj in CurObjects)
                    {
                        if(sceneObj.GetId() == obj.GetId() && sceneObj.IsSaving)
                        {
                            founded = true;
                            if (sceneObj.Killed || data[i].savedKilled)
                            {
                                sceneObj.gameObject.SetActive(false);
                            }
                        }
                    }

                    if (obj.GetName() == savingName && !founded && !data[i].savedKilled)
                    {
                        var clone = Instantiate(obj);
                        clone.transform.position = savingPos;
                        clone.transform.rotation = Quaternion.Euler(savingRotation);
                        clone.SetName(savingName);
                        clone.SetId(savingId+1);
                    }
                }
            }
            /*savingPos = data.savedPos;
            savingRotation = data.savedRotation;
            savingName = data.savedName;*/

            Vector3 pPos = Vector3.zero;
            Vector3 pRot = Vector3.zero;

            pPos.x = PlayerPrefs.GetFloat("pPosX");
            pPos.y = PlayerPrefs.GetFloat("pPosY");
            pPos.z = PlayerPrefs.GetFloat("pPosZ");
            _player.transform.position = pPos;

            pRot.x = PlayerPrefs.GetFloat("pRotX");
            pRot.y = PlayerPrefs.GetFloat("pRotY");
            pRot.z = PlayerPrefs.GetFloat("pRotZ");

            PlayerRotator.Instance.enabled = false;

            _player.transform.rotation = Quaternion.Euler(pRot);

            pRot.z = 0;
            pRot.x = pRot.y;
            pRot.y = 0;

            PlayerRotator.Instance.SetRotation(pRot);

            PlayerRotator.Instance.enabled = true;

        }
        else
        {
            Vector3 pRot = Vector3.zero;
            PlayerRotator.Instance.enabled = false;

            _player.transform.rotation = Quaternion.Euler(pRot);

            pRot.x = pRot.y+90f;

            PlayerRotator.Instance.SetRotation(pRot);
            PlayerRotator.Instance.enabled = true;
        }

        if (File.Exists(Application.persistentDataPath
          + "/StarsSaves.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
              File.Open(Application.persistentDataPath
              + "/StarsSaves.dat", FileMode.Open);

            List<SaveStar> stars = (List<SaveStar>)bf.Deserialize(file);
            foreach (SaveStar star in stars)
            {
                if (!SavedStars.TryGetValue(star.starSeed, out SaveStar savedStar))
                {
                    SavedStars.Add(star.starSeed, star);
                }
            }

            file.Close();
        }
    }

    [ContextMenu("SaveGame")]
    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
          + "/ObjectsSaves.dat");

        _takingObjects = FindObjectsOfType<TakingObject>(true).ToList();
        List<SaveData> data = new List<SaveData>();

        for (int i = 0; i < _takingObjects.Count; i++)
        {
            if (_takingObjects[i].IsSaving)
            {
                SaveData dat = new SaveData();

                Vector3 pos = _takingObjects[i].gameObject.transform.position;
                dat.savedPosX = pos.x;
                dat.savedPosY = pos.y;
                dat.savedPosZ = pos.z;

                Vector3 rot = _takingObjects[i].gameObject.transform.rotation.eulerAngles;
                dat.savedRotationX = rot.x;
                dat.savedRotationY = rot.y;
                dat.savedRotationZ = rot.z;

                dat.savedName = _takingObjects[i].GetName();
                dat.savedId = _takingObjects[i].GetId();
                dat.savedKilled = _takingObjects[i].Killed;
                Debug.Log(" " + dat.savedKilled + " ");

                data.Add(dat);
            }
        }
        bf.Serialize(file, data);
        file.Close();

        //PlayerSaving
        Vector3 pPos = _player.transform.position;
        Vector3 pRot = _player.transform.rotation.eulerAngles;

        PlayerPrefs.SetFloat("pPosX", pPos.x);
        PlayerPrefs.SetFloat("pPosY", pPos.y);
        PlayerPrefs.SetFloat("pPosZ", pPos.z);

        PlayerPrefs.SetFloat("pRotX", pRot.x);
        PlayerPrefs.SetFloat("pRotY", pRot.y);
        PlayerPrefs.SetFloat("pRotZ", pRot.z);

        //StarsSaving
        List<SaveStar> stars = new List<SaveStar>();
        foreach(var starDic in SavedStars)
        {
            SaveStar star = starDic.Value;
            stars.Add(star);
        }

        bf = new BinaryFormatter();
        file = File.Create(Application.persistentDataPath
          + "/StarsSaves.dat");

        bf.Serialize(file, stars);
        file.Close();
    }

    public void SpecialSave(SpecialObjectSaver obj)
    {
        Vector3 oPos = obj.transform.position;
        Vector3 oRot = obj.transform.rotation.eulerAngles;

        PlayerPrefs.SetFloat(obj.Name + obj.ID + "PosX", oPos.x);
        PlayerPrefs.SetFloat(obj.Name + obj.ID + "PosY", oPos.y);
        PlayerPrefs.SetFloat(obj.Name + obj.ID + "PosZ", oPos.z);

        PlayerPrefs.SetFloat(obj.Name + obj.ID + "RotX", oRot.x);
        PlayerPrefs.SetFloat(obj.Name + obj.ID + "RotY", oRot.y);
        PlayerPrefs.SetFloat(obj.Name + obj.ID + "RotZ", oRot.z);
    }


    public void AddMoney(int money) => SaveMoney(money + GetMoney());
    public void SaveMoney(int money) => PlayerPrefs.SetInt("Money", money);
    public int GetMoney() => PlayerPrefs.GetInt("Money");


    [ContextMenu("ResetData")]
    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath
          + "/ObjectsSaves.dat"))
        {
            File.Delete(Application.persistentDataPath
              + "/ObjectsSaves.dat");
            savingPos = Vector3.zero;
            savingRotation = Vector3.zero;
            savingName = "";
        }

        if (File.Exists(Application.persistentDataPath
          + "/StarsSaves.dat"))
        {
            File.Delete(Application.persistentDataPath
              + "/StarsSaves.dat");
            savingPos = Vector3.zero;
            savingRotation = Vector3.zero;
            savingName = "";
        }

        PlayerPrefs.DeleteAll();
    }

    public void AddStar(StarGUI star)
    {
        var savestar = new SaveStar();
        savestar.starSeed = star.Seed;
        savestar.IsVisited = star.IsVisited;
        savestar.IsCurrent = star.IsCurrent;

        SavedStars.Add(star.Seed, savestar);
    }

    [Serializable]
    class SaveData
    {
        public float savedPosX;
        public float savedPosY;
        public float savedPosZ;

        public float savedRotationX;
        public float savedRotationY;
        public float savedRotationZ;

        public string savedName;
        public int savedId;
        public bool savedKilled;
    }
}
