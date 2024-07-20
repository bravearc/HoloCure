using UnityEditor;
using UnityEngine;
using UniRx.Triggers;
using UniRx;
using System;
using static System.Collections.Specialized.BitVector32;
using System.ComponentModel;

public class EnemyController : MonoBehaviour
{
    Transform[] _sponPoint;
    GameObject _enemys;
    float _timer;
    float _monsterRegen = 15;
    int _minutes = 0;

    void Start() => Init();

    void Init()
    {
        _sponPoint = new Transform[transform.childCount];

        for (int i = 0; i < _sponPoint.Length; ++i)
        {
            _sponPoint[i] = transform.GetChild(i);
        }
        _enemys = new GameObject(nameof(_enemys));

        Manager.Game.ElapsedTime.Subscribe(TimerUpdate).AddTo(this);
        this.UpdateAsObservable().Subscribe(_ => EnemyUpdate());
    }
    void TimerUpdate(TimeSpan time)
    {
        if (time.Minutes > _minutes)
        {
            _minutes = time.Minutes;
            _monsterRegen -= 0.5f;
        }
    }
    void EnemyUpdate()
    {
        _timer += Time.fixedDeltaTime;
        if( _timer > _monsterRegen) 
        {
            RandomSpawn();
            _timer = 0;
        }
    }
    public void ChangeEnemyRezenPosition(Transform mainMap)
    {
        transform.position = mainMap.position;
    }

    private void RandomSpawn()
    {
        GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Enemy.prefab");

        int randomInt = UnityEngine.Random.Range(0, _sponPoint.Length);
        GameObject spawnedEnemy = Instantiate(go, _sponPoint[randomInt].position, Quaternion.identity);
        spawnedEnemy.transform.SetParent(_enemys.transform, false);
    }

}
