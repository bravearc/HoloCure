public enum CharacterID
{
    None
}
public enum CharacterType
{
    MYTH,
    COUNCIL,
    Gamers,
    JP0,
    JP1,
    JP2
}
public class CharacterData
{
    public CharacterID ID { get; set; }
    public string Name { get; set; }
    public int Hp { get; set; }
    public float Attack { get; set; }
    public float Speed { get; set; }
    public float Criticial { get; set; }
    public float Pickup { get; set; }
    public float Haste { get; set; }
    public int StartingWeapon { get; set; }
    public CharacterType Type { get; set; }
    public string Sprite { get; set; }
}
