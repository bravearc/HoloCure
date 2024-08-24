using UnityEngine;
using System.Collections;

public class WeaponRandomShooting : Weapon
{
    float _speedAdjustment = 4f;
    protected override void AttackAction(Attack attack)
    {
        base.AttackAction(attack);
        WeaponSetComponent(attack);
        attack.GetSprite().sortingOrder = 2;  
        attack.transform.position = _character.transform.position;

        Rigidbody2D _rd;
        _rd = attack.GetRigid();

        float angle = UnityEngine.Random.Range(0f, 360f);
        Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        _rd.AddForce(direction * _weaponData.Speed * _speedAdjustment, ForceMode2D.Impulse);
    }

}
