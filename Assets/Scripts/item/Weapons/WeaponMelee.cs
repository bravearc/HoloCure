using System;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class WeaponMelee : Weapon
{
    IEnumerator _positionCo;
    IDisposable _disposable;
    protected override void AttackAction(Attack attack)
    {
        base.AttackAction(attack);
        WeaponSetComponent(attack);
        attack.GetSprite().sortingOrder = 2;

        _positionCo = PositionCo(attack);

        _disposable = attack.OnDisableAsObservable().Subscribe(_ =>
        {
            StopCoroutine(_positionCo);
            _disposable?.Dispose();

            _positionCo = null;
            _disposable = null;
        });
        StartCoroutine(_positionCo);
    }

    IEnumerator PositionCo(Attack attack)
    {
        Vector3 previousCharacterPosition = _character.transform.position;

        while (attack.isActiveAndEnabled)
        {
            Vector3 characterMovement = _character.transform.position - previousCharacterPosition;
            attack.transform.position += characterMovement;

            previousCharacterPosition = _character.transform.position;

            yield return null;
        }

    }

}
