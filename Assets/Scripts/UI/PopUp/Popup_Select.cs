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
        Popup_Select
    }
    protected enum Animations
    {
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
    protected override void Init()
    {
        BindImage(typeof(Images));
        BindText(typeof(Texts));
        BindAnimation(typeof(Animations));
        BindAnimator(typeof(Animators));

        ShowIdol();
        _disposable = this.UpdateAsObservable().Subscribe(_ => OnPressKey());
    }

    void OnPressKey()
    {
        if (Input.GetButtonDown(Define.KeyCode.CONFIRM))
        {

        }
        else if (Input.GetButtonDown(Define.KeyCode.CANCEL))
        {

        }
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
        ++id;

        GetAnimator((int)Animators.Popup_Select).SetTrigger(Define.AniTrigger.PointerMove);
        SelectData data = Manager.Data.Select[id];

        GetText((int)Texts.NameText).text = data.Name;
        GetText((int)Texts.HPText).text = data.Hp.ToString();
        GetText((int)Texts.ATKText).text = data.Attack.ToString();
        GetText((int)Texts.SPDText).text = data.Speed.ToString();
        GetText((int)Texts.CRTText).text = data.Critical.ToString();
      
        GetAnimation((int)Animations.CharacterAni).clip = Manager.Asset.LoadAniClip(data.AniClip);
        CharacterData character = Manager.Data.Character[id];
        GetImage((int)Images.CharacterBGImage).sprite = Manager.Asset.LoadSprite(character.Sprite_Icon);

        GetText((int)Texts.SkillNameText).text = data.SpecialName;
        GetText((int)Texts.SkillDescriptionText).text = data.SpecialDescription;

        WeaponData weapon = Manager.Data.Weapon[(ItemID)character.NormalWeapon][1];
        GetText((int)Texts.WeaponNameText).text = weapon.Name;
        GetText((int)Texts.WeaponDescriptionText).text = weapon.Explanation;
    }

    void SetAnimation(SelectData data)
    {
        GetAnimation((int)Animations.CharacterAni).clip = Manager.Asset.LoadAniClip(data.AniClip);
    }
}
