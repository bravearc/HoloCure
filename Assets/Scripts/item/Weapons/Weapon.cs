using System.Collections;
using UniRx;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public ReactiveProperty<int> Level { get; private set; } = new();
    private WeaponData _weaponData;
    private float _attackTimer;
    public ItemID ID;
    IEnumerator _attackCycle;

    public WeaponData WeaponData { get; private set; }
    public virtual void Init(ItemID id)
    {
        WeaponData = Manager.Data.Weapon[id][0];
        Level.Subscribe(SetStats).AddTo(this);
        _attackCycle = AttackCycle();
        StartCoroutine(_attackCycle);
    }

    public virtual void LevelUp()
    {
        ++Level.Value;
    }

    IEnumerator AttackCycle()
    {
        while(_weaponData.AttackCycle > _attackTimer) 
        {
            _attackTimer += Time.deltaTime;
            yield return null;
        }
        
        AttackStrike();
        StartCoroutine(_attackCycle);
    }
    protected virtual void AttackStrike(){ }
    protected virtual void SetStats(int level)
    {
        _weaponData = Manager.Data.Weapon[_weaponData.ID][level];
    }


}
