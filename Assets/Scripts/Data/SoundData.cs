public enum SoundID
{
    None
}
public class SoundData
{
    public SoundID ID { get; set; }
    public string Name { get; set; }
    public string AudioClip { get; set; }
    public int Volume { get; set; }
}
