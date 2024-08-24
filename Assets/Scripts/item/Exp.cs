using UniRx.Triggers;
using UniRx;
using UnityEngine;
using System.Collections;
using System;
using Random = UnityEngine.Random;

public enum ExpType
{
    Exp,
    Hamburger
}
public class Exp : MonoBehaviour
{
    public ExpData Data;
    ExpType _expType;

    Character _character;
    SpriteRenderer _spriteRenderer;
    Animator _animator;
    CircleCollider2D _circleCollider;
    Vector2 _initialPosition;

    IEnumerator _moveCo;
    IEnumerator _dyingMoveCo;
    IDisposable _trigger;

    float _shakeY = 0.2f;
    float _shakeDuration = 0.8f;
    int _rainbowExp = 5;
    int _hamburgerHp = 10;

    bool _isHamburger;

    void Awake()
    {
        _character = Manager.Game.Character;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _circleCollider.offset = new Vector2(0, 3.67f);
        _circleCollider.radius = 7.17f;
        gameObject.tag = Define.Tag.EXP;
    }

    public void Init(Transform newPos, ExpType type)
    {
        transform.position = newPos.position + Vector3.down;
        _initialPosition = (Vector2)transform.position + Vector2.up;
        _expType = type;
        _isHamburger = false;
        GetExpID();
        SetExpComponent();

        _trigger?.Dispose();
        _trigger = this.OnTriggerEnter2DAsObservable().Subscribe(ExpTrigger);
        _dyingMoveCo = DyingMoveCo();
        SetCoroutine();
    }
    void SetCoroutine()
    {
        _moveCo = MoveCo();
        StartCoroutine(_moveCo);
    }

    void ExpTrigger(Collider2D collider)
    {
        switch (collider.tag)
        {
            case Define.Tag.EXP :
                Exp newExp = collider.GetComponent<Exp>();
                AddExpData(Data, newExp.Data);
                newExp.Die();
                break;
            case Define.Tag.IDOL:
                StopCoroutine(_dyingMoveCo);
                GetItem();
                Die();
                break;
            case Define.Tag.HASTE: 
                StartCoroutine(_dyingMoveCo); 
                break;
        }
    }
    void GetItem()
    {
        if(_expType == ExpType.Exp)
        {
            _character.GetExp(Data.Exp);
        }
        else
        {
            _character.GetHp(_hamburgerHp);
        }
    }
    void GetExpID()
    {
        int normal = Random.Range(1, 100);
        int id = normal switch
        {
            < 35 => 1,
            < 55 => 2,
            < 70 => 3,
            < 85 => 4,
            < 95 => 5,
            _ => 5
        };

        Data = Manager.Data.Exp[id - 1];
    }

    void SetExpComponent()
    {
        if (_expType == ExpType.Exp)
        {
            transform.transform.localScale = new Vector3(0.0330494f, 0.0330494f);
            _circleCollider.radius = 7.17f;
            _circleCollider.offset = new Vector2(0, 3.67f);

            if (Data.ID != _rainbowExp)
            {
                _animator.enabled = false;
                _spriteRenderer.sprite = Manager.Asset.LoadSprite($"spr_Exp_{Data.ID}");
            }
            else
            {
                _animator.enabled = true;
            }
        }
        else
        {
            _animator.enabled = false;
            _circleCollider.radius = 0.12f;
            _circleCollider.offset = Vector2.zero;
            transform.transform.localScale = new Vector2(2.5f, 2.5f);
            _isHamburger = true;
            _spriteRenderer.sprite = Manager.Asset.LoadSprite($"spr_Hamburger_0");
        }
    }

    void AddExpData(ExpData exp, ExpData exp2) 
    {
        if (_isHamburger == false)
        {
            int newID = exp.ID + exp2.ID;
            if (newID > 5) newID = 5;

            Data = Manager.Data.Exp[newID];
            SetExpComponent();
            SetCoroutine();
        }
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

        float elapsedTime = 0f;

        while (isActiveAndEnabled)
        {
            if (Time.timeScale == 0f) yield return null;

            Vector2 startPos = transform.position;
            Vector2 endPos = _character.transform.position;
            Vector2 newPos = Vector2.Lerp(startPos, endPos, elapsedTime);
            transform.position = newPos;
            elapsedTime += Time.deltaTime * 0.01f;
            yield return null;
        }

    }
    void Die()
    {
        StopCoroutine(_dyingMoveCo);
        Manager.Spawn.Exp.Release(this);
    }
}
