using UnityEngine;

public class Onigiri : WeaponMultishot
{
    Vector2 size;
    Vector2 colSize = new Vector2(24, 21);
    protected override void WeaponSetComponent(Attack attack)
    {
        size = new Vector2(_weaponData.Size, _weaponData.Size);
        attack.SetAttackComponent(false, true, size, colSize, Vector2.zero, false, 1f);
    }
}
