public interface IItem
{
    public ItemID ID { get; set; }
    string Name { get; set; }
    string Explanation { get; set; }
    public ItemType Type { get; set; }
    public int Level { get; set; }
}
public enum ItemID
{
    None,
    Weapon = 1,
    Equipment = 1001,
    Drop = 2001
}
public enum ItemType
{
    None,
    Weapon,
    Equipment,
    Drop,
    Melee = 101,
    Ranged = 102,
    Multishot = 103
}


public class EquipmentData : IItem
{
    public ItemID ID { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public string Explanation { get; set; }
    public ItemType Type { get; set; }
}
public class WeaponData : IItem
{
    public ItemID ID { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public float Attack { get; set; }
    public float Quantity { get; set; }
    public float Speed { get; set; }
    public float AttackRange { get; set; }
    public float Size { get; set; }
    public int Knockback { get; set; }
    public int MaxLevel { get; set; }
    public string Explanation { get; set; }
    public ItemType Type { get; set; }
}
public class ItemData
{
    public ItemID ID { get; set; }
    public string Name { get; set; }
    public ItemType Type { get; set; }
    public string Explanation { get; set; }
    public string IconIamge { get; set; }
}
