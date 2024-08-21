public enum BossID
{
    Fubuzilla = 1001,
    SmollAme
}
public class BossData
{
    public BossID ID { get; set; } 
    public string Name { get; set; }
    public float Hp { get; set; }
    public float Attack { get; set; }
    public float Speed { get; set; }
    public string Sprite { get; set; }
    public string Skill { get; set; }
}
