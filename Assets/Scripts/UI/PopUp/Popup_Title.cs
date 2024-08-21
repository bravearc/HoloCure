using UnityEngine;

public class Popup_Title : UI_Popup
{
    protected override void Init()
    {
        base.Init();
        Manager.Sound.Play(Define.SoundType.BGM, "TitleBGM", 1);
        Manager.UI.MakeSubItem<SubItem_Title>(transform);
    }

    void OnDisable()
    {
        base.ClosePopup();
    }
}
