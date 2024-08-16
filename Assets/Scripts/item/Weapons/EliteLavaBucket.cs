using UnityEngine;

public class EliteLavaBucket : WeaponRandomRanged
{
    Vector2 size = new Vector2(3, 3);
    protected override void WeaponSetComponent(Attack attack)
    {
        attack.SetAttackComponent(true, true, size, Vector2.one, Vector2.zero);

    }
}
