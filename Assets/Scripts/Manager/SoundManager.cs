using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    private AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.Max];
    public void Init()
    {
        string[] names = Enum.GetNames(typeof(Define.Sound));
        for (int i = 0; i < names.Length; i++)
        {
            GameObject go = new GameObject(names[i]);
            _audioSources[i] = go.AddComponent<AudioSource>();
            go.transform.parent = transform;
        }
        _audioSources[(int)Define.Sound.BGM].loop = true;
    }

    public void Play(Define.Sound type, string sound, int volume = 1)
    {
        AudioSource audioSource = _audioSources[(int)type];
        AudioClip clip = GetAudioClip(sound);

        if (type == Define.Sound.Effect)
        {
            _audioSources[(int)type].PlayOneShot(_audioSources[(int)type].clip);
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void Stop(Define.Sound type)
    {
        _audioSources[(int)type].Stop();
    }

    public void SoundScale(Define.Sound type, float volume) 
    { 
        AudioSource audioSource = _audioSources[(int)type];
        audioSource.volume = volume;
    }

    public void Clear()
    {
        foreach(AudioSource audioSource in _audioSources)
        {
            audioSource.Stop();
        }
    }

    public AudioClip GetAudioClip(string sound) => Manager.Asset.LoadAudioClip(sound);
}
