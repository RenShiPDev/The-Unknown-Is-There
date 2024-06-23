using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    public float Volume;
    public float MusicVolume;

    [SerializeField] private AudioSource _mainAudioSource;
    [SerializeField] private AudioSource _musicAudioSource;

    private float _timer;
    
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

        if (PlayerPrefs.GetInt("Showed") == 1)
        {
            Volume = PlayerPrefs.GetFloat("SoundValue");
        }
    }

    private void Start()
    {
        _mainAudioSource.volume = Volume;
        _musicAudioSource.volume = MusicVolume* Volume;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
    }
    
    public void PlayMusic(AudioClip clip)
    {
        if(!_musicAudioSource.isPlaying)
        {
            _musicAudioSource.clip = clip;
            _musicAudioSource.Play();
        }
    }

    public void ChangePlayMusic()
    {
        if (!_musicAudioSource.isPlaying)
        {
            _musicAudioSource.Play();
        }
        else
        {
            _musicAudioSource.Stop();
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (_timer > 0.01f)
        {
            _timer = 0;
            _mainAudioSource.PlayOneShot(clip);
        }
    }

    public void StopSound()
    {
        _mainAudioSource.Stop();
    }

    public void ChangeSound(float value)
    {
        Volume = value;
        _mainAudioSource.volume = Volume;
        _musicAudioSource.volume = MusicVolume* Volume;
        PlayerPrefs.SetFloat("SoundValue", value);
    }
}
