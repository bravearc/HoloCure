using UniRx;
using UniRx.Triggers;

public abstract class UI_SubItem : UI_Base
{
    protected override void Init()
    {
        Manager.UI.SetCanvas(gameObject, false);
    }

    protected virtual void KeyCheck(){}


    protected virtual void CloseSubItem()
    {
        Manager.Asset.Destroy(gameObject);
    }
}
