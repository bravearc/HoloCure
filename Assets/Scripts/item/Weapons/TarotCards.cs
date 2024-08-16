using System;
using System.Collections;
using System.Drawing;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class TarotCards : Weapon
{
    Vector2 size;
    Vector2 colSize = new Vector2(32, 7);
    IEnumerator _boomerang;
    IDisposable _disposable;
    protected override void AttackAction(Attack attack)
    {
        base.AttackAction(attack);
        WeaponSetComponent(attack);
        attack.GetSprite().sortingOrder = 2;

        _boomerang = Boomerang(attack);
        _disposable?.Dispose();
        _disposable = attack.OnDisableAsObservable().Subscribe(_ =>
        {
            StopCoroutine(_boomerang);
            _disposable?.Dispose();
            _disposable = null;
            _boomerang = null;
        });
        StartCoroutine(_boomerang);
    }
    protected override void WeaponSetComponent(Attack attack)
    {
        float verticalSize = _weaponData.Size * 1.5f;
        size = new Vector2(_weaponData.Size, verticalSize);
        attack.SetAttackComponent(false, false, size, colSize, Vector2.zero);
    }

    IEnumerator Boomerang(Attack attack)
    {
        Vector2 characterPos = (Vector2)_character.transform.position + new Vector2(0,Utils.GetRandomAngle());
        Vector2 startPos = attack.transform.localPosition;
        Vector2 cursorDirection = ((Vector2)_cursor.position - characterPos).normalized;
        Vector2 endPos = startPos + cursorDirection * 4;
        float duration = 3f;
        float timer = 0f;

        while (timer < duration)
        {
            attack.transform.localPosition = Vector2.Lerp(startPos, endPos, timer / duration);
            timer += Time.fixedDeltaTime;
            yield return null;
        }
        timer = 0f;

        while (timer < duration)
        {
            attack.transform.localPosition = Vector2.Lerp(endPos, startPos, timer / duration);
            timer += Time.fixedDeltaTime;
            yield return null;
        }
    }

}
