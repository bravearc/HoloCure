using System.Linq;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using System;
using static UnityEditor.Progress;

public class SpawnManager : MonoBehaviour
{
    private EnemyID enemyID;
    private const int WEAPON_MIN_IDX = 1000;
    private const int WEAPON_MAX_IDX = 1016;
    private const int EQUIPMENT_MIN_IDX = 2000;
    private const int EQUIPMENT_MAX_IDX = 2019;

    public GameObject EnemyRezen(int i)
    {
        EnemyID enemy = (EnemyID)i;
        return Manager.Asset.LoadObject(enemy.ToString());

    }

    public T RandomItem<T>() where T : IItem
    {
        IItem item;
        if (typeof(T) == typeof(WeaponData))
        {
            item = Manager.Game.Inventory.IsItemTypeFull<WeaponData>()
                ? GetItem<WeaponData>()
                : GetItem<WeaponData>(Manager.Game.Inventory.Weapon);
        }
        else
        {
            item = Manager.Game.Inventory.IsItemTypeFull<EquipmentData>()
                   ? GetItem<EquipmentData>()
                   : GetItem<EquipmentData>(Manager.Game.Inventory.Equipment);
        }
        return (T)item;
    }

    public T GetItem<T>()
    {
        if (typeof(T) == typeof(WeaponData))
            return Utils.Shuffle<T>(WEAPON_MIN_IDX, WEAPON_MAX_IDX);
        else if (typeof(T) == typeof(EquipmentData))
            return Utils.Shuffle<T>(EQUIPMENT_MIN_IDX, EQUIPMENT_MAX_IDX);
        else
            throw new ArgumentException("Unsupported type");
    }

    public T GetItem<T>(List<T> t)
    {
        return t[Utils.Shuffle<int>(0, t.Count)];
    }
}
