using System;
using System.Collections.Generic;
using UnityEngine;
using Type = System.Type;


public class Inventory : MonoBehaviour
{
    Dictionary<Type, IList<IItem>> _itemLists;
    public List<WeaponData> Weapon = new();
    public List<EquipmentData> Equipment = new();

    private const int INVENTORY_MAX_COUNT = 6;
    private const int WEAPON_MIN_NUMBER = 0;
    private const int WEAPON_MAX_NUMBER = 999;
    private const int EQUIPMENT_MIN_NUMBER = 1000;
    private const int EQUIPMENT_MAX_NUMBER = 1999;
    private const int STAT_NUMBER = 3000;

    public Inventory()
    {
        _itemLists = new Dictionary<Type, IList<IItem>>
        {
            { typeof(WeaponData), Weapon as IList<IItem> },
            { typeof(EquipmentData), Equipment as IList<IItem> }
        };
    }

    public bool IsItemTypeFull<T>() where T : IItem
    {
        if (typeof(T) == typeof(WeaponData))
            return Weapon.Count < INVENTORY_MAX_COUNT;
        else if (typeof(T) == typeof(EquipmentData))
            return Equipment.Count < INVENTORY_MAX_COUNT;
        else
            throw new ArgumentException("Unsupported type");
    }
    public bool IsItemAllFull()
    {
        if (Weapon.Count < INVENTORY_MAX_COUNT) 
            return true;
        else if(Equipment.Count < INVENTORY_MAX_COUNT)
            return true;
        return false;
    }

    public T FindExiItem<T>(IItem newItem) where T : IItem
    {
        IList<T> list;
        string name = newItem.Name;
        if (WEAPON_MIN_NUMBER < newItem.ID && (int)newItem.ID < WEAPON_MAX_NUMBER)
        {
            list = (IList<T>)_itemLists[typeof(WeaponData)] as IList<T>;
            return FindExiItem<T>(list, name);
        }
        else
        {
            list = (IList<T>)_itemLists[typeof(EquipmentData)] as IList<T>;
            return FindExiItem<T>(list, name);
        }
    }
    private T FindExiItem<T>(IList<T> list, string name) where T : IItem
    {
        foreach (T item in list) 
        {
            if (item.ToString() == name)
            {
                return item;
            }
        }
        return default(T);
    }

    public T ItemNextLevel<T>(IItem newItem) where T : IItem
    {
        int levelUp = 1;
        List<T> list = typeof(T) == typeof(WeaponData)
            ? Manager.Data.Weapon[newItem.ID] as List<T>
            : Manager.Data.Equipment[newItem.ID] as List<T>;

        for (int i = 0; i < list.Count; i++)
        {
            if (newItem.Level == list[i].Level)
            {
                return list[i + levelUp];
            }
        }

        return list[levelUp];
    }

    public void SetItem<T>(IItem newItem) where T : IItem
    {
        IList<T> list;
        if(newItem is WeaponData)
            list = (IList<T>)_itemLists[typeof(WeaponData)] as IList<T>;
        else
            list = (IList<T>)_itemLists[typeof(EquipmentData)] as IList<T>;

        for(int i = 0; i < list.Count; i++)
        {
            if (list[i].ID == newItem.ID)
            {
                list[i] = (T)newItem;
                return;
            }
        }

        list.Add((T)newItem);
    }
}
