using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using System;

public class WeaponRotation : Weapon
{
    protected float _radius = 1.2f;
    float angleSpeed = 1f;
    IEnumerator _rotationCo;
    IDisposable _disposable;
    protected override void AttackAction(Attack attack)
    {
        base.AttackAction(attack);
        WeaponSetComponent(attack);
        attack.GetSprite().sortingOrder = 2;


        _disposable = attack.OnDisableAsObservable().Subscribe(_ => Disable());
        _rotationCo = RotationCo(attack);
        StartCoroutine(_rotationCo);
    }

    IEnumerator RotationCo(Attack attack) 
    { 
        float initialAngle = 0f;
        
        float timer = 0f;
        while (true)
        {
            Vector2 pivotPoint = _character.transform.position;
            float currentAngle = initialAngle + angleSpeed * timer;

            float x = pivotPoint.x +  _radius * Mathf.Cos(currentAngle);
            float y = pivotPoint.y +  _radius * Mathf.Sin(currentAngle);
            attack.transform.position = new Vector2(x, y);

            timer += Time.fixedDeltaTime;
            yield return null;  
        }
    }
    void Disable()
    {
        StopCoroutine(_rotationCo);
        _disposable?.Dispose();
        _disposable = null;
        _rotationCo = null;
    }
}
