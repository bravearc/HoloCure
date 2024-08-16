using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Inventory : MonoBehaviour
{
    public List<Weapon> Weapons = new();
    public List<Equipment> Equipments = new();
    public ReactiveProperty<int> WeaponCount = new(-1);
    public ReactiveProperty<int> EquipmentCount = new(-1);

    private HashSet<ItemID> _itemIDList = new();

    private const int INVENTORY_MAX_COUNT = 6;
    public void Init()
    {
        GetItem((ItemID)Manager.Game.GetCharacterData().StartingWeapon);
    }
    public bool IsItemTypeFull(ItemID id)
    {
        if ((int)id < (int)Define.ItemNumber.StartingWeapon_End)
            return Weapons.Count >= INVENTORY_MAX_COUNT;
        else if ((int)id < (int)Define.ItemNumber.Equipment_End)
            return Equipments.Count >= INVENTORY_MAX_COUNT;
        else
            Debug.Assert(false);
        return true;
    }

    public bool IsInventoryFull()
    {
        return Weapons.Count >= INVENTORY_MAX_COUNT &&
            Equipments.Count >= INVENTORY_MAX_COUNT;
    }

    public void GetItem(ItemID id)
    {
        if (_itemIDList.Add(id))
        {
            if ((int)id < (int)Define.ItemNumber.StartingWeapon_End)
            {
                ItemData data = Manager.Data.Item[id];
                GameObject go = Manager.Asset.LoadObject(data.Name);
                Weapon newItem = Manager.Asset.Instantiate(go, transform).GetComponent<Weapon>();
                Weapons.Add(newItem);
                newItem.Init(id);
                WeaponCount.Value++;
            }
            else
            {
                ItemData data = Manager.Data.Item[id];
                Equipment newItem = Manager.Asset.Instantiate(data.Name, transform).GetComponent<Equipment>();
                Equipments.Add(newItem);
                newItem.Init(id);
                EquipmentCount.Value++;
            }
        }
        else
        {
            int idx = 0;
            if((int)id < (int)Define.ItemNumber.StartingWeapon_End)
            {
                foreach(Weapon weapon in Weapons)
                {
                    if (weapon.ID == id)
                    {
                        weapon.LevelUp();
                        break;
                    }
                    ++idx;
                }
            }
            else
            {
                foreach(Equipment equipment in Equipments)
                {
                    if(equipment.ID == id) 
                    { 
                        equipment.LevelUp();
                        break;
                    }
                }
            }
        }
    }

    public bool IsLevelUp(ItemID id)
    {
        return _itemIDList.Contains(id);
    }

    public void Claer()
    {
        _itemIDList.Clear();
        Weapons.Clear();
        Equipments.Clear();
        WeaponCount.Value = -1;
        EquipmentCount.Value = -1;
        foreach(Transform child in transform)
        {
            Manager.Asset.Destroy(child.gameObject);
        }
    }
}
