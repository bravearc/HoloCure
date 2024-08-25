using UniRx;
using UniRx.Triggers;
using System.Collections;
using UnityEngine;
using System;

public class WeaponRanged : Weapon
{
    IEnumerator _rangerCo;
    IDisposable _updatePos;
    protected override void AttackAction(Attack attack)
    {
        base.AttackAction(attack);
        WeaponSetComponent(attack);
        attack.GetSprite().sortingOrder = 1;

        _updatePos = this.FixedUpdateAsObservable().Subscribe(_ => Update_Position(attack));
        
        _rangerCo = RangedCo(attack);
        StartCoroutine(_rangerCo);
    }
    void Update_Position(Attack attack)
    {
        attack.transform.position = _character.transform.position;
    }
    IEnumerator RangedCo(Attack attack)
    {
        float timer = 0;
        float attackTime = 3;

        while (true)
        {
            timer += Time.deltaTime;
            
            if(timer > attackTime) 
            { 
                attack.OffCollider();
                timer = 0;
            }
            else
            {
                attack.OnCollider();
            }
            yield return new WaitForSeconds(attack.HoldingTime);
        }
    }
    protected override void WeaponSetComponent(Attack attack){ }

    protected override void Disable(Attack attack)
    {
        StopCoroutine(_rangerCo);
        _updatePos?.Dispose();
    }
}
