using UnityEngine.EventSystems;
using System;
using UniRx.Triggers;
using UniRx;
using UnityEngine;

public class Popup_Paused : UI_Popup
{
    protected override void Init()
    {
        base.Init();
        Manager.UI.MakeSubItem<SubItem_Stats>(transform);
        Manager.UI.MakeSubItem<SubItem_Paused>(transform);
    }
}
