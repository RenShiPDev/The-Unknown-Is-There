using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ObjectSound : MonoBehaviour
{
    [SerializeField] private bool _is2D;
    [SerializeField] private float _soundMult = 1;

    private void Start()
    {
        if (_soundMult == 0)
        {
            _soundMult = 1;
        }
    }

    public void PlaySound()
    {
        GetComponent<AudioSource>().volume = SoundManager.Instance.Volume * _soundMult;
        if(_is2D)
        {
            GetComponent<AudioSource>().spatialBlend = 0;
        }
        else
        {
            GetComponent<AudioSource>().spatialBlend = 1;
        }
        GetComponent<AudioSource>().Play();
    }

    public void StopSound()
    {
        GetComponent<AudioSource>().Stop();
    }

}
