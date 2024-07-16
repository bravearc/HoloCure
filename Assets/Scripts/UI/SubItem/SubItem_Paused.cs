using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

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

    #endregion
    protected override void Init()
    {
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        for (int i = 0; i < 5; i++) 
        {
            BindEvent(GetButton(i), OnClickButton, Define.UIEvent.Click, this);
            BindEvent(GetButton(i), OnEnterButton, Define.UIEvent.Enter, this);
        }
    }

    protected void OnClickButton(PointerEventData data)
    {
        Buttons button = Enum.Parse<Buttons>(data.pointerClick.name);
        switch (button) 
        { 
            case Buttons.Skill:
                Manager.UI.MakeSubItem<SubItem_Skill>(gameObject.transform);
                CloseSubItem();
                break;
            case Buttons.QuestionMark:
                Manager.UI.MakeSubItem<SubItem_Setting>(gameObject.transform);
                break;
            case Buttons.Resume:
                Manager.UI.MakeSubItem<SubItem_Resume>(gameObject.transform);
                break;
            case Buttons.Setting:
                Manager.UI.MakeSubItem<SubItem_Setting>(gameObject.transform);
                break;
            case Buttons.Quit:
                Manager.Game.GameOver();
                break;
        }
    }

    protected void OnEnterButton(PointerEventData data)
    {
        Buttons button = Enum.Parse<Buttons>(data.pointerClick.name);
        GetText((int)button).color = UnityEngine.Color.black;
    }
}
