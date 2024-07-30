
using System.Collections;
using UnityEngine;

public class WeaponRanged : Weapon
{
    protected override void AttackAction(Attack attack)
    {
        WeaponSetComponenet(attack);

        float timer = 0;
        
        //1초 
        float attackTime = 3;

        while (attack.HoldingTime > timer)
        {
            while ((int)timer / (int)attackTime == 0)
            {
                timer += Time.deltaTime;
                attack.OffCollider();
            }

            attack.OnCollider();
        }
    }

    //상속받은 클래스에서 재정의
    protected override void WeaponSetComponenet(Attack attack){ }
}
