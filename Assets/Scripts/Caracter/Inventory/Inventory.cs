using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public List<Item> Weapon;
    public List<Item> Equipment;
    public List<Item> Stemp;
    
    private const int MAX_WEAPON = 6;
    private const int MAX_EQUIPMENT= 6;
    private const int MAX_STEMP = 3;

    
    private void Start()
    {
        Weapon = new List<Item>();
        Equipment = new List<Item>();
        Stemp = new List<Item>();
    }
    /// <summary>
    /// interface를 매개변수로 전달하여 해당 인벤토리가 가득 찼는지 확인할 수 있다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool IsItemFull<T>(T item)
    {
        switch(item)
        {
            case global::Weapon:
                return Weapon.Count < MAX_WEAPON ? true : false;
            case global::Equipment: 
                return Equipment.Count < MAX_EQUIPMENT ? true : false;
            case global::Stemp:
                return Stemp.Count < MAX_STEMP ? true : false;
        }
        return false;
    }
    public void ItemObtain<T>(Item item, T t)
    {

        List<Item> list = t switch
        {
            global::Weapon => Weapon,
            global::Equipment => Equipment,
            global::Stemp => Stemp,
            _ => throw new System.NotImplementedException()
        };

        Item existingItem = FindExistingItem(list, item);
        if (existingItem != null)
        {
            existingItem.ItemLevelUp(existingItem._weaponID);
            return;
        }

        if (list == Stemp && Stemp.Count < MAX_STEMP) 
        {
            AddItemList(Stemp, item);
        }
        else if (list == Equipment && Equipment.Count < MAX_EQUIPMENT)
        {
            AddItemList(Equipment, item);
        }

        else if (list == Weapon && Weapon.Count < MAX_WEAPON)
        {
            AddItemList(Weapon, item);
        }
    }

    private Item FindExistingItem(List<Item> list, Item newItem)
    {
        foreach(Item item in list) 
        { 
            if (item._name == newItem._name) { return item; }
        }

        return null;
    }

    private void AddItemList(List<Item> list, Item item)
    {
        list.Add(item);
    }
}
