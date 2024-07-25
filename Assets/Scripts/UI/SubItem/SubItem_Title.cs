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

    protected void OnEnterButton(PointerEventData data)
    {
        Buttons nextButton = Enum.Parse<Buttons>(data.pointerEnter.name);
        CurrentButton = nextButton;
    }

    protected void OnClickButton(PointerEventData data)
    {
        Buttons button = Enum.Parse<Buttons>(data.pointerClick.name);
        switch(button) 
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
        GetText((int)button).color = Color.white;
        GetImage((int)button).sprite = Manager.Asset.LoadSprite("hud_OptionButton_0");
    }

    protected void SetButtonHighlighted(Buttons button)
    {
        GetText((int)button).color = Color.black;
        GetImage((int)button).sprite = Manager.Asset.LoadSprite("hud_OptionButton_1");
    }
}
