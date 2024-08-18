using UnityEngine;

public class CEOsTears : WeaponRandomShooting
{

    Vector2 size;
    Vector2 colSize = new Vector2(0.04f, 0.04f);
    protected override void WeaponSetComponent(Attack attack)
    {
        size = new Vector2(_weaponData.Size, _weaponData.Size);
        attack.SetAttackComponent(false, false, size, colSize, Vector2.zero);
    }
}
