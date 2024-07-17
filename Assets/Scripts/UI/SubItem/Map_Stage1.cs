using UnityEngine;

public class Map_Stage1 : MonoBehaviour
{
    public Transform MainMap;
    private Transform[] _maps = new Transform[4];
    private EnemyController _enemyController;
    private GameObject _character;
    private Collider2D _mapCol;
    private float _mapSizeX;
    private float _mapSizeY;

    private void Start() => Init();
    void Init()
    {
        _enemyController = Utils.GetOrAddComponent<EnemyController>(Manager.Asset.LoadObject("EnemyController"));

        _character = GameObject.FindWithTag("Character");

        for (int i = 0; i < transform.childCount; ++i)
        {
            _maps[i] = transform.GetChild(i).transform;
        }
        MainMap = _maps[0];
        
        _mapCol = Utils.GetOrAddComponent<BoxCollider2D>(MainMap.gameObject);
        _mapSizeX = _mapCol.bounds.extents.x;
        _mapSizeY = _mapCol.bounds.extents.y;
    }

    public void MapSwap()
    {
        Vector2 playerPos = _character.transform.position;
        Vector2 mainMapPos = MainMap.transform.position;

        int pathX = playerPos.x > mainMapPos.x ? 2 : -2;
        int pathY = playerPos.y > mainMapPos.y ? 2 : -2;

        float poX = _mapSizeX * pathX;
        float poY = _mapSizeY * pathY;

        float mainX = mainMapPos.x;
        float mainY = mainMapPos.y;

        int _count = 0;

        foreach (Transform t in _maps)
        {
            if (MainMap == t)
            {
                continue;
            }

            switch (_count)
            {
                case 0:
                    t.position = new Vector2(mainX + poX, mainY);
                    break;
                case 1:
                    t.position = new Vector2(mainX, mainY + poY);
                    break;
                case 2:
                    t.position = new Vector2(mainX + poX, mainY + poY);
                    break;
                default:
                    Debug.LogWarning("MapControllError");
                    MapSwap(); 
                    break;
            }
            ++_count;
        }
        _enemyController.ChangeMap(MainMap);
    }
}

