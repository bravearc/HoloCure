using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Transform[] _sponPoint;
    float _timer;
    float _monsterRegen = 15f;
    GameObject _enemys;

    private void Start()
    {
        _sponPoint = new Transform[transform.childCount];
        
        for (int i = 0; i < _sponPoint.Length; ++i)
        {
            _sponPoint[i] = transform.GetChild(i);
        }
        _enemys = new GameObject(nameof(_enemys));
    }

    private void Update()
    {
        _timer += Time.fixedDeltaTime;
        if( _timer > _monsterRegen) 
        {
            RandomSpawn();
            _timer = 0;
        }
    }
    public void ChangeMap(Transform mainMap)
    {
        transform.position = mainMap.position;
    }

    private void RandomSpawn()
    {
        GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Enemy.prefab");

        int randomInt = Random.Range(0, _sponPoint.Length);
        GameObject spawnedEnemy = Instantiate(go, _sponPoint[randomInt].position, Quaternion.identity);
        spawnedEnemy.transform.SetParent(_enemys.transform, false);
    }

}
