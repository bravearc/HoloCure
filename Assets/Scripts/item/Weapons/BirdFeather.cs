using UnityEngine;

public class BirdFeather: WeaponMultishot
{
    Vector2 size;
    Vector2 colsize = new Vector2(28, 13f);

    protected override void WeaponSetComponent(Attack attack)
    {
        _angle = Utils.GetRandomAngle();
        size = new Vector2(_weaponData.Size, _weaponData.Size);
        attack.SetAttackComponent(false, false, size, colsize, Vector2.zero);
    }
}
