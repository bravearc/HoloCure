public enum EnemyID
{
    None,
    Nomal = 1000,
    Boss = 2000
}
public class EnemyData 
{
    public EnemyID ID { get; set; }
    public float Hp { get; set; }
    public float Speed { get; set; }
    public int Attack { get; set; }
    public string Sprite { get; set; }
}
