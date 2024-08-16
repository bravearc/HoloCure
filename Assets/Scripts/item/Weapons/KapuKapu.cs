using UnityEngine;

public class KapuKapu : WeaponMelee
{
    Vector2 size;
    Vector2 offset = new Vector2(28, 0);
    Vector2 colSize = new Vector2(82, 121);
    protected override void WeaponSetComponent(Attack attack)
    {
        size = new Vector2(_weaponData.Size, _weaponData.Size);
        attack.SetAttackComponent(true, true, size, colSize, offset);
    }
}
