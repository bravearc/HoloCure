using UnityEngine;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioListener Listener;
    public AudioClip BGM;
    public AudioClip UISound;
    public AudioClip ClickSound;
    public List<AudioClip> ItemSound;
    public void Init()
    {
        Listener = gameObject.AddComponent<AudioListener>();
        ItemSound = new List<AudioClip>();
    }
    public void BGMStart()
    {

    }
}
