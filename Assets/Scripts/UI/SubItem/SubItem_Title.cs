using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class SubItem_Title : UI_SubItem
{
    #region enum
    protected enum Buttons
    {
        PlayButton,
        ShopButton,
        ReaderBoardButton,
        AchievementsButton,
        SettingsButton,
        CreditsButton,
        QuitButton
    }
    protected enum Texts
    {
        PlayText,
        ShopText,
        ReaderBoardText,
        AchievementsText,
        SettingsText,
        CreditsText,
        QuitText
    }
    protected enum Images
    {
        PlayButton,
        ShopButton,
        ReaderBoardButton,
        AchievementsButton,
        SettingsButton,
        CreditsButton,
        QuitButton
    }
    #endregion

    private Buttons _currentButton;
    private Buttons CurrentButton
    {
        get => _currentButton;
        set
        {
            SetButtonNormal(_currentButton);
            SetButtonHighlighted(value);
            _currentButton = value;
        }
    }
    protected override void Init()
    {
        base.Init();
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));

        CurrentButton = _currentButton;
        foreach (Buttons buttonIdx in Enum.GetValues(typeof(Buttons)))
        {
            Button button = GetButton((int)buttonIdx);
            button.BindEvent(OnEnterButton, Define.UIEvent.Enter, this);
            button.BindEvent(OnClickButton, Define.UIEvent.Click, this);
        }
    }

    protected override void OnPressKey()
    {
        if (Input.GetButtonDown(Define.Key.CONFIRM))
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

        if (nextButton < 0) 
        { 
            nextButton = 6;
        }
        else if (nextButton > 6) 
        { 
            nextButton = 0;
        }

        return nextButton;
    }

    protected void OnEnterButton(PointerEventData data)
    {
        Buttons nextButton = Enum.Parse<Buttons>(data.pointerEnter.name);
        CurrentButton = nextButton;
    }

    protected void OnClickButton(PointerEventData data)
    {
        Buttons button = Enum.Parse<Buttons>(data.pointerClick.name);
        Manager.Sound.Play(Define.SoundType.Effect, Define.Sound.ButtonClick);

        ProcessButton(button);
    }

    void ProcessButton(Buttons button)
    {
        switch (button)
        {
            case Buttons.PlayButton:
                Manager.UI.ClosePopupUI();
                Manager.UI.ShowPopup<Popup_Select>();
                break;
            case Buttons.ShopButton: break;
            case Buttons.ReaderBoardButton: break;
            case Buttons.SettingsButton: break;
            case Buttons.CreditsButton: break;
            case Buttons.QuitButton: break;
        }
    }

    protected void SetButtonNormal(Buttons button)
    {
        Manager.Sound.Play(Define.SoundType.Effect, Define.Sound.ButtonMove);
        GetText((int)button).color = Color.white;
        GetImage((int)button).sprite = Manager.Asset.LoadSprite("hud_OptionButton_0");
    }

    protected void SetButtonHighlighted(Buttons button)
    {
        GetText((int)button).color = Color.black;
        GetImage((int)button).sprite = Manager.Asset.LoadSprite("hud_OptionButton_1");
    }
}
