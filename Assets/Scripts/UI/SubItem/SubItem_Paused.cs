using System;
using UnityEngine.EventSystems;
using UnityEngine;

public class SubItem_Paused : UI_SubItem
{
    #region enum
    protected enum Buttons
    {
        Skill,
        QuestionMark,
        Resume,
        Setting,
        Quit
    }

    protected enum Texts
    {
        Skill,
        QuestionMark,
        Resume,
        Setting,
        Quit
    }

    protected enum Images
    {
        Skill,
        QuestionMark,
        Resume,
        Setting,
        Quit
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
        for (int i = 0; i < 5; i++)
        {
            BindEvent(GetButton(i), OnClickButton, Define.UIEvent.Click, this);
            BindEvent(GetButton(i), OnEnterButton, Define.UIEvent.Enter, this);
        }
        _popup_Paused = transform.parent.GetComponent<Popup_Paused>();
    }
    protected override void OnPressKey()
    {
        if (Input.GetButtonDown(Define.Key.CANCEL))
        {
            Manager.UI.ClosePopupUI();
            Time.timeScale = 1.0f;
        }
        else if (Input.GetButtonDown(Define.Key.CONFIRM))
        {
            ProcessButton(CurrentButton);
        }

        if (Input.GetButtonDown(Define.Key.UP))
        {
            int nextButton = CurrentButtonIndex(-1);
            CurrentButton = (Buttons)nextButton;
        }
        else if (Input.GetButtonDown(Define.Key.DOWN))
        {
            int nextButton = CurrentButtonIndex(1);
            CurrentButton = (Buttons)nextButton;
        }
    }

    int CurrentButtonIndex(int idx)
    {
        int nextButton = (int)CurrentButton + idx;
        if(nextButton < 0)
        {
            nextButton = 4;
        }
        else if(nextButton > 4)
        {
            nextButton = 0;
        }
        return nextButton;
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
            case Buttons.Skill:
                Manager.UI.MakeSubItem<SubItem_Skill>(_popup_Paused.transform);
                base.CloseSubItem();
                break;
            case Buttons.QuestionMark:
                Manager.UI.MakeSubItem<SubItem_QuestionMark>(_popup_Paused.transform);
                base.CloseSubItem();
                break;
            case Buttons.Resume:
                Manager.UI.MakeSubItem<SubItem_Resume>(_popup_Paused.transform);
                base.CloseSubItem();
                break;
            case Buttons.Setting:
                Manager.UI.MakeSubItem<SubItem_Setting>(_popup_Paused.transform);
                base.CloseSubItem();
                break;
            case Buttons.Quit:
                Manager.UI.MakeSubItem<SubItem_Quit>(_popup_Paused.transform);
                base.CloseSubItem();
                break;
        }
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
