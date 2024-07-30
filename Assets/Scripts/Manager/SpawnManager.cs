using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Util.Pool;

public class SpawnManager : MonoBehaviour
{
    public GameObject _objectContainer { get; private set; }
    public GameObject _boxEffectContainer { get; private set; }
    public Pool<Exp> Exp { get; private set; }
    public Pool<Enemy> Enemy { get; private set; }
    public Pool<Attack> Attack { get; private set; }
    public Pool<DamageText> DamageText { get; private set; }

    private const int ENEMY_SPAWN_OFFSET_COUNT = 36;
    private const int WIDTH = 30;
    private const int HEIGHT = 20;

    private Vector2[] _spawnPositions;


    public void Init()
    {
        _objectContainer = new GameObject("Object Container");
        _boxEffectContainer = new GameObject("BoxEffect Container");
        _boxEffectContainer.transform.parent = _objectContainer.transform;
    }

    public void GameStartInit()
    {
        Enemy = new Pool<Enemy>();
        DamageText = new Pool<DamageText>();
        Exp = new Pool<Exp>();
        Attack = new Pool<Attack>();

        DamageText.Init(_objectContainer);
        Enemy.Init(_objectContainer);
        Exp.Init(_objectContainer);
        Attack.Init(_objectContainer);

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
    public void SpawnExp(Transform newPos)
    {
        Exp exp = Exp.Get();
        exp.Init(newPos);
    }

    public void SpawnEnemy(EnemyID id)
    {
        Enemy enemy = Enemy.Get();
        enemy.Init(id, GetRandomPosition);
    }

    public void SpawnDamageText(float damage, Vector2 newPos)
    {
        DamageText damageText = DamageText.Get();
        damageText.Init(damage, newPos);
    }
    public GameObject GetBoss(int i)
    {
        EnemyID enemy = (EnemyID)i;
        return Manager.Asset.InstantiateObject(enemy.ToString());
    }

    public ItemID GetRandomItem()
    {
        int getType = Random.Range(1, 4);
        ItemID id = getType switch
        {
            1 => (ItemID)Random.Range((int)Define.ItemNumber.Weapon_Start, (int)Define.ItemNumber.Weapon_End),
            2 => (ItemID)Random.Range((int)Define.ItemNumber.Equipment_Start, (int)Define.ItemNumber.Equipment_End),
            3 => (ItemID)Random.Range((int)Define.ItemNumber.Stats_Start, (int)Define.ItemNumber.Stats_End),
            _ => throw new NotImplementedException()
        };

        return id;
    }

    public ItemID GetInventoryItem()
    {
        ItemID newItem;
        int random = Random.Range(1, 4);
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

    Vector2 GetRandomPosition => _spawnPositions[Random.Range(0, ENEMY_SPAWN_OFFSET_COUNT)];
}
