using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemID _id { get; set; }
    public string _name { get; set; }
    public int _level { get; set; }
    public float _attack { get; set; }
    public float _quantity { get; set; }
    public float _speed { get; set; }
    public float _attackRange { get; set; }
    public float _attackCycle { get; set; }
    public float _size { get; set; }
    public int _MAX_Level { get; set; }
    public bool isKnockback { get; set; }
    public virtual void ItemFunction() { }

}
