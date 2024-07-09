using UnityEngine;
using Type = System.Type;

public class PlayerHealth : MonoBehaviour
{
    public float Hp;
    public float MaxHp;
    public float Attack;
    public float Speed;
    public float Criticial;
    public float Pickup;
    public float Haste;

    public void Take(float damage)
    {
        Hp -= damage;
        if( Hp <= 0 )
        {
            //엔딩 크래딧 실행
        }
    }

    public void StatInit(CharacterData data)
    {
        Hp = data.Hp;
        MaxHp = data.MaxHp;
        Attack = data.Attack;
        Speed = data.Speed;
        Criticial = data.Criticial;
        Pickup =data.Pickup;
        Haste = data.Haste;
    }

}

