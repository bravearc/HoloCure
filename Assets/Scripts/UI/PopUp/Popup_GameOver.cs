using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Popup_GameOver : UI_Popup
{
    #region enum
    enum Texts
    {
        ReStartText,
        QuitText
    }

    enum Buttons
    {
        ReStartButton,
        QuitButton
    }
    enum Objects
    {
        GameOverText
    }
    #endregion
    private Buttons _currentButton;
    private Buttons CurrentButton
    {
        get
        {
            return _currentButton;
        }
        set
        {
            SetButtonNormal(_currentButton);
            SetButtonHighlighted(value);
            _currentButton = value;
        }
    }

    IDisposable _disposable;
    protected override void Init()
    {
        base.Init();

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindObject(typeof(Objects));

        for (int idx = 0; idx < Enum.GetValues(typeof(Buttons)).Length; idx++)
        {
            Button button = GetButton(idx);
            button.BindEvent(OnEnterButton, Define.UIEvent.Enter, this);
            button.BindEvent(OnClickButton, Define.UIEvent.Click, this);
        }
        Time.timeScale = 0f;
        _disposable = this.UpdateAsObservable().Subscribe(_ => OnPressKey());
        
    }

    void OnPressKey()
    {
        if (Input.GetButtonDown(Define.Key.CONFIRM))
        {
            ProcessButton(CurrentButton);
        }
        else if (Input.GetButtonDown(Define.Key.CANCEL))
        {

        }

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
    }

    void ProcessButton(Buttons button)
    {
        switch(button)
        {
            case Buttons.ReStartButton:
                Manager.Game.GameReStart();
                break;
            case Buttons.QuitButton:
                Manager.Game.GameOver();
                break;
        }
        Time.timeScale = 1f;
        base.ClosePopup();
        _disposable?.Dispose();
        _disposable = null;
    }

    protected override void OnEnterButton(PointerEventData data)
    {
        Buttons buttonIdx = Enum.Parse<Buttons>(data.pointerEnter.name);
        CurrentButton = buttonIdx;
    }

    protected override void OnClickButton(PointerEventData data)
    {
        Buttons buttonIdx = Enum.Parse<Buttons>(data.pointerClick.name);
        ProcessButton(buttonIdx);
    }
    void SetButtonNormal(Buttons button)
    {
        GetImage((int)button).sprite =
            Manager.Asset.LoadSprite("ui_menu_upgrade_window_0");
    }
    void SetButtonHighlighted(Buttons button)
    {
        GetImage((int)button).sprite =
            Manager.Asset.LoadSprite("ui_menu_upgrade_window_selected_0");
    }
}
