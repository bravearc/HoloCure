using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Popup : UI_Base
{
    protected override void Init()
    {
        base.Init();
        Manager.UI.SetCanvas(gameObject, true);
        GameObject go = Manager.Asset.LoadObject("KeyDescription");
        Manager.Asset.Instantiate(go, transform);
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
