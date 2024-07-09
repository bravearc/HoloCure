using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class SubItem_Resume : UI_SubItem
{
    private void Start()
    {
        this.UpdateAsObservable().Subscribe(_ => KeyCheck()).Dispose();
    }
    protected override void KeyCheck()
    {
        if (Input.GetKeyDown("Cancel"))
        {
            OnCancel();
        }
    }

    protected override void OnCancel()
    {
        Manager.UI.ShowPopup<Popup_Paused>();
        base.OnCancel();
    }
}
