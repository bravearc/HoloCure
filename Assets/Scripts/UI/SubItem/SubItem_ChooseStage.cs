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


    bool _isGoButtonSet;

    int _nextStage = 1;
    int NextStage
    {
        get
        {
            return _nextStage;
        }
        set
        {
            _nextStage = CurrentStage(value);
            StageReplacement(_nextStage);
        }
    }
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

    }
    protected override void OnPressKey()
    {
        if (Input.GetButtonDown(Define.Key.CONFIRM))
        {
            if (_isGoButtonSet) 
            {
                ProcessButton(Buttons.GoButton, 0);
            }
            else
            {
                ProcessButton(Buttons.StageButton, 0);
            }
        }
        else if (Input.GetButtonDown(Define.Key.CANCEL))
        {
            ProcessCancel();
        }

        if (_isGoButtonSet == false)
        {
            if (Input.GetButtonDown(Define.Key.LEFT))
            {
                NextStage = -1;
            }
            else if (Input.GetButtonDown(Define.Key.RIGHT))
            {
                NextStage = 1;
            }
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
        Manager.Sound.Play(Define.SoundType.Effect, Define.Sound.ButtonClick);
        Buttons button = Enum.Parse<Buttons>(data.pointerClick.name);

        ProcessButton(button, data.position.x);
    }

    void ProcessButton(Buttons button, float pos)
    {
        switch (button)
        {
            case Buttons.StageChangeButton:
                int idx = pos > 1000 ? 1 : -1;
                StageReplacement(CurrentStage(idx));
                break;
            case Buttons.GoButton:
                Manager.Game.GameStart();
                base.CloseSubItem();
                Manager.UI.CloseALLPopupUI();
                Manager.UI.ShowPopup<Popup_PlayUI>();
                break;
            case Buttons.StageButton:
                _isGoButtonSet = true;
                GetObject((int)Objects.Go).gameObject.SetActive(true);
                break;
        }
    }
    int CurrentStage(int idx)
    {
        int nextStage = idx + _nextStage;

        if (nextStage < 1)
        {
            nextStage = 3;
        }
        else if (nextStage > 3)
        {
            nextStage = 1;
        }
        return nextStage;
    }
    void StageReplacement(int nextStage)
    {
        this._nextStage = nextStage;
        GetImage((int)Images.StageButton).sprite = Manager.Asset.LoadSprite($"spr_Stage{_nextStage}Port_0");
        GetText((int)Texts.StageNameText).text = Manager.Data.Stage[_nextStage - 1].Name;
        GetText((int)Texts.HoloCoinText).text = $"x {_nextStage.ToString()}";
    }
}
