using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;

public class Popup_Select : UI_Popup
{
    #region enum
    protected enum Images
    {
        CharacterBGImage,
        NormalWeaponImage,
        SkillImage
    }
    protected enum Texts
    {
        NameText,
        HPText,
        ATKText,
        SPDText,
        CRTText,
        WeaponNameText,
        WeaponDescriptionText,
        SkillNameText,
        SkillDescriptionText
    }
    protected enum Animators
    {
        Popup_Select,
        CharacterAni
    }


    protected enum Objects
    {
        CharacterAni
    }
    #endregion
    [SerializeField] private SubItem_SelectIdol _idol;
    [SerializeField] private SubItem_ChooseMode _mode;
    [SerializeField] private SubItem_ChooseStage _stage;
    
    IDisposable _disposable;
    const int _randomButton = 30;
    bool _isRandom;
    bool _isAnimRun;
    float _intervalTime = 1f;
    float _elapsedTime;
    float _animTime;
    protected override void Init()
    {
        BindImage(typeof(Images));
        BindText(typeof(Texts));
        BindAnimator(typeof(Animators));

        ShowIdol();
        _disposable = this.UpdateAsObservable().Subscribe(_ => OnPressKey());
    }

    void OnPressKey()
    {
        if (Input.GetButtonDown(Define.Key.CONFIRM))
        {

        }
        else if (Input.GetButtonDown(Define.Key.CANCEL))
        {

        }

        ShuffleIdol();
        AnimTrigger();
    }
  
    public void ShowIdol()
    {
        _idol = Manager.UI.MakeSubItem<SubItem_SelectIdol>(transform);
        BindModelEvent(_idol.ID, UpdateUI_View, this);
    }

    public void ShowMode()
    {
        _mode = Manager.UI.MakeSubItem<SubItem_ChooseMode>(transform);
    }

    public void ShowStage()
    {
        _stage = Manager.UI.MakeSubItem<SubItem_ChooseStage>(transform);
    }

    void UpdateUI_View(CharacterID id)
    {
        if(id == 0) id = (CharacterID)1;

        if((int)id == _randomButton)
        {
            _isRandom = true;
            return;
        }
        
        GetAnimator((int)Animators.Popup_Select).SetTrigger(Define.Anim.PointerMove);
        
        _isRandom = false;

        SelectData data = Manager.Data.Select[id];
        SetTextAndImage(data);
        SetAnimation(data);

    }
    void SetTextAndImage(SelectData data)
    {
        GetText((int)Texts.NameText).text = data.Name;
        GetText((int)Texts.HPText).text = data.Hp.ToString();
        GetText((int)Texts.ATKText).text = data.Attack.ToString();
        GetText((int)Texts.SPDText).text = data.Speed.ToString();
        GetText((int)Texts.CRTText).text = data.Critical.ToString();
        GetText((int)Texts.SkillNameText).text = data.SpecialName;
        GetText((int)Texts.SkillDescriptionText).text = data.SpecialDescription;
        
        GetImage((int)Images.SkillImage).sprite = Manager.Asset.LoadSprite("spr_Default");
        GetText((int)Texts.WeaponNameText).text = data.Name;
        GetText((int)Texts.WeaponDescriptionText).text = data.WeaponDescription;

        string icon = data.Sprite;
        GetImage((int)Images.CharacterBGImage).sprite = Manager.Asset.LoadSprite($"spr_Title_{icon}_0");
        GetImage((int)Images.NormalWeaponImage).sprite = Manager.Asset.LoadSprite($"spr_{icon}weapon_0");
    }
    void SetAnimation(SelectData data)
    {
        Animator ani = GetAnimator((int)Animators.CharacterAni);
        var overrideController = new AnimatorOverrideController(ani.runtimeAnimatorController);

        string newAni = data.Sprite;

        overrideController[Define.Anim.Anim_Idol] = Manager.Asset.LoadAnimClip($"Ani_Idle_{newAni}_UI_0");
        overrideController[Define.Anim.Anim_run] = Manager.Asset.LoadAnimClip($"Ani_run_{newAni}_UI_0");

        ani.runtimeAnimatorController = overrideController;
    }
    void AnimTrigger()
    {
        _animTime += Time.fixedDeltaTime * 0.1f;
        if (_animTime > _intervalTime)
        {
            GetAnimator((int)Animators.CharacterAni).SetTrigger(Define.Anim.Idol_UI_Run);
            _animTime = 0;
        }
    }
    void ShuffleIdol()
    {
        if (!_isRandom)
            return;

        _elapsedTime += Time.fixedDeltaTime;
        if (_elapsedTime > _intervalTime)
        {
            CharacterID id = (CharacterID)UnityEngine.Random.Range(1, _randomButton);
            SelectData data = Manager.Data.Select[id];
            SetTextAndImage(data);
            SetAnimation(data);
            _elapsedTime = 0;
        }

    }

}
