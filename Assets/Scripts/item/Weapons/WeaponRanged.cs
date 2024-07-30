
using System.Collections;
using UnityEngine;

public class WeaponRanged : Weapon
{
    protected override void AttackAction(Attack attack)
    {
        WeaponSetComponenet(attack);

        float timer = 0;
        
        //1�� 
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

    //��ӹ��� Ŭ�������� ������
    protected override void WeaponSetComponenet(Attack attack){ }
}
