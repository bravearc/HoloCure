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
        //Manager.UI.SetCanvas(gameObject, false);
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));

        CurrentButton = _currentButton;
        Debug.Log(_currentButton.ToString());
        foreach (Buttons buttonIdx in Enum.GetValues(typeof(Buttons)))
        {
            Button button = GetButton((int)buttonIdx);
            button.BindEvent(OnEnterButton, Define.UIEvent.Enter, this);
            button.BindEvent(OnClickButton, Define.UIEvent.Click, this);
        }
    }

    protected void OnEnterButton(PointerEventData data)
    {
        Debug.Log(Enum.Parse<Buttons>(data.pointerEnter.name));
        Buttons nextButton = Enum.Parse<Buttons>(data.pointerEnter.name);
        CurrentButton = nextButton;
    }

    protected void OnClickButton(PointerEventData data)
    {
        UnityEngine.Debug.Log("클릭");
    }

    protected void SetButtonNormal(Buttons button)
    {
        Debug.Log("노멀" + button.ToString());
        GetText((int)button).color = Color.white;
        GetImage((int)button).sprite = Manager.Asset.LoadSprite("hud_OptionButton_0");
    }

    protected void SetButtonHighlighted(Buttons button)
    {
        Debug.Log("하이라이트" + button.ToString());
        GetText((int)button).color = Color.black;
        GetImage((int)button).sprite = Manager.Asset.LoadSprite("hud_OptionButton_1");
    }
}
