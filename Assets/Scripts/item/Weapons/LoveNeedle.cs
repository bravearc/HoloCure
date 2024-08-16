using UnityEngine;

public class LoveNeedle : WeaponMultishot
{
    Vector2 size;
    Vector2 colSize = new Vector2(100, 30);
    protected override void WeaponSetComponent(Attack attack)
    {
        size = new Vector2(_weaponData.Size, _weaponData.Size);
        attack.SetAttackComponent(false, false, size, colSize, Vector2.zero);
    }
}
