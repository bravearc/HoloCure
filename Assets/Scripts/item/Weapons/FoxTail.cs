using UnityEngine;

public class FoxTail : WeaponMelee
{
    Vector2 size = new Vector2(3, 3);
    Vector2 offset = new Vector2(0.48f, 0.06f);
    Vector2 colSize = new Vector2(0.88f, 0.79f);
    protected override void WeaponSetComponenet(Attack attack)
    {
        attack.SetAttackComponent(true, size, colSize, offset);

    }
}
