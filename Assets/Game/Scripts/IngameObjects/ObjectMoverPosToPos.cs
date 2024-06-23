using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoverPosToPos : MonoBehaviour
{
    [SerializeField] private List<Transform> _positions = new List<Transform>();
    [SerializeField] private float _speed;

    private int _curPos;

    private void Start()
    {
        if(_speed == 0)
        {
            _speed = 10;
        }
    }

    private void Update()
    {
        ChangePos();
    }

    private void ChangePos()
    {
        if ((gameObject.transform.position - _positions[_curPos].position).magnitude >= 0.1f)
        {
            gameObject.transform.position = Vector3.Slerp(gameObject.transform.position, _positions[_curPos].position, _speed * Time.deltaTime);
        }
        else
        {
            _curPos++;
            if(_curPos >= _positions.Count)
            {
                _curPos = 0;
            }
        }
        
    }
}
