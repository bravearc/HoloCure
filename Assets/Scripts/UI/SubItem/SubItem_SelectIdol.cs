using System;
using UniRx;
using UniRx.Triggers;
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

    Buttons _currentButton;
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
    public ReactiveProperty<CharacterID> ID = new();
    
    Popup_Select _select;
    const int _randomButton = 29;

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
        if (Input.GetButtonDown(Define.KeyCode.CONFIRM))
        {

        }
        else if (Input.GetButtonDown(Define.KeyCode.CANCEL))
        {
            ProcessCancel();
        }
    }
    private void OnEnterButton(PointerEventData data)
    {
        Buttons button = Enum.Parse<Buttons>(data.pointerEnter.name);
        CurrentButton = button;
    }    
    private void OnClickButton(PointerEventData data)
    {
        ProcessButton();
    }

    private void SetButtonHighlighted(Buttons button)
    {
        //»ç¿îµå
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
