using UnityEngine;

public class Orayo : WeaponMultishot
{
    Vector2 size;
    Vector2 colSize = new Vector2(35, 20.7f);
    Vector2 offset = new Vector2(16, 2.5f);
    protected override void WeaponSetComponent(Attack attack)
    {
        size = new Vector2(_weaponData.Size, _weaponData.Size);
        _angle = Utils.GetRandomAngle();
        attack.SetAttackComponent(false, false, size, colSize, offset);
        bool flipY = Random.Range(0, 2) == 1;
        attack.SetFlipY(flipY);
    }
}
