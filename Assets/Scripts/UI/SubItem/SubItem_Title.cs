using System;
using UniRx;
using UnityEngine.EventSystems;
public class SubItem_Title : UI_SubItem
{
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

    void Start() => Init();

    protected override void Init()
    {
        base.Init();
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindEvent(GetButton((int)Define.UIEvent.Enter), OnEnterButton, Define.UIEvent.Enter, this);
        BindEvent(GetButton((int)Define.UIEvent.Click), OnClickButton, Define.UIEvent.Click, this);
    }

    protected void OnEnterButton(PointerEventData data)
    {
        Buttons button = Enum.Parse<Buttons>(data.pointerEnter.name);
        int buttonIdx = (int)button;
        GetText(buttonIdx).color = UnityEngine.Color.white; ;
    }

    protected void OnClickButton(PointerEventData data)
    {

    }
}
