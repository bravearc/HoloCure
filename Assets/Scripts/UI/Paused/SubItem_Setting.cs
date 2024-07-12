using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class SubItem_Setting : UI_SubItem
{
    IDisposable _updateDisposable;
    void Start()
    {
        _updateDisposable =
            this.UpdateAsObservable().Subscribe(_ => KeyCheck());
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
        Manager.Asset.Destroy(gameObject);
    }
    private void OnDestroy()
    {
        _updateDisposable?.Dispose();
    }
}
