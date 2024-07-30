using UnityEngine;

public class SummonTentacle : WeaponMelee
{
    Vector2 colsize = new Vector2(1, 0.76f);
    Vector2 offset = new Vector2(0.55f, 0);
    Vector2 size = new Vector2(3, 3);
    protected override void WeaponSetComponenet(Attack attack)
    {
        attack.SetAttackComponent(true, size, colsize, offset);
    }
}
