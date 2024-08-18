using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class UI_Popup : UI_Base
{
    protected override void Init()
    {
        base.Init();
        Manager.UI.SetCanvas(gameObject, true);
    }
    protected virtual void ClosePopup()
    {
        Manager.UI.ClosePopupUI();
    }
    protected virtual void OnEnterButton(PointerEventData data)
    {
        Manager.Sound.Play(Define.SoundType.Effect, "ButtonMove");
    }

    protected virtual void OnClickButton(PointerEventData data)
    {
        Time.timeScale = 1;
        Manager.Sound.SoundScale(Define.SoundType.BGM, 1);
        Manager.Asset.Destroy(gameObject);
    }


}
