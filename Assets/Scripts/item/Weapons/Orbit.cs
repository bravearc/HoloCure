using UnityEngine;

public class Orbit : WeaponRotation
{
    Vector2 size = new Vector2(2, 2);

    protected override void WeaponSetComponenet(Attack attack)
    {
        attack.SetAttackComponent(false, size, Vector2.one, Vector2.zero);
    }
}
