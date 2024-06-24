using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance;
    public static DataManager Data;
    public static SoundManager Sound;
    public static SpawnManager Spawn;
    public static AssetManager Asset;
    public static UIManager UI;
    void Awake()
    {
        Init();
    }

    void Init()
    {
        Data = new DataManager();

        GameObject go;

        go = new GameObject(nameof(SoundManager));
        go.transform.parent = transform;
        go.AddComponent<SoundManager>();

        go = new GameObject(nameof(SpawnManager));
        go.transform.parent = transform;
        go.AddComponent<SpawnManager>();

        go = new GameObject(nameof(AssetManager));
        go.transform.parent = transform;
        go.AddComponent<AssetManager>();

        go = new GameObject(nameof(UIManager));
        go.transform.parent = transform;
        go.AddComponent<UIManager>();

    }
}
