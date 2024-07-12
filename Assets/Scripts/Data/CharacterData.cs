public enum CharacterID
{
    None
}
public enum CharacterType
{

}
public class CharacterData
{
    public CharacterID ID { get; set; }
    public string Name { get; set; }
    public float MaxHp { get; set; }
    public float Hp { get; set; }
    public float Attack { get; set; }
    public float Speed { get; set; }
    public float Criticial { get; set; }
    public float Pickup { get; set; }
    public float Haste { get; set; }
    public int NormalWeapon { get; set; }
    public string Sprite_Icon { get; set; }
    public string Sprite_Special { get; set; }
    public CharacterType Type { get; set; }
    public int Unlock { get; set; }
}
