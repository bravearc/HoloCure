using UnityEngine;

public class BirdFeather: WeaponMultishot
{
    Vector2 size = new Vector2(0.8f, 0.8f);
    Vector2 colsize = new Vector2(1, 0.5f);

    protected override void WeaponSetComponenet(Attack attack)
    {
        _angle = Utils.GetRandomAngle();
        attack.SetAttackComponent(false, size, colsize, Vector2.zero);
    }
}
