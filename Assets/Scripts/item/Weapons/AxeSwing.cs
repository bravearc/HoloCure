using System.Drawing;
using UnityEngine;

public class AxeSwing : WeaponMelee
{
    Vector2 size;
    Vector2 colsize = new Vector2(76f, 125);
    Vector2 offset = new Vector2(37f, 0);
    protected override void WeaponSetComponent(Attack attack)
    {
        size = new Vector2(_weaponData.Size, _weaponData.Size);
        attack.SetAttackComponent(true, true, size, colsize, offset);
    }
}
