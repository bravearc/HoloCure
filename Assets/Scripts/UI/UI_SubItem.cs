using UniRx;
using UniRx.Triggers;
using UnityEngine;

public abstract class UI_SubItem : UI_Base
{
    protected override void Init()
    {
        Manager.UI.SetCanvas(gameObject, false);
        this.UpdateAsObservable().Subscribe(_ => OnPressKey());
    }

    protected virtual void OnPressKey()
    {

    }


    protected virtual void CloseSubItem()
    {
        Manager.Asset.Destroy(gameObject);
    }
}
