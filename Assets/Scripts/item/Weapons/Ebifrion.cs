using System;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Ebifrion : WeaponMelee
{
    Vector2 size;
    Vector2 colSize = new Vector2(80, 40);
    Vector2 offset = new Vector2(41, 21);
    IDisposable _disposable;
    IEnumerator _rotationCo;
    protected override void WeaponSetComponent(Attack attack)
    {
        size = new Vector2(_weaponData.Size, _weaponData.Size);
        attack.SetAttackComponent(false, false, size, colSize, offset);
        attack.transform.rotation = Quaternion.Euler(0, 0, 66);
        
        _rotationCo = RotationCo(attack);
        StartCoroutine(RotationCo(attack));
        
        _disposable?.Dispose();
        _disposable = attack.OnDisableAsObservable().Subscribe(_ =>
        {
            StopCoroutine(_rotationCo);
            _rotationCo = null;
        });
    }

    IEnumerator RotationCo(Attack attack)
    {
        while (true)
        {
            Quaternion currentPos = attack.transform.rotation;
            currentPos.z -= Time.fixedDeltaTime;
            attack.transform.rotation = currentPos;
            yield return null;
        }
    }
}
