using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SubItem_ChooseStage : UI_SubItem
{
    #region enum
    protected enum Buttons
    {
        StageChangeButton,
        GoButton
    }

    protected enum Texts
    {
        StageNameText,
        HoloCoinText
    }

    protected enum Images 
    {
        StageImage
    }

    #endregion
    protected override void Init()
    {
        base.Init();

        BindButton(typeof(Buttons));
        BindImage(typeof(Images));
        BindText(typeof(Texts));

        for (int idx = 0; idx < Enum.GetValues(typeof(Buttons)).Length; idx++) 
        { 
            Button button = GetButton(idx);
            button.BindEvent(OnClickButton, Define.UIEvent.Click, this);
        }

        GetButton((int)Buttons.GoButton).gameObject.SetActive(false);
    }

    void OnClickButton(PointerEventData data)
    {
        Buttons button = Enum.Parse<Buttons>(data.pointerClick.name);
        ProcessButton(button);
    }

    void ProcessButton(Buttons button)
    {
        switch (button) 
        { 
            case Buttons.StageChangeButton:
                GetButton((int)Buttons.GoButton).gameObject.SetActive(true); 
                break;
            case Buttons.GoButton:
                Manager.Game.GameStart();
                    break;
        }
    }

}
