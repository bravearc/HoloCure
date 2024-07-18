using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance;
    public static DataManager Data;
    public static SoundManager Sound;
    public static SpawnManager Spawn;
    public static AssetManager Asset;
    public static GameManager Game;
    public static UIManager UI;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        Init();
    }

    void Init()
    {
        Data = new DataManager();

        GameObject go;

        go = new GameObject(nameof(SoundManager));
        go.transform.parent = transform;
        Sound = Utils.GetOrAddComponent<SoundManager>(go);
        
        go = new GameObject(nameof(SpawnManager));
        go.transform.parent = transform;
        Spawn = Utils.GetOrAddComponent<SpawnManager>(go);

        go = new GameObject(nameof(AssetManager));
        go.transform.parent = transform;
        Asset = Utils.GetOrAddComponent<AssetManager>(go);

        go = new GameObject(nameof(GameManager));
        go.transform.parent = transform;
        Game = Utils.GetOrAddComponent<GameManager>(go);

        go = new GameObject(nameof(UIManager));
        go.transform.parent = transform;
        UI = Utils.GetOrAddComponent<UIManager>(go);

        Asset.Init();
        Data.Init();
        //Sound.Init();
        Game.Init();
    }

}
