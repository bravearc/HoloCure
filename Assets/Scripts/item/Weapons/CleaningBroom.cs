using UnityEngine;

public class CleaningBroom : WeaponMultishot
{
    Vector2 size;
    Vector2 colSize = new Vector2(30, 55);
    protected override void WeaponSetComponent(Attack attack)
    {
        size = new Vector2(_weaponData.Size, _weaponData.Size); 
        attack.SetAttackComponent(false, true, size, colSize, Vector2.zero, false, 1f);
    }
}
