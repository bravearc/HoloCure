using UnityEngine;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using System;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    enum BGMList
    {
        Main,
        Play
    }


    private AudioListener _listener;
    private AudioClip _bgm;
    private AudioClip _mouseSound;
    private AudioClip ClickSound;
    private List<AudioClip> ItemSound;
    public void Init()
    {
        _listener = gameObject.AddComponent<AudioListener>();
        ItemSound = new List<AudioClip>();        
    }

    public void BGMChange(int i)
    {

    }
}
