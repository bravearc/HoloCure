using UnityEngine;

public class AmeliaWatson : ICaracter, ISpecialSkill
{
    public float Hp { get; set; }
    public float Attack { get; set; }
    public float Speed { get; set; }
    public float Critical { get; set; }

    void Start()
    {
        //GameObject go = GameObject.FindGameObjectWithTag("Player");
    }
    public float[] StatCheck()
    {
        float[] stat = new float[4] {Hp, Attack, Speed, Critical};
        return stat;
    }
    public void SpecialSkill()
    {

    }
}
