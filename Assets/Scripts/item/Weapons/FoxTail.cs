using UnityEngine;

public class FoxTail : WeaponMelee
{
    Vector2 size;
    Vector2 offset = new Vector2(57, 0);
    Vector2 colSize = new Vector2(100f, 73f);
    protected override void WeaponSetComponent(Attack attack)
    {
        size = new Vector2(_weaponData.Size, _weaponData.Size);
        attack.SetAttackComponent(true, true, size, colSize, offset);

    }
}
