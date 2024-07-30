using UnityEngine;

public class PistolShot : WeaponMultishot
{
    Vector2 colSize = new Vector2(0.4f, 0.4f);
    Vector2 size = new Vector2(3, 3);
    protected override void WeaponSetComponenet(Attack attack)
    {
        attack.SetAttackComponent(false, size, colSize, Vector2.zero);
    }
}
