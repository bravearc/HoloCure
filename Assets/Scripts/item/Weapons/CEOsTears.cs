using UnityEngine;

public class CEOsTears : WeaponRandomShooting
{

    Vector2 size = new Vector2(6f, 6f);
    Vector2 colSize = new Vector2(0.04f, 0.04f);
    protected override void WeaponSetComponent(Attack attack)
    {
        attack.SetAttackComponent(false, false, size, colSize, Vector2.zero);
    }
}
