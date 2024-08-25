using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class SubItem_Quit : UI_SubItem
{
    #region enum
    enum Buttons
    {
        YesButton,
        NoButton
    }
    enum Texts
    {
        YesText,
        NoText
    }
    enum Images
    {
        YesButton,
        NoButton
    }
    #endregion
    Popup_Paused _popup_Paused;

    Buttons _currentButton = 0;
    Buttons CurrentButton
    {
        get
        {
            return _currentButton;
        }
        set
        {
            SetButtonNormal(_currentButton);
            SetButtonHighligthed(value);
            _currentButton = value;
        }
    }
    protected override void Init()
    {
        base.Init();
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));

        for (int i = 0; i < 2; i++)
        {
            BindEvent(GetButton(i), OnClickButton, Define.UIEvent.Click, this);
            BindEvent(GetButton(i), OnEnterButton, Define.UIEvent.Enter, this);
        }

        _popup_Paused = transform.parent.GetComponent<Popup_Paused>();
    }
    protected override void OnPressKey()
    {
        if (Input.GetButtonDown(Define.Key.UP))
        {
            int idx = (int)CurrentButton == 0 ? 1 : 0;
            CurrentButton = (Buttons)idx;
        }
        else if (Input.GetButtonDown(Define.Key.DOWN))
        {
            int idx = (int)CurrentButton == 1 ? 0 : 1;
            CurrentButton = (Buttons)idx;
        }

        if (Input.GetButtonDown(Define.Key.CONFIRM)) 
        {
            ProcessButton(CurrentButton);
        }
        else if (Input.GetButtonDown(Define.Key.CANCEL))
        {
            ProcessButton(Buttons.NoButton);
        }
    }

    void OnEnterButton(PointerEventData data)
    {
        Buttons button = Enum.Parse<Buttons>(data.pointerEnter.name);
        CurrentButton = button;
    }
    void OnClickButton(PointerEventData data)
    {
        Manager.Sound.Play(Define.SoundType.Effect, Define.Sound.ButtonClick);
        Buttons button = Enum.Parse<Buttons>(data.pointerClick.name);

        ProcessButton(button);
    }

    void ProcessButton(Buttons button)
    {
        switch (button)
        {
            case Buttons.YesButton:
                Manager.Game.GameOver();
                break;
            case Buttons.NoButton:
                Manager.UI.MakeSubItem<SubItem_Paused>(_popup_Paused.transform);
                base.CloseSubItem();
                break;
        }

        Time.timeScale = 1f;
    }

    void SetButtonNormal(Buttons button)
    {
        GetText((int)button).color = Color.white;
        GetImage((int)button).sprite = Manager.Asset.LoadSprite("hud_Button_0");
    }

    void SetButtonHighligthed(Buttons button)
    {
        Manager.Sound.Play(Define.SoundType.Effect, Define.Sound.ButtonMove);
        GetText((int)button).color = Color.black;
        GetImage((int)button).sprite = Manager.Asset.LoadSprite("hud_OptionButton_1");
    }
}
