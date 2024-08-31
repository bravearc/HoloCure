using System;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Util.Pool;

public class Attack : MonoBehaviour
{
    #region Component
    private WeaponData _weaponData;
    private BoxCollider2D _collider;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private AnimationClip _animClip;
    private Action<Attack> _action;
    private Action<Attack> _impectAction;
    private Transform _character;
    private Transform _cursor;
    private ParticleSystem _particleSystem;
    private Coroutine _currentCoroutine;
    #endregion
    IDisposable _trigger;

    bool _isActive;
    public float HoldingTime;

    void Awake()
    {
        _rb = Utils.GetOrAddComponent<Rigidbody2D>(gameObject);
        _spriteRenderer = Utils.GetOrAddComponent<SpriteRenderer>(gameObject);
        _spriteRenderer.drawMode = SpriteDrawMode.Simple;
        _animator = Utils.GetOrAddComponent<Animator>(gameObject);
        _particleSystem = Utils.GetOrAddComponent<ParticleSystem>(gameObject);
         _character = Manager.Game.Character.transform;
        _collider = Utils.GetOrAddComponent<BoxCollider2D>(gameObject);
        _cursor = Manager.Game.Character.Cursor;
    }
    private void OnEnable()
    {
        Utils.ResetRigidbody(GetRigid());
        Utils.ResetParticle(gameObject);
        _isActive = false;
    }
    public void Init(WeaponData data, Action<Attack> action)
    {

        _weaponData = data;
        transform.rotation = CalculateRotation();
        SetSpriteOrAnim();

        _spriteRenderer.flipY = IsFlip();
        _action = action;

        _trigger = this.OnTriggerEnter2DAsObservable().Subscribe(StirkeTrigger);

        _currentCoroutine = StartCoroutine(StrikeShot());
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
    Quaternion CalculateRotation()
    {
        Vector2 direction = (_cursor.position - _character.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, angle);
    }
    public void SetAttackComponent(bool aniTime, bool aniActive, Vector2 size, Vector2 col, Vector2 offset, bool particle = false, float gravity = 0, bool isActive = false, Action<Attack> action = null)
    {
        HoldingTime = aniTime ? _animClip.length : _weaponData.Duration;
        _animator.enabled = aniActive;
        
        var emission = _particleSystem.emission;
        emission.enabled = particle;

        _rb.gravityScale = gravity;

        transform.localScale = size;

        _collider.size = col;
        _collider.offset = offset;

        _isActive = isActive;
        _impectAction = action;
    }
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

    public bool IsAnim() => Manager.Asset.LoadAnimClip($"Ani_{_weaponData.Animation}_0") != null; 
    public bool IsFlip() => _character.position.x > _cursor.position.x ;
    public void OffCollider() => _collider.enabled = false;
    public void OnCollider() => _collider.enabled = true;
    public void SetFlipY(bool boo) => _spriteRenderer.flipY = boo;
    public float GetAniLength() => _animClip.length;
    public SpriteRenderer GetSprite() => _spriteRenderer;
    public Rigidbody2D GetRigid() => _rb;
    public void StopCurrentAction()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }
        _rb.velocity = Vector2.zero; 
    }
    private void AttackDie()
    {
        _trigger?.Dispose();
        _impectAction?.Invoke(this);
        Manager.Spawn.Attack.Release(this);
    }
}
