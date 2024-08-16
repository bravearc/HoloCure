using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Collections;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Popup_ItemBox : UI_Popup
{
    #region enum
    protected enum Buttons
    {
        TakeButton,
        CancelButton,

    }
    protected enum Texts
    {
        TakeText,
        CancelText,
        NameText,
        ExplanationText,
        TypeText,
        NewText
    }
    protected enum Images
    {
        TakeButton,
        CancelButton,
        GetItemImage0,
        GetItemImage1,
        TypeImage,
        ItemFrameImage
    }
    protected enum Objects
    {
        OpenButton,
        CloseBox,
        Pointer
    }
    protected enum Anis
    {
        Box,
    }
    #endregion
    IDisposable _updateDisposable;
    Inventory _inventory;
    ItemID _id;
    public RectTransform _pointer;

    Buttons _currentButon;
    Buttons CurrentButton 
    { 
        get
        {
            return _currentButon;
        } 
        set
        {
            SetButtonNormal(_currentButon);
            SetButtonsHighlight(value);
            _currentButon = value;
        }
    }
    protected override void Init()
    {
        base.Init();
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindObject(typeof(Objects));
        BindImage(typeof(Images));
        BindAnimator(typeof(Anis));

        _pointer = Utils.GetOrAddComponent<RectTransform>(GetObject((int)Objects.Pointer));
        
        GetAnimator((int)Anis.Box).updateMode = AnimatorUpdateMode.UnscaledTime;
        GetObject((int)Objects.CloseBox).SetActive(false);

        _updateDisposable =
            this.UpdateAsObservable().Subscribe(_ => KeyCheck());

        CurrentButton = _currentButon;

        for (int idx = 0; idx < Enum.GetValues(typeof(Buttons)).Length; idx++) 
        { 
            Button button = GetButton(idx);
            button.BindEvent(OnEnterButton, Define.UIEvent.Enter, this);
            button.BindEvent(OnClickButton, Define.UIEvent.Click, this);
        }
        Manager.Sound.Stop(Define.SoundType.BGM);
        Manager.Sound.Play(Define.SoundType.Effect, "BoxOpenStart");
        Time.timeScale = 0f;
    }

    protected void KeyCheck()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Manager.Sound.Play(Define.SoundType.Effect, "BoxOpenOngoing");
            OpenBox();
            _updateDisposable.Dispose();
        }
    }
    void SetupItem()
    {
        _inventory = Manager.Game.Inventory;
        _id = Manager.Spawn.GetRandomItem();
        if (Utils.IsWeapon(_id))
        {
            WeaponData data;
            string newtext = "";
            Weapon weapon = _inventory.Weapons.Find(weapon => weapon.ID == _id);
            if (weapon != null) 
            {
                int level = weapon.Level.Value;
                data = Manager.Data.Weapon[_id][level + 1];
                
            }
            else
            {
                data = Manager.Data.Weapon[_id][1];
                newtext = "New!";
            }
            if(GetText((int)Texts.NameText) == null)
            {
                Debug.Log($"{GetText((int)Texts.NameText)}: null");
            }
            GetText((int)Texts.NameText).text = data.Name;
            GetText((int)Texts.TypeText).text = ">> Weapon";
            string sprite = Manager.Data.Item[_id].IconImage;
            GetImage((int)Images.GetItemImage0).sprite = Manager.Asset.LoadSprite(sprite);
            GetImage((int)Images.GetItemImage1).sprite = Manager.Asset.LoadSprite(sprite);
            GetImage((int)Images.TypeImage).sprite = Manager.Asset.LoadSprite(data.WeaponType.ToString());
            GetImage((int)Images.ItemFrameImage).sprite = Manager.Asset.LoadSprite("spr_optionWeaponIcon_0");
            GetText((int)Texts.ExplanationText).text = data.Explanation;
            GetText((int)Texts.NewText).text = newtext;
        }
        else
        {
            EquipmentData data;
            string newtext = "";
            Equipment equipment = _inventory.Equipments.Find(weapon => weapon.ID == _id);
            if (equipment != null)
            {
                int level = equipment.Level.Value;
                data = Manager.Data.Equipment[_id][level + 1];
                newtext = "New!";
            }
            else
            {
                data = Manager.Data.Equipment[_id][1]; ;
            }
            GetText((int)Texts.NameText).text = data.Name;
            GetText((int)Texts.TypeText).text = ">> Equipment";
            GetImage((int)Images.GetItemImage0).sprite = Manager.Asset.LoadSprite(data.Name);
            GetImage((int)Images.GetItemImage1).sprite = Manager.Asset.LoadSprite(data.Name);
            GetImage((int)Images.TypeImage).sprite = null;
            GetImage((int)Images.ItemFrameImage).sprite = Manager.Asset.LoadSprite("spr_optionsprEquipmentIcon_0");
            GetText((int)Texts.ExplanationText).text = data.Explanation;
            GetText((int)Texts.NewText).text = newtext;
        }

    }

    void OpenBox()
    {
        Animator animator = GetAnimator((int)Anis.Box);
        animator.SetTrigger(Define.Anim.Anim_BoxMove);

        GetObject((int)Objects.OpenButton).SetActive(false);


        Observable.EveryUpdate()
            .SkipWhile(_ => animator.GetCurrentAnimatorStateInfo(0).shortNameHash != Animator.StringToHash("NormalBox"))
            .TakeWhile(_ => animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            .Subscribe(_ =>
            {
                Debug.Log("´ë±âÁß");
            },
            () =>
            {
                Manager.Sound.Stop(Define.SoundType.Effect);
                Manager.Sound.Play(Define.SoundType.Effect, "BoxOpenEnd");
                GetObject((int)Objects.CloseBox).SetActive(true);
                Manager.UI.MakeSubItem<SubItem_Stats>(transform);
                SetupItem();
            });
    }

    protected override void OnEnterButton(PointerEventData data)
    {
        Buttons button = Enum.Parse<Buttons>(data.pointerEnter.name);
        CurrentButton = button;
        Manager.Sound.Play(Define.SoundType.Effect, Define.Sound.ButtonMove);
    }

    protected override void OnClickButton(PointerEventData data)
    {

        Buttons button = Enum.Parse<Buttons>(data.pointerEnter.name);
        switch(button)
        {
            case Buttons.TakeButton:
                _inventory.GetItem(_id);
                break;
            case Buttons.CancelButton: break;
            default: break;
        }
        Manager.Sound.Play(Define.SoundType.Effect, Define.Sound.ButtonClick);
        Manager.Sound.Play(Define.SoundType.BGM, "StageOneBGM");
        base.OnClickButton(data);
        Time.timeScale = 1f;
    }

    void SetButtonNormal(Buttons button)
    {
        GetImage((int)button).sprite = Manager.Asset.LoadSprite("hud_Button_0");
        GetText((int)button).color = Color.white;
    }

    void SetButtonsHighlight(Buttons button) 
    {
        GetImage((int)button).sprite = Manager.Asset.LoadSprite("hud_Button_1");
        GetText((int)button).color = Color.black;
        float resize = -586;
        float pointerY = GetImage((int)button).rectTransform.position.y + resize;
        _pointer.anchoredPosition = new Vector2(455, pointerY);
    }

    bool IsFever()
    {
        return UnityEngine.Random.Range(1, 101) < 30 ?  true : false;
    }
}
