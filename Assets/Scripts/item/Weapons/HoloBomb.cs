using UniRx.Triggers;
using UnityEngine;
using UniRx;
using System;

public class HoloBomb : Weapon
{
    Vector2 size;
    Vector2 colSize = new Vector2(23, 21);

    Vector2 _initPos;
    IDisposable _trigger;
    IDisposable _disposable;
    Attack _attack;
    protected override void AttackAction(Attack attack)
    {
        WeaponSetComponent(attack);

        this._attack = attack;
        attack.transform.position = _cursor.transform.position;
        attack.transform.rotation = Quaternion.Euler(0,0,0);
        attack.SetFlipY(false);

        _initPos = attack.transform.position;

        _trigger?.Dispose();
        _disposable?.Dispose();

        _trigger = attack.OnTriggerEnter2DAsObservable().Subscribe(Trigger);
        _disposable = attack.OnDisableAsObservable().Subscribe(_ => 
        {
            ImpectGo();
            _trigger?.Dispose();
            _disposable?.Dispose();
            _trigger = null;
            _disposable = null;
        });
    }

    void Trigger(Collider2D col)
    {
        if (col.CompareTag(Define.Tag.ENEMY))
        {
            ImpectGo();
        }
    }

    void ImpectGo()
    {
        Impact impect = Manager.Spawn.Impect.Get();
        impect.transform.position = _initPos;
        impect.Init("Ani_HoloBomb_0", new Vector2(5, 5));
    }

    protected override void WeaponSetComponent(Attack attack)
    {
        size = new Vector2(_weaponData.Size, _weaponData.Size);
        attack.SetAttackComponent(false, false, size, colSize, Vector2.zero, false, 0, true);
    }

}
