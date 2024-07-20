using UnityEngine;
using UniRx.Triggers;
using UniRx;

public class IdolAnim : MonoBehaviour
{
    IdolMove _idolMove;
    Animator _idolAnim;
    void Start() => Init();
    void Init()
    {
        _idolAnim = Utils.FindChild<Animator>(gameObject, "Body");
        _idolMove = transform.GetComponent<IdolMove>();

        SetAnim();
        //_idolMove.Input_Vector2.Subscribe(IdolRunAnim).AddTo(this);
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
    }
}
