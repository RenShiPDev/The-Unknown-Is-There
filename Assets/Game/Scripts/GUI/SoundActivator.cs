using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundActivator : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _sounds = new List<AudioClip>();

    private int soundId;

    public void PlaySound()
    {
            int newsoundId = Random.Range(0, _sounds.Count);
            if (newsoundId == soundId)
            {
                newsoundId = Random.Range(0, _sounds.Count);
            }
            soundId = newsoundId;

            SoundManager.Instance.PlaySound(_sounds[soundId]);
    }

    public void StopSound()
    {
        SoundManager.Instance.StopSound();
    }
}
