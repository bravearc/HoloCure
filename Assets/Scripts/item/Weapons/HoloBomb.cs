using UnityEngine;

public class HoloBomb : Weapon
{
    Vector2 size;
    Vector2 colSize = new Vector2(23, 21);

    protected override void AttackAction(Attack attack)
    {
        WeaponSetComponent(attack);

        attack.transform.position = _cursor.transform.position;
        attack.transform.rotation = Quaternion.Euler(0,0,0);
        attack.SetFlipY(false);
    }

    void ImpectGo(Attack attack)
    {
        Impact impect = Manager.Spawn.Impect.Get();
        impect.transform.position = attack.transform.position;
        impect.Init("Ani_HoloBomb_0", new Vector2(5, 5));
    }

    protected override void WeaponSetComponent(Attack attack)
    {
        size = new Vector2(_weaponData.Size, _weaponData.Size);
        attack.SetAttackComponent(false, false, size, colSize, Vector2.zero, false, 0, true, ImpectGo);
    }
    protected override void Disable(Attack attack)
    {

    }

}
