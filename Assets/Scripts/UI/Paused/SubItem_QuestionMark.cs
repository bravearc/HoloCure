using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class SubItem_QuestionMark : UI_SubItem
{
    void Start()
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
        Manager.Asset.Destroy(gameObject);
    }
}
