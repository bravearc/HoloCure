using UnityEngine;

public class PhoenixSword : WeaponMelee
{
    Vector2 offset = new Vector2(0, 0);
    Vector2 colSize = new Vector2(1, 1);
    Vector2 size = new Vector2(3, 3);
    protected override void WeaponSetComponenet(Attack attack)
    {
        attack.SetAttackComponent(true, size, colSize, offset);
    }
}
