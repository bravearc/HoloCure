using UnityEngine;

public class SubItem_Map : UI_SubItem
{
    public Transform MainMap;
    public EnemyController enemyController;
    public GameObject _player;
    public Transform[] _maps = new Transform[4];
    public Collider2D _mapCol;
    public float _mapSizeX;
    public float _mapSizeY;

    protected override void Init()
    {
        enemyController = Manager.Asset.LoadObject("EnemyController").GetComponent<EnemyController>();
        _player = GameObject.FindWithTag("Player");
        MainMap = transform.Find("Map0");
        //base.Init();
        for (int i = 0; i < transform.childCount; ++i)
        {
            _maps[i] = transform.GetChild(i).transform;
        }

        _mapCol = Utils.GetOrAddComponent<BoxCollider2D>(MainMap.gameObject);
        _mapSizeX = _mapCol.bounds.extents.x;
        _mapSizeY = _mapCol.bounds.extents.y;
    }

    public void MapSwap()
    {
        Vector2 playerPos = _player.transform.position;
        Vector2 mainMapPos = MainMap.transform.position;

        int pathX = playerPos.x > mainMapPos.x ? 2 : -2;
        int pathY = playerPos.y > mainMapPos.y ? 2 : -2;

        float poX = _mapSizeX * pathX;
        float poY = _mapSizeY * pathY;

        int _count = 0;

        float mainX = mainMapPos.x;
        float mainY = mainMapPos.y;

        foreach (Transform t in this._maps)
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
        enemyController.ChangeMap(MainMap);
    }
}

