using UniRx.Triggers;
using UniRx;
using UnityEngine;
using System.Collections;

public class Exp : MonoBehaviour
{
    Character _character;
    SpriteRenderer _spriteRenderer;
    Animator _animator;
    CircleCollider2D _circleCollider;
    public ExpData Data;
    float _shakeY = 0.2f;
    float _shakeDuration = 0.8f;
    int _rainbowExp = 5;
    Vector2 _initialPosition;

    IEnumerator _moveCo;
    IEnumerator _dyingMoveCo;

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

    public void Init(Transform newPos)
    {
        transform.position = newPos.position;
        _initialPosition = (Vector2)newPos.position + Vector2.up;
        
        GetExpID();
        SetExpComponent();

        this.OnTriggerEnter2DAsObservable().Subscribe(ExpTrigger);

        _moveCo = MoveCo();
        _dyingMoveCo = DyingMoveCo();
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
                _character.GetExp(Data.Exp); 
                Die();
                break;
            case Define.Tag.HASTE: 
                StartCoroutine(_dyingMoveCo); 
                break;
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
            _ => 6
        };

        Data = Manager.Data.Exp[id - 1];
    }

    void SetExpComponent()
    {
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

    void AddExpData(ExpData exp, ExpData exp2) 
    {
        Data = Manager.Data.Exp[exp.ID + exp2.ID];
        SetExpComponent();
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
        Manager.Spawn.Exp.Release(this);
    }
}
