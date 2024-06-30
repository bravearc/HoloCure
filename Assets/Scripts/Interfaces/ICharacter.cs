using UnityEngine;

public interface ICharacter
{
    public float Hp { get; set; }
    public float Attack { get; set; }
    public float Speed { get; set; }
    public float Critical { get; set; }

    void StatSet() { }
}
