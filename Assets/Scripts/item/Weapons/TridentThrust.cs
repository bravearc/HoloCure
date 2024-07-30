using UnityEngine;
public class TridentThrust : WeaponMelee
{
    Vector2 colSize = new Vector2(1, 0.2f);
    Vector2 offset = new Vector2(0.13f, 0);
    Vector2 size = new Vector2(3, 3);
    protected override void WeaponSetComponenet(Attack attack)
    {
        attack.SetAttackComponent(true, size, colSize, offset);
     }
}
           