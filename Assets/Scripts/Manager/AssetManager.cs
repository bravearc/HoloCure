using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AssetManager : MonoBehaviour
{
    public Dictionary<string, AssetBundle> Asset { get; private set; }
    public Dictionary<string, AudioClip> Sound { get; private set; }
    public Dictionary<string, Sprite> Sprite { get; private set; }
    public Dictionary<string, GameObject> Item { get; private set; }
    public Dictionary<string, GameObject> Object { get; private set; }

    public Dictionary<string, string> Text { get; private set; }


    public void Init()
    {

    }

    public AudioClip LoadAudioClip(string audio) => Load(Sound, string.Concat(Defind.Path.Audio, audio));
    public Sprite LoadSprite(string sprite) => Load(Sprite, string.Concat(Defind.Path.Sprite, sprite));
    public GameObject LoadChacter(string name, Transform tr = null) => Instantiate(string.Concat(Defind.Path.Character, name), tr);
    public GameObject LoadItem(string item, Transform tr = null) => Instantiate(string.Concat(Defind.Path.Item, item), tr);
    public GameObject LoadObject(string ob, Transform tr = null) => Instantiate(string.Concat(Defind.Path.Object, ob), tr);

    private T Load<T>(Dictionary<string, T>dic, string path, Transform tr = null) where T : Object
    {
        if (dic[path] != null)
        {
            return dic[path];
        }

        T t = ResourceObject<T>(path);
        dic.Add(path, t);
        return t; 
    }

    private T ResourceObject<T>(string path) where T : Object
    {
        T t = Resources.Load<T>(path);
        Instantiate(t);
        return t;
    }
    
    //경험치, 햄버거, 아이템박스, Skill Drop
    public GameObject Instantiate(string path, Transform tr = null)
    {
        GameObject obj = Resources.Load<GameObject>(path);
        Instantiate(obj, tr);
        return obj;

    }
}
