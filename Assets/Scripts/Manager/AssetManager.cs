using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public Dictionary<string, AssetBundle> Asset { get; private set; }
    public Dictionary<string, AudioClip> Sound { get; private set; }
    public Dictionary<string, Sprite> Sprite { get; private set; }
    public Dictionary<string, GameObject> Object { get; private set; }
    public Dictionary<string, string> Text { get; private set; }
    public Dictionary<string, AnimationClip> AniClip { get; private set; }

    public void Init()
    {
        Asset = new();
        Sound = new();
        Sprite = new();
        Object = new();
        Text = new();
        AniClip = new();
    }

    public AudioClip LoadAudioClip(string audio) => Load(Sound, string.Concat(Define.Path.Audio, audio));
    public Sprite LoadSprite(string sprite) => Load(Sprite, string.Concat(Define.Path.Sprite, sprite));
    public GameObject LoadObject(string ob, Transform tr = null) => Instantiate(string.Concat(Define.Path.Object, ob), tr);

    public AnimationClip LoadAniClip(string ani) => Load(AniClip, string.Concat(Define.Path.Ani, ani));
    public T Load<T>(Dictionary<string, T>dic, string path, Transform tr = null) where T : Object
    {
        if (false == dic.ContainsKey(path))
        {
            T resource = Resources.Load<T>(path);
            dic.Add(path, resource);
            return dic[path];
        }
        return dic[path]; 
    }

    public GameObject Instantiate(string path, Transform tr = null)
    {
        GameObject obj = Resources.Load<GameObject>(path);
        return Instantiate(obj, tr);
    }

    public GameObject Instantiate(GameObject obj, Transform tr = null) 
    {
        GameObject go = UnityEngine.Object.Instantiate(obj, tr);
        go.name = obj.name;
        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null) { return; }

        UnityEngine.Object.Destroy(go);
    }
}
