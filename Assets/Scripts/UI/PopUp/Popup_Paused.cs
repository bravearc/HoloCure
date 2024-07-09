using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Popup_Paused : UI_Popup
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
    protected enum Objects 
    { 
        Comingsoon
    }
    #endregion
    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindObject(typeof(Objects));
        for (int i = 0; i < 5; i++) 
        {
            BindEvent(GetButton(i), OnClickButton, Define.UIEvent.Click, this);
            BindEvent(GetButton(i), OnEnterButton, Define.UIEvent.Enter, this);
        }
    }

    protected override void OnClickButton(PointerEventData data)
    {
        Buttons button = Enum.Parse<Buttons>(data.pointerClick.name);
        switch (button) 
        { 
            case Buttons.Skill:
                Manager.UI.MakeSubItem<SubItem_Skill>(gameObject.transform);
                break;
            case Buttons.QuestionMark:
                GetObject((int)Objects.Comingsoon).SetActive(true);
                break;
            case Buttons.Resume:
                GetObject((int)Objects.Comingsoon).SetActive(true);
                break;
            case Buttons.Setting:
                GetObject((int)Objects.Comingsoon).SetActive(true);
                break;
            case Buttons.Quit:
                //
                break;
        }
    }

    protected override void OnEnterButton(PointerEventData data)
    {
        base.OnEnterButton(data);
        Buttons button = Enum.Parse<Buttons>(data.pointerClick.name);
        GetText((int)button).color = UnityEngine.Color.black;
    }
}
