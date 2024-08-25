using UnityEngine;
using UniRx.Triggers;
using UniRx;
using System;


public class EnemyController : MonoBehaviour
{
    EnemyID _enemyID;
    EnemyID _bossID = (EnemyID)1001;
    float _timer;
    float _monsterRegenTimer = 15;
    const int NEXT_ENEMY_TIME = 1;
    const int BOSS_REZEN_TIME = 10;
    IDisposable _disposable;

    void Start() => Init();

    void Init()
    {
        Manager.Game.PlayTimeMinute.Subscribe(EnemyRezenUpdate).AddTo(this);

        _disposable?.Dispose();
        _disposable = this.UpdateAsObservable().Subscribe(_ => EnemySpawn());
        _enemyID = (EnemyID)1;
    }
    void EnemyRezenUpdate(int minutes)
    {
        _monsterRegenTimer -= 0.3f;

        if (minutes == 0) return;

        if (minutes % NEXT_ENEMY_TIME == 0) 
        {
            BossSpawn(_enemyID, EnemyType.MiniBoss);
            ++_enemyID;
        }

        if (minutes % BOSS_REZEN_TIME == 0)
        {
            BossSpawn(_bossID, EnemyType.Boss);
            ++_bossID;
        }
    }
    void EnemySpawn()
    {
        _timer += Time.fixedDeltaTime;
        if (_timer > _monsterRegenTimer)
        {
            Enemy enemy = Manager.Spawn.Enemy.Get();
            enemy.Init(_enemyID, Manager.Spawn.GetRandomPosition, EnemyType.Normal);
            _timer = 0;
        }
    }
    void BossSpawn(EnemyID id, EnemyType type)
    {
        Enemy boss = Manager.Spawn.Enemy.Get();
        boss.Init(id, Manager.Spawn.GetRandomPosition, type);

        if (type == EnemyType.Boss)
        {
            string bossName = Manager.Data.Enemy[id].Name;
            GameObject bossOb = Manager.Asset.LoadObject(bossName);
            Boss_Base go = Manager.Asset.Instantiate(bossOb, boss.transform).GetComponent<Boss_Base>();
            go.Init(id);
            go.transform.parent = boss.transform;
        }
    }
}
