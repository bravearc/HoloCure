using System;
using UniRx.Triggers;
using UnityEngine;
using UniRx;

public class WeaponMultishot : Weapon
{
    protected float _speedAdjustment = 4f;
    protected float _angle = 0f;
    Rigidbody2D _rb;
    IDisposable _disposable;
    protected override void AttackAction(Attack attack)
    {
        base.AttackAction(attack);
        WeaponSetComponent(attack);
        
        attack.transform.position = _character.transform.position;
        attack.GetSprite().sortingOrder = 2;

        _rb = attack.GetRigid();
        Vector2 direction = (_cursor.position - _character.transform.position).normalized;
        direction = Quaternion.Euler(0, 0, _angle) * direction;
        _rb.AddForce(direction * _weaponData.Speed * _speedAdjustment, ForceMode2D.Impulse);

        _disposable?.Dispose();
        _disposable = attack.OnDisableAsObservable().Subscribe(_ =>
        {
            _rb.velocity = Vector2.zero;
            _rb.angularVelocity = 0f;
            _disposable?.Dispose();
            _disposable = null;
        });
    }

}
