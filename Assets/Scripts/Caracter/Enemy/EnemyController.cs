using UnityEngine;
using UniRx.Triggers;
using UniRx;


public class EnemyController : MonoBehaviour
{
    float _timer;
    float _monsterRegenTimer = 15;
    int _minutes = 0;
    EnemyID _enemyID;
    const int NEXT_ENEMY_TIME = 1;
    int BOSS_TIME = 3;
    bool _isBossRezen;

    void Start() => Init();

    void Init()
    {
        Manager.Game.PlayTimeMinute.Subscribe(TimerUpdate).AddTo(this);
        this.UpdateAsObservable().Subscribe(_ => EnemySpawn());
        _enemyID = (EnemyID)1;
    }
    void TimerUpdate(int minutes)
    {
        _minutes = minutes;
        _monsterRegenTimer -= 0.5f;

        if (minutes == 0) return;

        if (minutes % NEXT_ENEMY_TIME == 0) 
        {
            _isBossRezen = true;
            ++_enemyID;
        }
    }
    void EnemySpawn()
    {
        _timer += Time.fixedDeltaTime;
        if (_timer > _monsterRegenTimer)
        {
            Enemy enemy = Manager.Spawn.Enemy.Get();
            enemy.Init(_enemyID, Manager.Spawn.GetRandomPosition, false);
            _timer = 0;

            if (_isBossRezen)
            {
                Enemy boss = Manager.Spawn.Enemy.Get();
                boss.Init(_enemyID, Manager.Spawn.GetRandomPosition, true);

                string bossName = Manager.Data.Enemy[_enemyID].Name;
                GameObject bossOb = Manager.Asset.LoadObject(bossName);
                GameObject go = Manager.Asset.Instantiate(bossOb);
                go.transform.parent = boss.transform;

                _isBossRezen = false;
            }
        }

    }
}
