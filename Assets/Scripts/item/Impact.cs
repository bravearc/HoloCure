using System.Collections;
using UnityEngine;

public class Impact : MonoBehaviour
{
    Animator _anim;
    SpriteRenderer spriteRenderer;
    float _holdingTime;
    IEnumerator _aniCo;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 2;
    }
    public void Init(string animName, Vector2 size)
    {
        transform.localScale = size;
        AnimationClip ani = Manager.Asset.LoadAnimClip(animName);
        Animator animator = _anim;
        var overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        overrideController[Define.Anim.Anim_Attack] = ani;
        _anim.runtimeAnimatorController = overrideController;
        _holdingTime = ani.length;
        spriteRenderer.drawMode = SpriteDrawMode.Simple;
        transform.localScale = new Vector2(0.025f, 0.025f);
        _aniCo = AnimCo();
        StartCoroutine(_aniCo);
    }

    IEnumerator AnimCo()
    {
        yield return new WaitForSeconds(_holdingTime);
        StopCoroutine(_aniCo);
        _aniCo = null;
        Manager.Spawn.Impect.Release(this);
    }
}
