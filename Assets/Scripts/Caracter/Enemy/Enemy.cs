using UnityEngine;
using System.Collections;
using UniRx.Triggers;
using UniRx;
using System;
public enum EnemyType
{
    Normal,
    MiniBoss,
    Boss
}
public class Enemy : MonoBehaviour
{
    Rigidbody2D _body;
    Character _character;
    Animator _animator;
    EnemyData _data;
    SpriteRenderer _enemyRenderer;
    BoxCollider2D _boxCollider;
    EnemyType _enemyType;
    IEnumerator _moveEnemy;
    IEnumerator _backMoveEnemy;

    readonly Vector2 _normalSize = Vector2.one;

    bool _isDead = false;
    float _speed = 1f;
    float _hp;
    float _bossSize = 3;

    void Awake() 
    {
        _enemyRenderer = Utils.GetOrAddComponent<SpriteRenderer>(gameObject);
        _character = Manager.Game.Character;
        _body = Utils.GetOrAddComponent<Rigidbody2D>(gameObject);
        _animator = Utils.GetOrAddComponent<Animator>(gameObject);
        _boxCollider = Utils.GetOrAddComponent<BoxCollider2D>(gameObject);
        gameObject.tag = Define.Tag.ENEMY;

        _enemyRenderer.drawMode = SpriteDrawMode.Simple;

        _enemyRenderer.sortingOrder = 2;
        SpriteRenderer shadowRenderer = transform.Find("Shadow_0").GetComponent<SpriteRenderer>();
        shadowRenderer.sortingOrder = 1;
        shadowRenderer.drawMode = SpriteDrawMode.Simple;
        shadowRenderer.transform.localPosition = new Vector2(-0.7f, 0);
        shadowRenderer.transform.localScale = new Vector2(1, 1);


        this.OnTriggerEnter2DAsObservable().Subscribe(DamageStrike);
    }

    public void Init(EnemyID id, Vector2 newPosition, EnemyType type)
    {
        _data = Manager.Data.Enemy[id];
        _isDead = false;
        _enemyType = type;

        transform.position = (Vector2)_character.transform.position + newPosition;


        SetNormalAnim();

        SetupStats();
        SetCollider();

        _moveEnemy = MoveEnemy();
        _backMoveEnemy = BackMoveEnemy();
        StartCoroutine(_moveEnemy);
    }
    void SetupStats()
    {
        _speed = _data.Speed;

        switch (_enemyType)
        {
            case EnemyType.Normal:
                transform.localScale = new Vector2(0.03f, 0.03f);
                _hp = _data.Hp;
                break;
            case EnemyType.MiniBoss:
                transform.localScale = new Vector2(0.07f, 0.07f);
                _hp = _data.Hp * 10;
                break;
            case EnemyType.Boss:
                transform.localScale = new Vector2(0.1f, 0.1f);
                _hp = _data.Hp;
                break;
        }

    }
    void SetNormalAnim()
    {
        Animator anim = GetComponent<Animator>();
        var overrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        overrideController[Define.Anim.Anim_run] = Manager.Asset.LoadAnimClip($"Ani_{_data.Sprite}_0");
        overrideController[Define.Anim.Ani_AttackSkill] = Manager.Asset.LoadAnimClip(_data.Skill);
        _animator.runtimeAnimatorController = overrideController;
    }
 
    void DamageStrike(Collider2D collision)
    {
        if (collision.CompareTag("Idol"))
        {
            int damage = _data.Attack * -1;
            _character.GetHp(damage);
        }
    }


    public void TakeDamage(float damage)
    {
        _hp -= damage;
        if (_isDead == false)
        {
            Manager.Spawn.SpawnDamageText(damage, transform.position);
        }

        if (_hp <= 0)
        {
            Die();
            _isDead = true;
        }
    }


    protected IEnumerator MoveEnemy()
    {
        while (gameObject.activeSelf) 
        {
            Vector2 direction = (Vector2)transform.position + GetForward * _speed * Time.fixedDeltaTime;
            _body.MovePosition(direction);
            
            yield return null;
        }
    }
    IEnumerator BackMoveEnemy()
    {
        float timer = 0f;
        float moveTime = 3f;
        while (isActiveAndEnabled)
        {
            while (timer < moveTime)
            {
                Vector2 direction = (Vector2)transform.position - GetForward * _speed * Time.fixedDeltaTime;
                _body.MovePosition(direction);

                Color newColor = _enemyRenderer.color;
                newColor.a -= Time.fixedDeltaTime * 0.5f;
                _enemyRenderer.color = newColor;

                timer += Time.fixedDeltaTime;
                yield return null;
            }
            _enemyRenderer.color = Color.white;
            timer = 0f;
            Manager.Spawn.Enemy.Release(this);
        }
    }
    void Die()
    {
        if (_isDead)
            return;

        if(_enemyType == EnemyType.Normal) 
        {
            Manager.Spawn.EnemyReward(transform);
        }
        else 
        {
            Manager.Spawn.BossReward(transform);
        }

        Manager.Sound.Play(Define.SoundType.Effect, "PlayerDamaged");

        StopCoroutine(_moveEnemy);
        StartCoroutine(_backMoveEnemy);

    }
    Vector2 GetForward => (_character.transform.position - transform.position).normalized;
    public Animator GetAnim() => _animator;
    void SetCollider()
    {
        if (_enemyType == EnemyType.Boss)
        {
            _boxCollider.offset = new Vector2(0, 20);
            _boxCollider.size = new Vector2(30, 30);
        }
        else
        {
            _boxCollider.offset = new Vector2(0, 14);
            _boxCollider.size = new Vector2(20, 30);
        }
    }
}
