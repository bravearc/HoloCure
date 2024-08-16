using UnityEngine;
using System.Collections;
using UniRx.Triggers;
using UniRx;
using System;
using static System.Collections.Specialized.BitVector32;
public enum EnemyType
{
    Normal,
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
    float _speed = 2f;
    float _hp;
    float _bossSize = 3;

    Action<Enemy> _action;

    void Awake() 
    {
        _enemyRenderer = Utils.GetOrAddComponent<SpriteRenderer>(gameObject);
        _character = Manager.Game.Character;
        _body = Utils.GetOrAddComponent<Rigidbody2D>(gameObject);
        _animator = Utils.GetOrAddComponent<Animator>(gameObject);
        _boxCollider = Utils.GetOrAddComponent<BoxCollider2D>(gameObject);
        gameObject.tag = Define.Tag.ENEMY;

        _boxCollider.offset = new Vector2(0, 14);
        _boxCollider.size = new Vector2(20, 30);
        _enemyRenderer.drawMode = SpriteDrawMode.Simple;

        _enemyRenderer.sortingOrder = 2;
        SpriteRenderer shadowRenderer = transform.Find("Shadow_0").GetComponent<SpriteRenderer>();
        shadowRenderer.sortingOrder = 1;
        shadowRenderer.drawMode = SpriteDrawMode.Simple;
        shadowRenderer.transform.localPosition = new Vector2(-0.7f, 0);
        shadowRenderer.transform.localScale = new Vector2(1, 1);


        this.OnTriggerEnter2DAsObservable().Subscribe(DamageStrike);
    }

    public void Init(EnemyID id, Vector2 newPosition, bool _isBoss = false)
    {
        _data = Manager.Data.Enemy[id];
        _speed = _data.Speed;
        _hp = _isBoss ? _data.Hp * 10 : _data.Hp;
        _isDead = false;
        _action = null;
        _enemyType = _isBoss ? EnemyType.Boss : EnemyType.Normal;

        transform.position = (Vector2)_character.transform.position + newPosition;

        if(_isBoss == false)
        {
            SetSize();
        }
        
        SetAnim();
        _moveEnemy = MoveEnemy();
        _backMoveEnemy = BackMoveEnemy();
        StartCoroutine(_moveEnemy);
    }
    void SetSize(Vector2? size = null)
    {
        if(size == null)
            transform.localScale = new Vector2(0.03f, 0.03f);
        else
            transform.localScale = (Vector2)size;

    }
    void SetAnim()
    {
        Animator anim = GetComponent<Animator>();
        var overrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        overrideController[Define.Anim.Anim_run] = Manager.Asset.LoadAnimClip($"Ani_{_data.Sprite}_0");
        overrideController[Define.Anim.Ani_AttackSkill] = Manager.Asset.LoadAnimClip($"Ani_{_data.Skill}_0");

        anim.runtimeAnimatorController = overrideController;
    }
 
    public void SetBoss(Vector2 size)
    {
        SetSize(size);
        _action?.Invoke(this);

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
        Manager.Spawn.SpawnDamageText(damage, transform.position);

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
            Manager.Spawn.EnemyReward(transform);
        else
            Manager.Spawn.BossReward(transform);

        Manager.Sound.Play(Define.SoundType.Effect, "PlayerDamaged");

        _action = null;
        StopCoroutine(_moveEnemy);
        StartCoroutine(_backMoveEnemy);

    }
    Vector2 GetForward => (_character.transform.position - transform.position).normalized;
    public Animator GetAnim() => _animator;
}
