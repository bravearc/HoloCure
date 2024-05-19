using UnityEngine;


public class Managers : MonoBehaviour
{
    private static Managers Instance;
    public static DataManager Data { get; private set; }
    public static SoundManager Sound { get; private set; }
    public static SpawnManager Spawn { get; private set; }
    public static ResourceManager Resource { get; private set; }
    public static UIManager UI { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //Data = new DataManager();
        //UI = new UIManager();
        //Resource = new ResourceManager();

        GameObject go;

        go = new GameObject(nameof(SoundManager));
        go.transform.parent = transform;
        Sound = go.AddComponent<SoundManager>();

        go = new GameObject(nameof(SpawnManager));
        go.transform.parent = transform;
        Spawn = go.AddComponent<SpawnManager>();

        go = new GameObject(nameof(DataManager));
        go.transform.parent = transform;
        Data = go.AddComponent<DataManager>();

        go = new GameObject(nameof(ResourceManager));
        go.transform.parent = transform;
        Resource = go.AddComponent<ResourceManager>();

        go = new GameObject(nameof(UIManager));
        go.transform.parent = transform;
        UI = go.AddComponent<UIManager>();

        //Data.Init();
        //UI.Init();
        //Resource.Init();
        //Item.Init();
        //Dunjeon.Init();
        //Spawn.Init();
        //Sound.Init();
        //UI.Init(PlayerStats);
    }
}