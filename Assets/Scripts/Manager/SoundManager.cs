using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] _audioSources = new AudioSource[(int)Define.SoundType.Max];
    public void Init()
    {
        string[] names = Enum.GetNames(typeof(Define.SoundType));
        for (int i = 0; i < (int)Define.SoundType.Max; i++)
        {
            GameObject go = new GameObject(names[i]);
            _audioSources[i] = go.AddComponent<AudioSource>();
            go.transform.parent = transform;
        }
        _audioSources[(int)Define.SoundType.BGM].loop = true;
    }

    public void Play(Define.SoundType type, string sound, int volume = 2)
    {
        AudioSource audioSource = _audioSources[(int)type];
        audioSource.volume = volume;
        AudioClip clip = GetAudioClip(sound);

        if (type == Define.SoundType.Effect)
        {
            _audioSources[(int)type].PlayOneShot(clip);
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

    public void Stop(Define.SoundType type)
    {
        _audioSources[(int)type].Stop();
    }

    public void SoundScale(Define.SoundType type, float volume) 
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
