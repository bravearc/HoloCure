using UnityEngine;

public class Popup_Title : UI_Popup
{
    protected override void Init()
    {
        Manager.UI.MakeSubItem<SubItem_Title>();
    }

    void OnDisable()
    {
        base.ClosePopup();
    }
}
