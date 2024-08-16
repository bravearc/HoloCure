using System.Drawing;
using UnityEngine;

public class PhoenixSword : WeaponMelee
{
    Vector2 size;
    Vector2 colSize = new Vector2(117, 127);
    Vector2 offset = new Vector2(21, 0);
    protected override void WeaponSetComponent(Attack attack)
    {
        size = new Vector2(_weaponData.Size, _weaponData.Size);
        attack.SetAttackComponent(true, true, size, colSize, offset);
    }
}
