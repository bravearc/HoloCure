using UnityEngine;

public class ClockHands : WeaponMelee
{
    Vector2 size;
    Vector2 colSize = new Vector2(94, 23f);
    Vector2 offset = new Vector2(47.8f, 0);
    protected override void WeaponSetComponent(Attack attack)
    {
        size = new Vector2(_weaponData.Size, _weaponData.Size);
        attack.SetAttackComponent(true, true, size, colSize, offset);
    }

}
