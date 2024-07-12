using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Popup : UI_Base
{
    protected override void Init()
    {
        base.Init();
        Manager.UI.SetCanvas(gameObject);
        Time.timeScale = 0;
        Manager.Sound.SoundScale(Define.Sound.BGM, 0.5f);
    }
    public virtual void ClosePopup()
    {
        Manager.UI.ClosePopupUI();
    }
    protected virtual void OnEnterButton(PointerEventData data)
    {
        Manager.Sound.Play(Define.Sound.Effect, "ButtonMove");
    }

    protected virtual void OnClickButton(PointerEventData data)
    {
        Time.timeScale = 1;
        Manager.Sound.SoundScale(Define.Sound.BGM, 1);
        Manager.Asset.Destroy(gameObject);
    }
}
