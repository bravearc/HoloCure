using System.Drawing;
using UnityEngine;

public class ScytheSwing : WeaponMelee
{
    Vector2 size;
    protected override void WeaponSetComponent(Attack attack)
    {
        size = new Vector2(_weaponData.Size, _weaponData.Size);
        attack.SetAttackComponent(true, true, size, Vector2.one, Vector2.zero);
    }
}
