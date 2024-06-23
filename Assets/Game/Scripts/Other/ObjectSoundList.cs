using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ObjectSoundList : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _clipList;

    [SerializeField] private float _soundMult = 1;

    [SerializeField] private bool _noRandom;

    private int _clipId;

    private void Start()
    {
        if(_soundMult == 0)
        {
            _soundMult = 1;
        } 
    }

    public void PlaySound()
    {
        GetComponent<AudioSource>().volume = SoundManager.Instance.Volume * _soundMult;

        if (!_noRandom)
        {
            GetComponent<AudioSource>().clip = _clipList[Random.Range(0, _clipList.Count)];
        }
        else
        {
            GetComponent<AudioSource>().clip = _clipList[_clipId];
            _clipId++;
            if(_clipId >= _clipList.Count)
            {
                _clipId = 0;
            }
        }

        GetComponent<AudioSource>().Play();
    }

}
