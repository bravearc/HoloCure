using UnityEditor;
using UnityEngine;
using UniRx.Triggers;
using UniRx;
using System;


public class EnemyController : MonoBehaviour
{
    float _timer;
    float _monsterRegenTimer = 15;
    int _minutes = 0;
    EnemyID _enemyID;
    const int NEXT_ENEMY = 3;


    void Start() => Init();

    void Init()
    {
        Manager.Game.ElapsedTime.Subscribe(TimerUpdate).AddTo(this);
        this.UpdateAsObservable().Subscribe(_ => EnemySpawn());
        _enemyID = (EnemyID)1;
    }
    void TimerUpdate(TimeSpan time)
    {
        if (time.Minutes > _minutes)
        {
            _minutes = time.Minutes;
            _monsterRegenTimer -= 0.5f;
        }

        if (time.Minutes > _minutes + NEXT_ENEMY) 
        {
            ++_enemyID;
        }
    }
    void EnemySpawn()
    {
        _timer += Time.fixedDeltaTime;
        if( _timer > _monsterRegenTimer) 
        {
            Manager.Spawn.SpawnEnemy(_enemyID);
            _timer = 0;
        }
    }
}
