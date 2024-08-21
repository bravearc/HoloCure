using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using UniRx;
using UniRx.Triggers;

public class PlayDice : WeaponMultishot
{
    Vector2 size;
    Vector2 colSize = new Vector2(23f, 23f);
    IEnumerator _moveStopCo;
    IDisposable _disposable;
    protected override void WeaponSetComponent(Attack attack)
    {
        int idx = Random.Range(0, 6);
        SpriteRenderer spriteRenderer = attack.GetSprite();
        spriteRenderer.sprite = Manager.Asset.LoadSprite($"spr_BaeDice_{idx}");

        size = new Vector2(_weaponData.Size, _weaponData.Size);
        attack.SetAttackComponent(false, false, size, colSize, Vector2.zero);

        _disposable = attack.OnDisableAsObservable().Subscribe(_ =>
        {
            StopCoroutine(_moveStopCo);
            _disposable?.Dispose();
            _disposable = null;
            _moveStopCo = null;

        });

        _moveStopCo = MoveStopCo(attack);
        StartCoroutine(_moveStopCo);
    }
    IEnumerator MoveStopCo(Attack attack)
    {
        Vector2 startPos = attack.transform.position;
        Vector2 currentPos = startPos;
        float targetDistance = 4f;
        while (true)
        {
            currentPos = attack.transform.position;
            float movePos = Vector2.Distance(startPos, currentPos);

            if (movePos > targetDistance)
                break;
            yield return null;
            
        }

        Rigidbody2D rd = attack.GetRigid();
        rd.velocity = Vector2.zero;
    }
}
