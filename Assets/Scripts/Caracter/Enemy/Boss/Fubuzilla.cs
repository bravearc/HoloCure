using System;
using System.Collections;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Fubuzilla : Boss_Base
{
    IEnumerator _warningCo;
    IDisposable _disposable;
    GameObject warningLine;
    protected override void SetBoss(Enemy boss)
    {
        _warningCo = WarningCo();
        _animator = GetAnim(gameObject);

        transform.localPosition = new Vector2(0, 26);
        warningLine = transform.Find("WarningLine").gameObject;
        warningLine.SetActive(false);

        _disposable = this.OnDisableAsObservable().Subscribe(_ =>
        {
            StopCoroutine(_warningCo);
            _disposable?.Dispose();
            _warningCo = null;
            _disposable = null;
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

            //_bossSprite.flipX = IsBossFlip();
            Debug.Log("start");
            yield return new WaitForSeconds(0.3f);
            warningLine.SetActive(true);

            Debug.Log("ing..");
            _animator.SetTrigger(Define.Anim.Ani_AttackSkill);
            yield return new WaitForSeconds(1.5f);

            Debug.Log("end");
            warningLine.SetActive(false);
            StopCoroutine(_warningCo);
        }
    }
}
