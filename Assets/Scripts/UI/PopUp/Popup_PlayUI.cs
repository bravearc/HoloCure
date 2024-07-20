using UnityEngine;

public class Popup_PlayUI : UI_Popup
{
    GameObject stage1;
    protected override void Init()
    {
        base.Init();
        stage1 = Manager.Asset.LoadObject(nameof(Map_Stage1)).gameObject;

        Manager.UI.MakeSubItem<SubItem_PlayUI>(transform);
        Manager.UI.MakeSubItem<SubItem_Inventory>(transform);

    }

    private void OnDisable()
    {
        Manager.Asset.Destroy(stage1);
    }
}
