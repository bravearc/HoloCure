using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject GetEnemy(int i)
    {
        EnemyID enemy = (EnemyID)i;
        return Manager.Asset.LoadObject(enemy.ToString());
    }

    public GameObject GetBoss(int i)
    {
        EnemyID enemy = (EnemyID)i;
        return Manager.Asset.LoadObject(enemy.ToString());
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
}
