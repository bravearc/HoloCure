using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SubItem_ChooseStage : UI_SubItem
{
    #region enum
    protected enum Buttons
    {
        StageChangeButton,
        GoButton,
        StageButton
    }

    protected enum Texts
    {
        StageNameText,
        HoloCoinText
    }

    protected enum Images 
    {
        StageButton
    }
    protected enum Objects
    {
        Go
    }

    #endregion
    int _nextStage = 1;
    protected override void Init()
    {
        base.Init();

        BindButton(typeof(Buttons));
        BindImage(typeof(Images));
        BindText(typeof(Texts));
        BindObject(typeof(Objects));

        for (int idx = 0; idx < Enum.GetValues(typeof(Buttons)).Length; idx++) 
        { 
            Button button = GetButton(idx);
            button.BindEvent(OnClickButton, Define.UIEvent.Click, this);
        }

        GetObject((int)Objects.Go).gameObject.SetActive(false);
    }

    void OnClickButton(PointerEventData data)
    {
        Buttons button = Enum.Parse<Buttons>(data.pointerClick.name);

        switch (button)
        {
            case Buttons.StageChangeButton:
                StageReplacement(data.position.x);
                break;
            case Buttons.GoButton:
                //GameManager에 스테이지 세팅
                Manager.Game.GameStart();
                base.CloseSubItem();
                break;
            case Buttons.StageButton:
                GetObject((int)Objects.Go).gameObject.SetActive(true);
                break;
        }

        //ProcessButton(button);
    }
    void ProcessButton(Buttons button)
    {
        switch (button) 
        { 
            case Buttons.StageChangeButton:
                
                break;
            case Buttons.GoButton:
                Manager.Game.GameStart();
                break;
            case Buttons.StageButton:
                GetButton((int)Buttons.GoButton).gameObject.SetActive(true);
                break;
        }
    }
    void StageReplacement(float buttonPosition)
    {
        _nextStage += buttonPosition > 1000 ? 1 : -1;
        if (_nextStage < 1)
        {
            _nextStage = 3;
        }

        GetImage((int)Images.StageButton).sprite = Manager.Asset.LoadSprite($"spr_Stage{_nextStage}Port_0");
        GetText((int)Texts.StageNameText).text = Manager.Data.Stage[_nextStage].Name;
        GetText((int)Texts.HoloCoinText).text = _nextStage.ToString();
    }
}
