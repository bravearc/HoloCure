using UnityEditor;
using UnityEngine;

public class AmeliaWatson : MonoBehaviour, ICharacter, ISpecialSkill
{
    public float Hp { get; set; }
    public float Attack { get; set; }
    public float Speed { get; set; }
    public float Critical { get; set; }

    private float _timer;
    private float _time = 2f;
    void Start()
    {
        Hp = 60f;
        Attack = 10f;
        Speed = 2f;
        Critical = 3f;
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer > _time)
        {
            BulletSpwan();
            _timer = 0;
        }
    }
    public float[] StatCheck()
    {
        float[] stat = new float[4] {Hp, Attack, Speed, Critical};
        return stat;
    }
    public void SpecialSkill()
    {

    }

    private void BulletSpwan()
    {
        GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Bullet.prefab");
        Instantiate(go, transform.position, Quaternion.identity);

    }
}
