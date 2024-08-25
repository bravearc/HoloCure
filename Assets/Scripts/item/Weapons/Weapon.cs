using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public ReactiveProperty<int> Level { get; private set; } = new();
    protected WeaponData _weaponData;
    protected float _attackTimer;
    protected Character _character;
    protected Transform _cursor;
    public ItemID ID;
    private IEnumerator _attackCo;
    private Dictionary<Attack, IDisposable> _attackSubscriptions = new();

    float _attackDelay = 0.15f;


    void Awake()
    {
        _character = Manager.Game.Character;
        _cursor = Manager.Game.Character.Cursor;
    }
    public virtual void Init(ItemID id)
    {
        _weaponData = Manager.Data.Weapon[id][1];
        ID = id;
        _attackCo = AttackCo();
        StartCoroutine(_attackCo);
    }

    public virtual void LevelUp()
    {
        ++Level.Value;
        _weaponData = Manager.Data.Weapon[ID][Level.Value];
    }


    IEnumerator AttackCo()
    {
        while (true)
        {
            int _attackCount = 0;
            while (_weaponData.Quantity > _attackCount)
            {
                Attack attack = Manager.Spawn.GetAttack();
                attack.StopCurrentAction();
                attack.Init(_weaponData, AttackAction);
                WeaopnSound();

                IDisposable subscription = attack.OnDisableAsObservable().Subscribe(_ =>
                {
                    if (_attackSubscriptions.TryGetValue(attack, out IDisposable innerSubscription))
                    {
                        _attackSubscriptions.Remove(attack);
                        innerSubscription.Dispose();

                        Disable(attack);
                    }

                });

                _attackSubscriptions[attack] = subscription;

                ++_attackCount;
                yield return new WaitForSeconds(_attackDelay);
            }
            _attackCount = 0;
            yield return new WaitForSeconds(_weaponData.AttackCycle);
        }
    }

    protected virtual void AttackAction(Attack attack) 
    {
        attack.transform.position = _character.transform.position;
    }

    protected virtual void WeaponSetComponent(Attack attack) { }
    protected void WeaopnSound()
    {
        string sound = Manager.Data.Item[ID].Sound;
        if (sound == "null") 
        {
            return;
        }
        Manager.Sound.Play(Define.SoundType.Effect, sound);
    }

    protected virtual void Disable(Attack attack) { }
}
