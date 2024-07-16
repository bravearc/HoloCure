using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class SubItem_QuestionMark : UI_SubItem
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
            CloseSubItem();
        }
    }

    protected override void CloseSubItem()
    {
        base.CloseSubItem();
    }
    private void OnDestroy()
    {
        _updateDisposable?.Dispose();
    }
}
