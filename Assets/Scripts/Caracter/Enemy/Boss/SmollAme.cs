using UnityEngine;
public class SmollAme : Boss_Base
{
    protected override void SetBoss(Enemy boss)
    {
        _animator = boss.GetAnim();
    }
    protected override void BossAction()
    {
        _animator.SetTrigger(Define.Anim.Ani_AttackSkill);
    }
}
