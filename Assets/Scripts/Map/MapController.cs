using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    GameObject _player;
    /// <summary>
    /// 현재 플레이어가 위치한 맵
    /// </summary>
    public Transform MainMap;
    Transform[] _maps = new Transform[4];
    
    Collider2D _mapCol;
    float _mapSizeX;
    float _mapSizeY;

    private void Awake()
    {
        Init();
    }
    void Init()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            _maps[i] = transform.GetChild(i);
        }

        MainMap = _maps[0];
        _mapCol = MainMap.GetComponent<BoxCollider2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
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
                    MapSwap(); 
                    Debug.LogWarning("MapControllError");
                    break;
            }
            ++_count;
        }
    }
}

