using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniRx.Triggers;

public class SubItem_ChooseMode : UI_SubItem
{
    #region enum
    protected enum Buttons
    {
        StageModeButton,
        EndlessModeButton
    }
    protected enum Objects
    {
        Pointer
    }
    protected enum Animators
    {
        Pointer
    }
    #endregion

    Buttons _currentButton;
    Buttons CurrentButon
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
    Popup_Select _select;
    protected override void Init()
    {
        base.Init();

        BindButton(typeof(Buttons));
        BindObject(typeof(Objects));
        BindAnimator(typeof(Animators));

        _select = transform.parent.GetComponent<Popup_Select>();

        for (int idx = 0; idx < Enum.GetValues(typeof(Buttons)).Length; idx++) 
        {
            Button button = GetButton(idx);
            button.BindEvent(OnEnterButton, Define.UIEvent.Enter, this);
            button.BindEvent(OnClickButton, Define.UIEvent.Click, this);
        }

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

    private void ProcessCancel()
    {
        base.CloseSubItem();
        _select.ShowIdol();
    }

    void OnEnterButton(PointerEventData data)
    {
        Buttons buttons = Enum.Parse<Buttons>(data.pointerEnter.name);
        CurrentButon = buttons;

    }
    void OnClickButton(PointerEventData data)
    {
        Buttons buttons = Enum.Parse<Buttons>(data.pointerClick.name);
        ProcessButton(buttons);
    }

    void SetButtonHighlighted(Buttons button) 
    {
        GetAnimator((int)Animators.Pointer).SetTrigger(Define.Anim.PointerMove);
        RectTransform _pointerRect = Utils.GetOrAddComponent<RectTransform>(GetObject((int)Objects.Pointer));
        RectTransform buttonRect = GetButton((int)button).GetComponent<RectTransform>();

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_pointerRect.parent as RectTransform, buttonRect.position, null, out localPoint);

        _pointerRect.anchoredPosition = localPoint;
    }

    void ProcessButton(Buttons button)
    {
        switch (button)
        {
            case Buttons.StageModeButton : 
                Manager.Game.SetStageMode(true); break;
            case Buttons.EndlessModeButton : 
                Manager.Game.SetStageMode(false); break;
        }

        _select.ShowStage();
        base.CloseSubItem();
    }
}
