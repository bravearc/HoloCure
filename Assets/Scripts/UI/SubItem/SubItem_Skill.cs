using UnityEngine;


public class SubItem_Skill : UI_SubItem
{
    Popup_Paused _popup_Paused;

    protected override void Init()
    {
        base.Init();
        _popup_Paused = transform.parent.GetComponent<Popup_Paused>();
    }
    protected override void OnPressKey()
    {
        if (Input.GetButtonDown(Define.KeyCode.CANCEL))
        {
            Manager.UI.MakeSubItem<SubItem_Paused>(_popup_Paused.transform);
            base.CloseSubItem();
        }
    }
}
