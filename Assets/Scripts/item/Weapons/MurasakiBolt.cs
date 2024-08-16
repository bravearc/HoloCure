using UnityEngine;

public class MurasakiBolt : WeaponMultishot
{
    Vector2 size;
    Vector2 colSize = new Vector2(57, 20);
    protected override void WeaponSetComponent(Attack attack)
    {
        size = new Vector2(_weaponData.Size, _weaponData.Size);
        attack.SetAttackComponent(true, true, size, colSize, Vector2.zero);
    }
}
