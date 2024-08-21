public enum EnemyID
{
    None,
    Nomal,
    Boss = 1000
}
public class EnemyData 
{
    public EnemyID ID { get; set; }
    public string Name { get; set; }
    public float Hp { get; set; }
    public float Speed { get; set; }
    public int Attack { get; set; }
    public string Sprite { get; set; }
    public string Skill { get; set; }
}
