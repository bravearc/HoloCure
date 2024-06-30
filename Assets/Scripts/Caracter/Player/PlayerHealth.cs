using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float _hp;

    public void Take(float damage)
    {
        _hp -= damage;
        if( _hp <= 0 )
        {
            //엔딩 크래딧 실행
        }
    }
}
