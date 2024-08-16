using UnityEngine;
using UniRx.Triggers;
using UniRx;

public class IdolAnim : MonoBehaviour
{
    
    Animator _idolAnim;
    SpriteRenderer _spriteRenderer;
    CapsuleCollider2D _capsuleCollider;
    void Start() => Init();
    void Init()
    {
        _idolAnim = Utils.FindChild<Animator>(gameObject, "Body");
        _spriteRenderer = Utils.FindChild<SpriteRenderer>(gameObject, "Body");
        _capsuleCollider = Utils.FindChild<CapsuleCollider2D>(gameObject, "Body");

        SetAnim();

        Transform _body = transform.Find("Body").GetComponent<Transform>();
        _body.localScale = new Vector2(0.025f, 0.025f);
        _body.localPosition = new Vector2(0.1f, -0.31f);
        _spriteRenderer.drawMode = SpriteDrawMode.Simple;
        _capsuleCollider.offset = new Vector2(0, 17);
        _capsuleCollider.size = new Vector2(23, 36);

        this.UpdateAsObservable().Subscribe(_ => IdolRunAnim());
    }
    void SetAnim()
    {
        var overrideController = new AnimatorOverrideController(_idolAnim.runtimeAnimatorController);
        
        CharacterData data = Manager.Game.GetCharacterData();
        string newAni = data.Sprite;
        overrideController[Define.Anim.Anim_Idol] = Manager.Asset.LoadAnimClip($"Ani_Idle_{newAni}_0");
        overrideController[Define.Anim.Anim_run] = Manager.Asset.LoadAnimClip($"Ani_run_{newAni}_0");

        _idolAnim.runtimeAnimatorController = overrideController;
    }

    void IdolRunAnim()
    {
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)
            || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            _idolAnim.SetBool(Define.Anim.Idol_Play_Run, true);
        }
        else
            _idolAnim.SetBool(Define.Anim.Idol_Play_Run, false);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _spriteRenderer.flipX = true;
        }
        else
            _spriteRenderer.flipX = false;
    }
}
