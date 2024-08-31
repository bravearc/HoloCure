using System;
using System.Collections;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Fubuzilla : Boss_Base
{
    IEnumerator _warningCo;
    IDisposable _disposable;
    GameObject _warningLine;
    protected override void SetBoss(Enemy boss)
    {
        _warningCo = WarningCo();
        _animator = GetAnim(gameObject);

        transform.localPosition = new Vector2(0, 26);
        _warningLine = transform.Find("WarningLine").gameObject;
        _warningLine.SetActive(false);

        transform.rotation = IsBossFlip() == true ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        
        _disposable = boss.OnDisableAsObservable().Subscribe(_ =>
        {
            _disposable?.Dispose();
        });
    }
    protected override void BossAction()
    {
        StartCoroutine(_warningCo);
    }
    IEnumerator WarningCo()
    {
        while (isActiveAndEnabled)
        {
            yield return new WaitForSeconds(0.3f);
            _warningLine.SetActive(true);

            _animator.SetTrigger(Define.Anim.Ani_AttackSkill);
            yield return new WaitForSeconds(1.5f);

            _warningLine.SetActive(false);
            StopCoroutine(_warningCo);
        }
    }
}
