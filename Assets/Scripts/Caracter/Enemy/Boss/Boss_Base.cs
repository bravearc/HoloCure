using System.Collections;
using UnityEngine;

public abstract class Boss_Base : MonoBehaviour
{
    protected Enemy _enemy;
    protected float _skillTimer = 0;
    void Start() => Init();
    void Init()
    {
        _enemy = transform.parent.GetComponent<Enemy>();
        SetBoss();
    }

    protected virtual void SetBoss() 
    {
        StartCoroutine(AttackAnimCo());
    }

    IEnumerator AttackAnimCo()
    {
        while (true)
        {
            if (_skillTimer > 5)
            {
                Animator animator = _enemy.GetAnim();
                animator.SetTrigger(Define.Anim.Ani_AttackSkill);
                _skillTimer = 0;
            }
            _skillTimer += Time.deltaTime;
            yield return null;
        }
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
