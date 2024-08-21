using UnityEngine;

public class SubItem_Setting : UI_SubItem
{
    Popup_Paused _popup_Paused;

    protected override void Init()
    {
        base.Init();
        _popup_Paused = transform.parent.GetComponent<Popup_Paused>();
    }
    protected override void OnPressKey()
    {
        if (Input.GetButtonDown(Define.Key.CANCEL))
        {
            Manager.UI.MakeSubItem<SubItem_Paused>(_popup_Paused.transform);
            base.CloseSubItem();
        }
    }
}
