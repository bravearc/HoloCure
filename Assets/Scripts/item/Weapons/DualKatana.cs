using UnityEngine;

public class DualKatana : WeaponMelee
{
    Vector2 size;
    Vector2 colSize = new Vector2(120, 120);
    Vector2 offset = new Vector2(65.5f, 0);
    protected override void WeaponSetComponent(Attack attack)
    {
        size = new Vector2(_weaponData.Size, _weaponData.Size);
        attack.SetAttackComponent(true, true, size, colSize, offset);
    }
}
