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
        Init();
    }

    void Init()
    {
        Data = new DataManager();

        GameObject go;

        go = new GameObject(nameof(SoundManager));
        go.transform.parent = transform;
        Utils.GetOrAddComponent<SoundManager>(go);

        go = new GameObject(nameof(SpawnManager));
        go.transform.parent = transform;
        Utils.GetOrAddComponent<SpawnManager>(go);

        go = new GameObject(nameof(AssetManager));
        go.transform.parent = transform;
        Utils.GetOrAddComponent<AssetManager>(go);

        go = new GameObject(nameof(GameManager));
        go.transform.parent = transform;
        Utils.GetOrAddComponent<GameManager>(go);

        go = new GameObject(nameof(UIManager));
        go.transform.parent = transform;
        Utils.GetOrAddComponent<UIManager>(go);

    }
}
