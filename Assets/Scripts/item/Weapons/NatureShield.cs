using UnityEngine;

public class NatureShield : Weapon
{
    Vector2 size = new Vector2(3, 3);
    Vector2 _initPos;
    Attack[] newAttack = new Attack[2];
    Vector3[] positions = new Vector3[3] 
    { 
        new Vector3(-2,2),
        new Vector3(2,2),
        new Vector3(0, -2)
    };
    protected override void WeaponSetComponenet(Attack attack)
    {
        Vector3 tr = attack.transform.position;
        attack.SetAttackComponent(false, size, tr + positions[0], Vector2.zero);
        WeaponRotation(attack, 0);
        for (int idx = 1; idx < positions.Length; idx++) 
        {
            newAttack[idx] = Manager.Spawn.GetAttack();
            newAttack[idx].SetAttackComponent(false, size, tr + positions[idx], Vector2.zero);
            newAttack[idx].SetSprite(Manager.Asset.LoadSprite(WeaponData.Animation));
            WeaponRotation(newAttack[idx], idx);
        }
    }

    protected override void AttackAction(Attack attack)
    {
        WeaponSetComponenet(attack);
    }
    void WeaponRotation(Attack attack, int idx)
    {
        WeaponSetComponenet(attack);

        Transform weapon = attack.GetComponent<Transform>();
        _initPos = weapon.position;

        while (true)
        {
            weapon.RotateAround(_initPos, Vector3.up, WeaponData.Speed * Time.deltaTime);

            Vector2 direction = ((Vector2)weapon.position - _initPos).normalized;
            weapon.position = _initPos + direction * WeaponData.AttackRange;
        }
    }
    private void OnDisable()
    {
        foreach(var attack in newAttack)
        {
            Manager.Spawn.Attack.Release(attack);
        }
    }
}
