using UniRx.Triggers;
using UniRx;
using UnityEngine;
using System.Collections;
using System;

public class Exp : MonoBehaviour
{
    Character _character;
    SpriteRenderer _spriteRenderer;
    ExpData _data;
    float _shakeY = 0.2f;
    float _shakeDuration = 0.8f;
    Vector2 _initialPosition;

    IEnumerator _moveCo;
    IEnumerator _dyingMoveCo;

    void Awake()
    {
        _character = Manager.Game.Character;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(ExpData data, Transform newPos)
    {
        _moveCo = MoveCo();
        _dyingMoveCo = DyingMoveCo();
        transform.position = newPos.position;
        _data = data;
        _spriteRenderer.sprite = Manager.Asset.LoadSprite($"spr_Exp_{data.ID}");

        this.OnTriggerEnter2DAsObservable().Subscribe(ExpTrigger);

        _initialPosition = (Vector2)newPos.position + Vector2.up;
        StartCoroutine(_moveCo);
    }

    void ExpTrigger(Collider2D collider)
    {
        switch (collider.tag)
        {
            case Define.Tag.EXP :
                Exp newExp = collider.GetComponent<Exp>();
                Manager.Spawn.SpawnExp(AddExpData(_data, newExp._data), transform);
                Die();
                break;
            case Define.Tag.IDOL:
                _character.GetExp(_data.Exp); Die();
                break;
            case Define.Tag.HASTE: 
                StartCoroutine(_dyingMoveCo); 
                break;
        }

    }

    ExpData AddExpData(ExpData exp, ExpData exp2) 
    {
        return Manager.Data.Exp[exp.ID + exp2.ID];
    }

    IEnumerator MoveCo()
    {
        while(true)
        {
            float newY = Mathf.Abs(Mathf.Sin(Time.time * Mathf.PI / _shakeDuration)) * _shakeY;

            var newPosition = _initialPosition;
            newPosition.y += newY;

            transform.position = newPosition;
            yield return null;
        }
    }

    IEnumerator DyingMoveCo()
    {
        StopCoroutine(_moveCo);

        Vector2 startPos = transform.position;
        Vector2 endPos = _character.transform.position;
        float elapsedTime = 0f;

        while (isActiveAndEnabled)
        {
            Vector2 newPos = Vector2.Lerp(startPos, endPos, elapsedTime);
            transform.position = newPos;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

    }
    void Die()
    {
        StopCoroutine(_dyingMoveCo);
        Manager.Spawn.Exp.Release(this);
    }
}
