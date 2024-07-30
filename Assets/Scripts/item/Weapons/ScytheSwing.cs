using UnityEngine;

public class ScytheSwing : WeaponMelee
{
    Vector2 size = new Vector2(3, 3);
    protected override void WeaponSetComponenet(Attack attack)
    {
        attack.SetAttackComponent(true, size, Vector2.one, Vector2.zero);
    }
}
