using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Util.Pool;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject _objectContainer { get; private set; }
    public GameObject _boxEffectContainer { get; private set; }
    public GameObject _enemyContainer { get; private set; }
    public GameObject _attackContainer { get; private set; }
    public GameObject _expContainer { get; private set; }
    public Pool<Exp> Exp { get; private set; }
    public Pool<Enemy> Enemy { get; private set; }
    public Pool<Attack> Attack { get; private set; }
    public Pool<DamageText> DamageText { get; private set; }
    public Pool<Impact> Impect { get; private set; }
    public Pool<Hamburger> Hamburger { get; private set; }
    public Pool<ItemBox> ItemBox { get; private set; }

    private const int ENEMY_SPAWN_OFFSET_COUNT = 36;
    private const int WIDTH = 15;
    private const int HEIGHT = 10;

    private Vector2[] _spawnPositions;


    public void Init()
    {
        _objectContainer = new GameObject("Object Container");
        _boxEffectContainer = new GameObject("BoxEffect Container");
        _enemyContainer = new GameObject("Enemy Container");
        _attackContainer = new GameObject("Attack Container");
        _expContainer = new GameObject("Exp Container");

        _boxEffectContainer.transform.parent = _objectContainer.transform;
        _enemyContainer.transform.parent = _objectContainer.transform;
        _attackContainer.transform.parent = _objectContainer.transform;
        _expContainer.transform.parent = _objectContainer.transform;

    }

    public void SetSpawn()
    {
        Enemy = new Pool<Enemy>();
        DamageText = new Pool<DamageText>();
        Exp = new Pool<Exp>();
        Attack = new Pool<Attack>();
        Impect = new Pool<Impact>();
        ItemBox = new Pool<ItemBox>();

        DamageText.Init(_attackContainer);
        Enemy.Init(_enemyContainer);
        Exp.Init(_expContainer);
        Attack.Init(_attackContainer);
        Impect.Init(_boxEffectContainer);
        ItemBox.Init(_expContainer);

        SetEnemySpawnPosition();
    }

    void SetEnemySpawnPosition()
    {
        _spawnPositions = new Vector2[ENEMY_SPAWN_OFFSET_COUNT];
        for(int idx = 0; idx < ENEMY_SPAWN_OFFSET_COUNT; idx++) 
        {
            float angleDiv = 360 / ENEMY_SPAWN_OFFSET_COUNT;
            float angle = idx * angleDiv * Mathf.Rad2Deg;
            _spawnPositions[idx] = new Vector2(WIDTH * Mathf.Cos(angle), HEIGHT * Mathf.Sin(angle));
        }
    }
    public Attack GetAttack()
    {
        return Attack.Get();
    }
    public void EnemyReward(Transform newPos)
    {
        Exp exp = Exp.Get();
        int type = Random.Range(1, 5);
        switch (type)
        {
            case 1:
                exp.Init(newPos, ExpType.Hamburger);
                break;
            default:
                exp.Init(newPos, ExpType.Exp);
                break;
        }
    }

    public void BossReward(Transform newPos)
    {
        ItemBox box = ItemBox.Get();
        box.Init(newPos);
    }

    public void SpawnDamageText(float damage, Vector2 newPos, DamageType type)
    {
        DamageText damageText = DamageText.Get();
        damageText.Init(damage, newPos, type);
    }
    public GameObject GetBoss(int i)
    {
        EnemyID enemy = (EnemyID)i;
        return Manager.Asset.InstantiateObject(enemy.ToString());
    }

    public ItemID GetRandomItem()
    {
        if(Manager.Game.Inventory.IsInventoryFull())
        {
            return GetInventoryItem();
        }

        int getType = Random.Range(1, 3);
        ItemID id = getType switch
        {
            1 => (ItemID)Random.Range((int)Define.ItemNumber.Weapon_Start, (int)Define.ItemNumber.Weapon_End),
            2 => GetInventoryItem(),
            3 => (ItemID)Random.Range((int)Define.ItemNumber.Equipment_Start, (int)Define.ItemNumber.Equipment_End),
            4 => (ItemID)Random.Range((int)Define.ItemNumber.Stats_Start, (int)Define.ItemNumber.Stats_End),
            _ => throw new NotImplementedException()
        };

        return id;
    }

    public ItemID GetInventoryItem()
    {
        ItemID newItem;
        int random = Random.Range(1, 2);
        if (random == 1)
        {
            List<Weapon> list = Manager.Game.Inventory.Weapons;
            newItem = list[Random.Range(0, list.Count)].ID;
        }
        else if (random == 2)
        {
            List<Equipment> list = Manager.Game.Inventory.Equipments;
            newItem = list[Random.Range(0, list.Count)].ID;
        }
        else
        {
            newItem = (ItemID)Random.Range((int)Define.ItemNumber.Stats_Start, (int)Define.ItemNumber.Stats_End);
        }
        return newItem;
    }
    public void PoolReset()
    {
        Enemy.Dispose();
        DamageText.Dispose();
        Exp.Dispose();
        Attack.Dispose();
        Impect.Dispose();

        foreach(Transform tr in _objectContainer.transform)
        {
            foreach(Transform child in tr)
            {
                Manager.Asset.Destroy(child.gameObject);
            }
        }
    }
    public Vector2 GetRandomPosition => _spawnPositions[Random.Range(0, ENEMY_SPAWN_OFFSET_COUNT)];
}
