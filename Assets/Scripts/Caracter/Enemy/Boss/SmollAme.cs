using System;
using UniRx;
using UniRx.Triggers;

public class SmollAme : Boss_Base
{
    protected override void SetBoss(Enemy boss)
    {
        _animator = boss.GetAnim();
        IDisposable disposable = boss.OnDisableAsObservable().Subscribe(_ =>
        {
            GameClear();
        });
    }
    protected override void BossAction()
    {
        _animator.SetTrigger(Define.Anim.Ani_AttackSkill);
    }

}
