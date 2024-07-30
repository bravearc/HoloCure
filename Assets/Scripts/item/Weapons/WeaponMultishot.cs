using UnityEngine;

public class WeaponMultishot : Weapon
{
    float speedAdjustment = 0.03f;
    protected float _angle = 0f;
    protected override void AttackAction(Attack attack)
    {
        WeaponSetComponenet(attack);

        Rigidbody2D rb = attack.GetRigid();
        float timer = 0f;
        while (attack.HoldingTime > timer)
        {
            Vector2 direction = (_cursor.position - _character.transform.position).normalized;
            direction = Quaternion.Euler(0, 0, _angle) * direction;

            rb.AddForce(direction * WeaponData.Speed * speedAdjustment, ForceMode2D.Impulse);
            timer += Time.fixedDeltaTime;
        }
    }

}
