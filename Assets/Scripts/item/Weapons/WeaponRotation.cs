using UnityEngine;
using System.Collections;

public class WeaponRotation : Weapon
{
    Vector2 _initPos;

    protected override void AttackAction(Attack attack)
    {
        WeaponSetComponenet(attack);

        Transform weapon = attack.GetComponent<Transform>();
        _initPos = weapon.position;

        while (true) 
        {
            weapon.RotateAround(_initPos, Vector3.up, WeaponData.Speed * Time.deltaTime);

            Vector2 direction = ((Vector2)weapon.position - _initPos).normalized;
            weapon.position = _initPos + direction * WeaponData.AttackRange;
        }
    }

}
