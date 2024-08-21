using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Popup_PlayUI : UI_Popup
{
    GameObject _stage1;
    Popup_Paused _paused;
    bool _isPlaying;
    protected override void Init()
    {
        base.Init();
        _stage1 = Manager.Asset.InstantiateObject(nameof(Map_Stage1)).gameObject;
        Manager.Sound.Play(Define.SoundType.BGM, "StageOneBGM", 1);
        Manager.UI.MakeSubItem<SubItem_PlayUI>(transform);
        Manager.UI.MakeSubItem<SubItem_Inventory>(transform);
        BindModelEvent(Manager.Game.IsPlaying, PlayChack, this);
        this.UpdateAsObservable().Subscribe(_ => OnPressKey());
        
    }
    void PlayChack(bool b)
    {
        _isPlaying = b;
    }

    void OnPressKey()
    {
        if (_isPlaying)
        {
            if (Input.GetButtonDown(Define.Key.CANCEL) && _paused == null)
            {
                _paused = Manager.UI.ShowPopup<Popup_Paused>();
                Time.timeScale = 0f;
            }
        }
    }

    private void OnDisable()
    {
        Manager.Asset.Destroy(_stage1);
    }
}
