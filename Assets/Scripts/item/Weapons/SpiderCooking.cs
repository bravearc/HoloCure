using UniRx;
using UnityEngine;

public class SpiderCooking : Weapon
{
    CircleCollider2D _circle2D;
    float _timer;
    bool isDamage;

    public override void Init(ItemID id)
    {
        base.Init(id);
        _circle2D = GetComponent<CircleCollider2D>();
    }

    void FixedUpdate()
    {
        _timer += Time.fixedDeltaTime;
        if(_timer > WeaponData.AttackCycle) 
        {
            isDamage = !isDamage;
            _timer = 0;
        }
    }
    void Update()
    {
        if(isDamage)
        {
            _circle2D.radius = WeaponData.AttackRange;
        }
        else
        {
            _circle2D.radius = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<IEnemy>() is IEnemy enemy)
        {
            //isKnockback를 매개변수로 전달해서 넉백 기능 추가.
            enemy.TakeAttack(WeaponData.Attack);
        }
    }

    public virtual void ResetSettings() 
    {
        transform.localScale = new Vector2(WeaponData.Size, WeaponData.Size);
    }
}
