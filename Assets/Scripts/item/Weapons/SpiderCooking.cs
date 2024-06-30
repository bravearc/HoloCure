using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderCooking : Item
{
    CircleCollider2D _circle2D;
    float _timer;
    bool isDamage;

    void Start()
    {
        _circle2D = GetComponent<CircleCollider2D>();
    }

    public override void ItemLevelUp(WeaponData data)
    {
        base.ItemLevelUp(data);
    }

    void FixedUpdate()
    {
        _timer += Time.fixedDeltaTime;
        if(_timer > _attackCycle) 
        {
            isDamage = !isDamage;
            _timer = 0;
        }
    }
    void Update()
    {
        if(isDamage)
        {
            _circle2D.radius = _attackRange;
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
            enemy.TakeAttack(_attack);
        }
    }
}
