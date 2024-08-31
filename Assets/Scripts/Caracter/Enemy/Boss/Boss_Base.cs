using System.Collections;
using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;

public abstract class Boss_Base : MonoBehaviour
{
    #region Component
    EnemyData _bossData;
    Enemy _boss;
    Character _character;
    SpriteRenderer _bossSprite;
    public Animator _animator;
    IDisposable _disposable;

    protected float _skillTime = 5;
    protected float _skillCount = 0;
    #endregion

    private void Awake()
    {
        _character = Manager.Game.Character;
        _boss = transform.parent.GetComponent<Enemy>();
        _bossSprite = _boss.GetComponent<SpriteRenderer>();
    }
    public void Init(EnemyID id)
    {
        _bossData = Manager.Data.Enemy[id];

        SetBoss(_boss);

        _bossSprite.flipX = IsBossFlip();
        _disposable = _boss.OnDisableAsObservable().Subscribe(_ =>
        {
            Manager.Asset.Destroy(gameObject);
            _disposable?.Dispose();
        });
        StartCoroutine(AttackAnimCo());
    }
    protected virtual void SetBoss(Enemy boss){ }
    protected virtual void BossAction() { }
    IEnumerator AttackAnimCo()
    {

        while (true)
        {
            if (_skillCount > _skillTime)
            {
                BossAction();
                _skillCount = 0;
            }
            _skillCount += Time.deltaTime;
            yield return null;
        }
    }
    protected Animator GetAnim(GameObject go)
    {
        Animator anim = go.GetComponent<Animator>();
        var overrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        overrideController[Define.Anim.Ani_AttackSkill] = Manager.Asset.LoadAnimClip(_bossData.Skill);

        anim.runtimeAnimatorController = overrideController;
        return anim;
    }
    protected bool IsBossFlip()
    {
        return _boss.transform.position.x > _character.transform.position.x;
    }
    protected void GameClear()
    {
        Manager.Game.IsGameClear = true;
        Manager.UI.ShowPopup<Popup_GameOver>();
    }
}
