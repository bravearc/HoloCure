
using UnityEngine;

public interface IItem
{
    public void EquipItem();
}

public class Weapon : MonoBehaviour, IItem
{
    public string _name;
    public float _attackRange;
    public float _attackCycle;
    public float _attack;
    public float _size;
    public void EquipItem() { }
}

public class Equipment : MonoBehaviour, IItem
{
    public void EquipItem() { }
}

public class Stemp : MonoBehaviour, IItem
{
    public void EquipItem() { }
}
