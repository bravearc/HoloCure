using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class Popup_ItemBox : UI_Popup
{
    protected enum Buttons
    {
        Open,

    }
    protected enum Texts
    {

    }
    protected enum Images
    {
        Icon,
        Item
    }
    protected enum Objects
    {
        UnBoxing,
        New
    }

    void Start()
    {
        this.UpdateAsObservable().Subscribe(_ => KeyCheck()).Dispose();
        Init();
    }

    protected override void Init()
    {
        base.Init();

    }

    protected void KeyCheck()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnClick();
        }
    }

    protected void OnClick()
    {
        //오픈 버튼 비활성화
        //
    }

}
