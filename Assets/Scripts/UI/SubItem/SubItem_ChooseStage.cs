using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniRx.Triggers;
using UniRx;
using UnityEngine;

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

    Popup_Select _select;

    int _nextStage = 1;
    bool _isGoButtonSet;
    protected override void Init()
    {
        base.Init();

        _select = transform.parent.GetComponent<Popup_Select>();

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

        this.UpdateAsObservable().Subscribe(_ => OnPressKey());
    }
    private void OnPressKey()
    {
        if (Input.GetButtonDown(Define.KeyCode.CONFIRM))
        {

        }
        else if (Input.GetButtonDown(Define.KeyCode.CANCEL))
        {
            ProcessCancel();
        }
    }
    private void ProcessCancel()
    {
        if (_isGoButtonSet)
        {
            GetObject((int)Objects.Go).SetActive(false);
            _isGoButtonSet = false;
        }
        else
        {
            base.CloseSubItem();
            Manager.UI.MakeSubItem<SubItem_ChooseMode>(_select.transform);
        }
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
                _isGoButtonSet = true;
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
        else if (_nextStage > 3) 
        { 
            _nextStage = 1;
        }

        GetImage((int)Images.StageButton).sprite = Manager.Asset.LoadSprite($"spr_Stage{_nextStage}Port_0");
        GetText((int)Texts.StageNameText).text = Manager.Data.Stage[_nextStage - 1].Name;
        GetText((int)Texts.HoloCoinText).text = $"x {_nextStage.ToString()}";
    }
}
