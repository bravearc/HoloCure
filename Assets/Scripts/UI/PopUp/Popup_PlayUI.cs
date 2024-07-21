using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Popup_PlayUI : UI_Popup
{
    GameObject _stage1;
    Popup_Paused _paused;
    protected override void Init()
    {
        base.Init();
        _stage1 = Manager.Asset.LoadObject(nameof(Map_Stage1)).gameObject;

        Manager.UI.MakeSubItem<SubItem_PlayUI>(transform);
        Manager.UI.MakeSubItem<SubItem_Inventory>(transform);
        this.UpdateAsObservable().Subscribe(_ => OnPressKey());
    }

    void OnPressKey()
    {

        if (Input.GetButtonDown(Define.KeyCode.CANCEL) && _paused == null)
        {
            _paused = Manager.UI.ShowPopup<Popup_Paused>();
            Time.timeScale = 0f;
        }
    }

    private void OnDisable()
    {
        Manager.Asset.Destroy(_stage1);
    }
}
