using System.Collections;
using UniRx;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public ReactiveProperty<int> Level { get; private set; } = new();
    public WeaponData WeaponData;
    protected float _attackTimer;
    protected Character _character;
    protected Transform _cursor;
    public ItemID ID;
    public IEnumerator _attackCo;

    float _attackDelay = 0.13f;


    void Awake()
    {
        _character = Manager.Game.Character;
        _cursor = Manager.Game.Character.Cursor;
    }
    public virtual void Init(ItemID id)
    {
        WeaponData = Manager.Data.Weapon[id][1];
        ID = id;
        _attackCo = AttackCo();
        StartCoroutine(_attackCo);
    }

    public virtual void LevelUp()
    {
        ++Level.Value;
        WeaponData = Manager.Data.Weapon[ID][Level.Value];
    }

    IEnumerator AttackCo()
    {
        while (true)
        {
            int _attackCount = 0;
            while (WeaponData.Quantity > _attackCount)
            {
                Attack attack = Manager.Spawn.GetAttack();
                attack.Init(WeaponData, _cursor.position, AttackAction);
                ++_attackCount;
                yield return new WaitForSeconds(_attackDelay);
            }
            _attackCount = 0;
            yield return new WaitForSeconds(WeaponData.AttackCycle);
        }
    }

    protected virtual void AttackAction(Attack attack) {}

    protected virtual void WeaponSetComponenet(Attack attack) { }

}
