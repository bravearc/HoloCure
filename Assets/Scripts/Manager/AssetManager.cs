using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public Dictionary<string, AssetBundle> Asset { get; private set; }
    public Dictionary<string, AudioClip> Sound { get; private set; }
    public Dictionary<string, Sprite> Sprite { get; private set; }
    public Dictionary<string, GameObject> Object { get; private set; }
    public Dictionary<string, string> Text { get; private set; }
    public Dictionary<string, AnimationClip> AnimClip { get; private set; }
    public Dictionary<string, Material> Material { get; private set; }

    AtlasLoader _atlasLoader;

    public void Init()
    {
        Asset = new();
        Sound = new();
        Sprite = new();
        Object = new();
        Text = new();
        AnimClip = new();
        Material = new();
        
        _atlasLoader = Utils.GetOrAddComponent<AtlasLoader>(gameObject);

        AddAssetBundle<AudioClip>();
        AddAssetBundle<Sprite>();
        AddAssetBundle<GameObject>();
        AddAssetBundle<AnimationClip>();
        AddAssetBundle<Material>();


    }

    public AudioClip LoadAudioClip(string audio) => Load(Sound, audio);
    public Sprite LoadSprite(string sprite) => Load(Sprite, sprite);
    public GameObject LoadObject(string ob) => Load(Object, ob);
    public Material LoadMaterial(string mat) => Load(Material, mat);
    public AnimationClip LoadAnimClip(string ani) => Load(AnimClip, ani);
    public GameObject InstantiateObject(string ob, Transform trans = null) => Instantiate(ob
        , trans);
    public T Load<T>(Dictionary<string, T>dic, string path) where T : Object
    {
        if (false == dic.ContainsKey(path))
        {
            T asset = Asset[GetNameofT<T>()].LoadAsset<T>(path);
            dic.Add(path, asset);
            return dic[path];
        }
        return dic[path]; 
    }

    public GameObject Instantiate(string path, Transform tr = null)
    {
        GameObject obj = Asset["GameObject"].LoadAsset<GameObject>(path);
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

    void AddAssetBundle<T>()
    {
        string assetPath = string.Concat("Bundle/", GetNameofT<T>());
        AssetBundle asset = AssetBundle.LoadFromFile(assetPath);
        Asset.Add(GetNameofT<T>(), asset);
    }
    string GetNameofT<T>()
    {
        if (typeof(T) == typeof(GameObject))
            return "GameObject";
        if (typeof(T) == typeof(Sprite))
            return "Sprite";
        if (typeof(T) == typeof(AudioClip))
            return "Sound";
        if (typeof(T) == typeof(Material))
            return "Material";
        if (typeof(T) == typeof(AnimationClip))
            return "Animation";
        return null;
    }
}
