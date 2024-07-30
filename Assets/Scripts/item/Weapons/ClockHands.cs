using UnityEngine;

public class ClockHands : WeaponMelee
{
    Vector2 size = new Vector2(3, 1);
    protected override void WeaponSetComponenet(Attack attack)
    {
        attack.SetAttackComponent(true, size, Vector2.zero, Vector2.one);
    }

}
