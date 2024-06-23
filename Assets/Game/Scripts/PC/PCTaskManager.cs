using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PCTaskManager : MonoBehaviour
{
    public UnityEvent OnComplete;

    [SerializeField] private PCTextShower _shower;
    [SerializeField] private TMP_Text _memeticText;

    private List<ModifierPC> _modifiers = new List<ModifierPC>();

    private int _taskID;
    private int _memeticPoints;
    private int _modifier;

    private char _targetChar;
    private string _targetString;

    private float _timer;
    private float _holdTimer;

    private void Start()
    {
        SetModifier();

        _memeticPoints = SaverManger.Instance.GetMoney();
        _memeticText.text = "Memetic points: " + _memeticPoints;
        OnComplete?.Invoke();
    }

    public void SetModifier()
    {
        _modifiers = FindObjectsByType<ModifierPC>(FindObjectsSortMode.None).ToList<ModifierPC>();
        _modifier = 0;
        foreach (ModifierPC modifier in _modifiers)
        {
            if (modifier.GetActive() == 1)
            {
                _modifier += 1;
            }
        }
    }

    public void CheckMessage(string message)
    {
        if (message == "start")
        {
            _memeticPoints = SaverManger.Instance.GetMoney();
            _shower.ResetText();
            _shower.WriteString("Task started.");
            StartNewTask();
        }
    }

    public void CheckKey(char key)
    {
        if (_shower.IsTask)
        {
            if (_taskID == 1)
            {
                if (key == _targetChar)
                {
                    CompleteTask(_taskID);
                }
            }
            if(_taskID == 3)
            {
                if (key == _targetChar)
                {
                    _holdTimer += Time.deltaTime;

                    string str = "";
                    for(int i = 0; i < _holdTimer / 0.2f; i++)
                    {
                        str += "_";
                    }
                    _shower.ChangeString(str);
                    if(_holdTimer > 3f)
                    {
                        CompleteTask(_taskID*4);
                        _holdTimer = 0;
                    }
                }
            }
        }
    }

    public void CheckString(string str)
    {
        if (_shower.IsTask)
        {
            if (str == "exit")
            {
                EndTasks();
            }

            if (_taskID == 2)
            {
                if (str == _targetString)
                {
                    CompleteTask(_taskID+ _targetString.Length+3);
                }
            }
        }
    }

    public void UpdateMoneyText()
    {
        _memeticPoints = SaverManger.Instance.GetMoney();
        _memeticText.text = "Memetic points: " + _memeticPoints;
    }

    private void Update()
    {
        if (_shower.IsTask && _timer < 1)
        {
            _timer += Time.deltaTime;
        }

        if(_shower.IsTask && _taskID == 3)
        {
            if (_holdTimer > 0)
            {
                _holdTimer -= Time.deltaTime/3f;
                string str = "";
                for (int i = 0; i < _holdTimer / 0.2f; i++)
                {
                    str += "_";
                }
                _shower.ChangeString(str);
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape) && _timer > 0.5f)
        {
            EndTasks();
        }
    }

    private void CompleteTask(int taskID)
    {
        int points = Random.Range(1, 3) * taskID;

        int addingPoints = points * (int)Mathf.Pow(2, _modifier);
        //addingPoints = (int)(addingPoints * (1-PlayerHungryStats.Instance.GetValue()) * (1-PlayerMindStats.Instance.GetValue()));

        _memeticPoints += addingPoints;
        _memeticText.text = "Memetic points: " + _memeticPoints;

        SaverManger.Instance.SaveMoney(_memeticPoints);
        PlayerPrefs.SetInt("TotalMoney", PlayerPrefs.GetInt("TotalMoney") + addingPoints);
        OnComplete?.Invoke();

        _shower.WriteString("Task completed. You got - " + addingPoints);
        _shower.WriteString(" ");
        _shower.AcceptWriting = true;
        StartNewTask();
    }

    private void StartNewTask()
    {
        _shower.IsTask = true;

        var taskID = Random.Range(1, 4);
        switch (taskID)
        {
            case 1:
                StartTask1();
                break;
            case 2:
                StartTask2();
                break;
            case 3:
                StartTask3();
                break;
            default:
                StartTask1();
                break;
        }
    }

    private void StartTask1()
    {
        _taskID = 1;
        _targetChar = _shower.GetChars()[Random.Range(0, _shower.GetChars().Length - 1)];
        if(_targetChar == ' ')
        {
            _shower.WriteString("Press space");
        }
        else
        {
            _shower.WriteString("Press " + _targetChar);
        }
    }
    private void StartTask2()
    {
        _taskID = 2;

        _targetString = "";
        for (int i = 0; i < Random.Range(4,10); i++)
        {
            _targetString += _shower.GetChars()[Random.Range(0, _shower.GetChars().Length - 1)];
        }
        _targetString = _targetString.Replace(" ", "");
        _shower.WriteString("Type - " + _targetString);
    }

    private void StartTask3()
    {
        _taskID = 3;
        _targetChar = _shower.GetChars()[Random.Range(0, _shower.GetChars().Length - 1)];

        _shower.AcceptWriting = false;

        if (_targetChar == ' ')
        {
            _shower.WriteString("Hold space");
        }
        else
        {
            _shower.WriteString("Hold " + _targetChar);
        }
    }

    private void EndTasks()
    {
        _shower.WriteString("Task ended.");
        _shower.IsTask = false;
        _timer = 0;
        _holdTimer = 0;
        _shower.AcceptWriting = true;
    }
}
