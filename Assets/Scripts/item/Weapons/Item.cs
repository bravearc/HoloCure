using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    WeaponData _weaponData;
    protected int _level;
    protected float _attack;
    protected float _quantity;
    protected float _speed;
    protected float _attackRange;
    protected float _attackCycle;
    protected float _size;
    protected int _MAX_Level;
    protected bool isKnockback;
    public virtual void ItemLevelUp(WeaponData data)
    {
        _level = data.Level;
        _attack = data.Attack;
        _quantity = data.Quantity;
        _speed = data.Speed;
        _attackRange = data.AttackRange;
        _attackCycle = data.AttackCycle;
        _size = data.Size;
        _MAX_Level = data.MAX_Level;
        if (data.Knockback == 0)
        {
            isKnockback = true;
        }
    }

    public virtual void EquipItem()
    {
        //Inventory class¿¡ Àü´Þ.
    }
}
