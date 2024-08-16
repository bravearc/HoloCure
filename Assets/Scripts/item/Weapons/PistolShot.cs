using System.Drawing;
using UnityEngine;

public class PistolShot : WeaponMultishot
{
    Vector2 size;
    Vector2 colSize = new Vector2(12f, 9f);
    protected override void WeaponSetComponent(Attack attack)
    {
        size = new Vector2(_weaponData.Size, _weaponData.Size);
        attack.SetAttackComponent(false, false, size, colSize, Vector2.zero);
    }
}
