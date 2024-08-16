using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;

public class WeaponRandomShooting : Weapon
{
    float _speedAdjustment = 4f;
    Rigidbody2D _rd;
    IDisposable _disposable;

    protected override void AttackAction(Attack attack)
    {
        base.AttackAction(attack);
        WeaponSetComponent(attack);
        attack.GetSprite().sortingOrder = 2;  
        attack.transform.position = _character.transform.position;

        _rd = attack.GetRigid();
        float angle = UnityEngine.Random.Range(0f, 360f);

        Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        _rd.AddForce(direction * _weaponData.Speed * _speedAdjustment, ForceMode2D.Impulse);

        _disposable = attack.OnDisableAsObservable().Subscribe(_ => Disable());
    }

    private void Disable()
    {
        _rd.velocity = Vector2.zero;
        _rd.angularVelocity = 0f;
        _disposable?.Dispose();
        _disposable = null;
    }

}
