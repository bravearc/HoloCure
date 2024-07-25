using UnityEngine;
using System.Collections;
using UniRx.Triggers;
using UniRx;
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
    SpriteRenderer _spriteRenderer;
    EnemyType _enemyType;
    IEnumerator _moveEnemy;

    bool _isMove;
    float _speed = 2f;
    float _hp;
    float _attack;
    float _bossSize = 3;
    const int BOSS_NUMBER = 1000;

    void Awake() 
    {
        _spriteRenderer = Utils.GetOrAddComponent<SpriteRenderer>(gameObject);
        _character = Manager.Game.Character;
        _body = Utils.GetOrAddComponent<Rigidbody2D>(gameObject);
        _animator = Utils.GetOrAddComponent<Animator>(gameObject);
    }

    public void Init(EnemyID id, Vector2 newPosition)
    {
        _data = Manager.Data.Enemy[id];
        _speed = _data.Speed;
        _hp = _data.Hp;

        if ((int)id < BOSS_NUMBER)
            _enemyType = EnemyType.Normal;
        else
            _enemyType = EnemyType.Boss;

        transform.position = (Vector2)_character.transform.position + newPosition;

        SetAnim();
        SetSize();
        this.OnTriggerEnter2DAsObservable().Subscribe(AttackCo);
        _moveEnemy = MoveEnemy();
        StartCoroutine(_moveEnemy);
    }
    protected void SetSize()
    {
        switch (_enemyType)
        {
            case EnemyType.Normal:
                break;
            case EnemyType.Boss:
                transform.localScale = new Vector3(_bossSize, _bossSize, 1);
                break;
        }
        _spriteRenderer.sortingOrder = 2;
        SpriteRenderer spriteRenderer = transform.Find("Shadow_0").GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 1;
    }
    protected void SetAnim()
    {
        Animator anim = GetComponent<Animator>();
        var overrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        overrideController[Define.Anim.Anim_run] = Manager.Asset.LoadAnimClip($"Ani_{_data.Sprite}_0");
        overrideController[Define.Anim.Anim_TakeDamage] = Manager.Asset.LoadAnimClip($"Ani_{_data.Sprite}_0");

        anim.runtimeAnimatorController = overrideController;
    }
 
    void AttackCo(Collider2D collision)
    {
        if (collision.CompareTag("Idol"))
        {
            _character.Update_Hp(_data.Attack);
        }
    }

    private IEnumerator MoveEnemy()
    {
        while (gameObject.activeSelf) 
        {
            Vector2 newPosition = (Vector2)transform.position + GetForward * _speed * Time.fixedDeltaTime;
            _body.MovePosition(newPosition);
            
            yield return null;
        }

        StopCoroutine(_moveEnemy);
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage;
        Manager.Spawn.SpawnDamageText(damage, transform);

        if (_hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {

        Manager.Spawn.SpawnExp(GetExpID(), transform);
        Manager.Spawn.Enemy.Release(this);

    }

    ExpData GetExpID()
    {
        int normal = Random.Range(1, 100);
        int id = normal switch
        {
            < 40 => 1,
            < 60 => 2,
            < 75 => 3,
            < 90 => 4,
            _ => 5
        };
        return Manager.Data.Exp[id];
    }
    Vector2 GetForward => (_character.transform.position - transform.position).normalized;
}
