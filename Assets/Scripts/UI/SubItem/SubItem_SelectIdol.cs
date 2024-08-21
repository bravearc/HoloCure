using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SubItem_SelectIdol : UI_SubItem
{
    #region enum
    protected enum Buttons 
    {
        WatsonAmelia,
        GawrGura,
        NinomaeInanis,
        TakanashiKiara,
        MoriCalliope,
        HakosBaelz,
        OuroKronii,
        CeresFauna,
        NanashiMumei,
        TsukumoSana,
        IRyS,
        ShirakamiFubuki,
        OokamiMio,
        NekomataOkayu,
        InugamiKorone,
        TokinoSora,
        AZKi,
        RobocoSan,
        HoshimachiSuisei,
        SakuraMiko,
        AkaiHaato,
        YozoraMel,
        NatsuiroMatsuri,
        AkiRosenthal,
        OozoraSubaru,
        YuzukiChoco,
        MurasakiShion,
        NakiriAyame,
        MinatoAqua,
        Rendom
    }
    protected enum Objects
    {
        Pointer
    }
    #endregion

    public ReactiveProperty<CharacterID> ID = new();
    
    Popup_Select _select;

    Buttons _currentButton = 0;
    Buttons CurrentButton 
    { 
        get 
        { 
            return _currentButton;
        }
        set
        {
            SetButtonHighlighted(value);
            _currentButton = value;
        }
    }

    protected override void Init()
    {
        base.Init();
        BindButton(typeof(Buttons));
        BindObject(typeof(Objects));
        _select = transform.parent.GetComponent<Popup_Select>();

        for (int idx = 0; idx < Enum.GetValues(typeof(Buttons)).Length; idx++) 
        { 
            Button button = GetButton(idx);
            button.BindEvent(OnEnterButton, Define.UIEvent.Enter, this);
            button.BindEvent(OnClickButton, Define.UIEvent.Click, this);
        }
        CurrentButton = _currentButton;

        ID.Value = (CharacterID)1;
    }

    protected override void OnPressKey()
    {
        if (Input.GetButtonDown(Define.Key.CONFIRM))
        {
            ProcessButton();
        }
        else if (Input.GetButtonDown(Define.Key.CANCEL))
        {
            ProcessCancel();
        }

        if (Input.GetButtonDown(Define.Key.UP)) { CurrentButton = CurrentButtonIndex(-5); }
        else if (Input.GetButtonDown(Define.Key.DOWN)) { CurrentButton = CurrentButtonIndex(5); }
        else if (Input.GetButtonDown(Define.Key.LEFT)) { CurrentButton = CurrentButtonIndex(-1); }
        else if (Input.GetButtonDown(Define.Key.RIGHT)) { CurrentButton = CurrentButtonIndex(1); }
    }
    Buttons CurrentButtonIndex(int idx)
    {
        Buttons nextButton = CurrentButton + idx;
        if ((int)nextButton < 0) 
        {
            nextButton = (Buttons)29;
        }
        else if((int)nextButton > 29) 
        {
            nextButton = (Buttons)0;
        }

        return nextButton;
    }
    private void OnEnterButton(PointerEventData data)
    {
        Buttons button = Enum.Parse<Buttons>(data.pointerEnter.name);
        CurrentButton = button;
    }    
    private void OnClickButton(PointerEventData data)
    {
        Manager.Sound.Play(Define.SoundType.Effect, Define.Sound.ButtonClick);
        ProcessButton();
    }

    private void SetButtonHighlighted(Buttons button)
    {
        Manager.Sound.Play(Define.SoundType.Effect, Define.Sound.ButtonMove);
        RectTransform _pointerRect = Utils.GetOrAddComponent<RectTransform>(GetObject((int)Objects.Pointer));
        RectTransform buttonRect = GetButton((int)button).GetComponent<RectTransform>();

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_pointerRect.parent as RectTransform, buttonRect.position, null, out localPoint);
        _pointerRect.anchoredPosition = localPoint;

        ID.Value = (CharacterID)button + 1;
    }

    private void ProcessButton()
    {
        Manager.Game.SetCharacterID(ID.Value);
        _select.ShowMode();
        base.CloseSubItem();
    }

    private void ProcessCancel()
    {
        Manager.UI.CloseALLPopupUI();
        Manager.UI.ShowPopup<Popup_Title>();
    }

}
