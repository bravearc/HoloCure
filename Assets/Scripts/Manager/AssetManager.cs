using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public Dictionary<string, AssetBundle> Asset { get; private set; }
    public Dictionary<string, GameObject> Character { get; private set; }
    public Dictionary<string, AudioClip> Sound { get; private set; }
    public Dictionary<string, Sprite> Sprite { get; private set; }


    public void Init()
    {

    }

    private T Load<T>(Dictionary<string, T>dic, string path) where T : AssetBundle
    {
        if (dic[path] != null)
        {
            return dic[path];
        }

        T t = dic[path];
        return t; 
    }
}
