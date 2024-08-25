using System;
using System.Collections;
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
        QuitText,
        GameOverText
    }

    enum Images
    {
        ReStartButton,
        QuitButton
    }
    enum Buttons
    {
        ReStartButton,
        QuitButton
    }
    enum Objects
    {
        GameOverText,
        ReStartButton,
        QuitButton
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
    Vector2 _endPos = new Vector2(0, 600);
    protected override void Init()
    {
        base.Init();

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindObject(typeof(Objects));
        BindImage(typeof(Images));

        for (int idx = 0; idx < Enum.GetValues(typeof(Buttons)).Length; idx++)
        {
            Button button = GetButton(idx);
            button.BindEvent(OnEnterButton, Define.UIEvent.Enter, this);
            button.BindEvent(OnClickButton, Define.UIEvent.Click, this);
        }

        GetObject((int)Objects.ReStartButton).SetActive(false);
        GetObject((int)Objects.QuitButton).SetActive(false);

        Time.timeScale = 0f;

        string gameover = Manager.Game.IsGameClear == true ? "GameClear" : "GameOver";
        GetText((int)Texts.GameOverText).text = gameover;
        Manager.Sound.Stop(Define.SoundType.BGM);
        Manager.Sound.Play(Define.SoundType.Effect, gameover);

        _disposable = this.UpdateAsObservable().Subscribe(_ => OnPressKey());

        StartCoroutine(MoveCo());
    }

    IEnumerator MoveCo()
    {
        RectTransform gameoverText = GetObject((int)Objects.GameOverText).GetComponent<RectTransform>();

        while(gameoverText.position.y > _endPos.y)
        {
            gameoverText.anchoredPosition -= new Vector2(0, 1);
            yield return new WaitForEndOfFrame();
        }

        GetObject((int)Objects.ReStartButton).SetActive(true);
        GetObject((int)Objects.QuitButton).SetActive(true);
    }

    void OnPressKey()
    {
        if (Input.GetButtonDown(Define.Key.CONFIRM))
        {
            ProcessButton(CurrentButton);
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
        GetText((int)button).color = Color.white;
        GetImage((int)button).sprite = Manager.Asset.LoadSprite("hud_Button_0");
    }
    void SetButtonHighlighted(Buttons button)
    {
        GetText((int)button).color = Color.black;
        GetImage((int)button).sprite = Manager.Asset.LoadSprite("hud_OptionButton_1");
    }
}
