using System.Drawing;
using UnityEngine;

public class SummonTentacle : WeaponMelee
{
    Vector2 size;
    Vector2 colsize = new Vector2(124, 37f);
    Vector2 offset = new Vector2(61f, 0);
    protected override void WeaponSetComponent(Attack attack)
    {
        size = new Vector2(_weaponData.Size, _weaponData.Size);
        attack.SetAttackComponent(true, true, size, colsize, offset);
    }
}
