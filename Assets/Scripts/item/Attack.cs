using System;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Attack : MonoBehaviour
{
    WeaponData _weaponData;
    BoxCollider2D _collider;
    Rigidbody2D _rd;
    SpriteRenderer _spriteRenderer;
    Animator _animator;
    AnimationClip _animClip;
    public Action<Attack> _action;
    Transform _character;
    Transform _cursor;
    ParticleSystem _particleSystem;
    IEnumerator _strikeShot;

    bool _isActive;
    float _damage;
    
    public float HoldingTime;
    void Awake()
    {
        _rd = Utils.GetOrAddComponent<Rigidbody2D>(gameObject);
        _spriteRenderer = Utils.GetOrAddComponent<SpriteRenderer>(gameObject);
        _spriteRenderer.drawMode = SpriteDrawMode.Simple;
        _animator = Utils.GetOrAddComponent<Animator>(gameObject);
        _particleSystem = Utils.GetOrAddComponent<ParticleSystem>(gameObject);
         _character = Manager.Game.Character.transform;
        _collider = Utils.GetOrAddComponent<BoxCollider2D>(gameObject);
        _cursor = Manager.Game.Character.Cursor;
    }
    public void Init(WeaponData data, Action<Attack> action)
    {
        _weaponData = data;
        transform.rotation = CalculateRotation();
        SetSpriteOrAnim();
        _spriteRenderer.flipY = IsFlip();
        _isActive = false;
        _action = null;
        _action = action;

        this.OnTriggerEnter2DAsObservable().Subscribe(StirkeTrigger);
        
        _strikeShot = StrikeShot();
        StartCoroutine(_strikeShot);
    }

    void SetSpriteOrAnim()
    {
        if (IsAnim() == true)
        {
            SetAnim();
        }
        else
        {
            _spriteRenderer.sprite = Manager.Asset.LoadSprite(_weaponData.Animation);
        }
    }

    IEnumerator StrikeShot()
    {
        _action?.Invoke(this);

        yield return new WaitForSeconds(HoldingTime);
        AttackDie();
    }

    private void StirkeTrigger(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Enemy enemy = col.GetComponent<Enemy>();
            enemy.TakeDamage(_weaponData.Attack);
            if (_isActive)
            {
                AttackDie();
            }
        }
    }
    public void SetAttackComponent(bool aniTime, bool aniActive, Vector2 size, Vector2 col, Vector2 offset, bool particle = false, float gravity = 0, bool isActive = false)
    {
        HoldingTime = aniTime ? _animClip.length : _weaponData.Duration;
        _animator.enabled = aniActive;
        
        var emission = _particleSystem.emission;
        emission.enabled = particle;

        _rd.gravityScale = gravity;

        transform.localScale = size;

        _collider.size = col;
        _collider.offset = offset;

        _isActive = isActive;
    }
    Quaternion CalculateRotation()
    {
        Vector2 direction = (_cursor.position - _character.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, angle);
    }

    public bool IsAnim() => Manager.Asset.LoadAnimClip($"Ani_{_weaponData.Animation}_0") != null; 
    public bool IsFlip() => _character.position.x > _cursor.position.x ;
    public void OffCollider() => _collider.enabled = false;
    public void OnCollider() => _collider.enabled = true;
    public SpriteRenderer GetSprite() => _spriteRenderer;
    public Rigidbody2D GetRigid() => _rd;
    public void SetFlipY(bool boo) => _spriteRenderer.flipY = boo;
    public float GetAniLength() => _animClip.length;

    public void SetAnim(string aniName = null)
    {
        Animator anim = _animator;
        var overrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        overrideController[Define.Anim.Anim_Attack] = Manager.Asset.LoadAnimClip($"Ani_{_weaponData.Animation}_0");
        _animator.runtimeAnimatorController = overrideController;
        if(aniName == null) 
        { 
            _animClip = Manager.Asset.LoadAnimClip($"Ani_{_weaponData.Animation}_0");
            return;
        }
        _animClip = Manager.Asset.LoadAnimClip(aniName);
    }
    private void AttackDie()
    {
        StopCoroutine(_strikeShot);
        _strikeShot = null;
        _action = null;
        Utils.ResetParticle(gameObject);
        Manager.Spawn.Attack.Release(this);
    }
}
