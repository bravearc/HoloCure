using System;
using System.Collections.Generic;
using System.Reflection;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Type = System.Type;

public class Popup_LevelUp : UI_Popup
{
    #region enum & struct
    protected enum Buttons
    {
        Button0,
        Button1,
        Button2,
        Button3
    }
    protected enum Texts
    {
        NameText0,
        NameText1,
        NameText2,
        NameText3,
        DescriptionText0 = 4,
        DescriptionText1,
        DescriptionText2,
        DescriptionText3
    }
    protected enum Images
    {
        TypeImage0,
        TypeImage1,
        TypeImage2,
        TypeImage3,
        ItemImage0 = 4,
        ItemImage1,
        ItemImage2,
        ItemImage3
    }
    protected enum Objects
    {
        Position0, 
        Position1, 
        Position2, 
        Position3,
        NewText0,
        NewText1,
        NewText2,
        NewText3,
        Pointer
    }
    #endregion
    private const int LEVELUP_ITEM_MAX_COUNT = 4;
    private IItem[] _itemList = new IItem[4];
    private GameObject[] _position = new GameObject[4];
    void Start() => Init();

    //Dictionary<int, Item<GameObject>> _itemdic = new();
    protected override void Init()
    {
        base.Init();
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));
        BindObject(typeof(Objects));
        BindEvent(GetButton((int)Define.UIEvent.Enter), OnEnterButton, Define.UIEvent.Enter, this);
        BindEvent(GetButton((int)Define.UIEvent.Click), OnEnterButton, Define.UIEvent.Click, this);
        Manager.UI.MakeSubItem<SubItem_Stats>(transform);

        for (int i = 0; i < 4; i++)
        {
            _position[i] = GetObject(i);
            BindEvent(GetButton(i), OnEnterButton, Define.UIEvent.Enter, this);
            BindEvent(GetButton(i), OnClickButton, Define.UIEvent.Click, this);
        }
        SetItemList();
    }

    void SetItemList()
    {
        int idx = 0;
        while (idx < 4)
        {
            IItem item;
            Type type = (Utils.Shuffle<int>(1, 3) == 1) ? typeof(WeaponData) : typeof(EquipmentData);
            if(type == typeof(WeaponData))
                item = Manager.Spawn.RandomItem<WeaponData>();
            else
                item = Manager.Spawn.RandomItem<EquipmentData>();

            SetComponent<WeaponData>(item, idx);

            ++idx;
        }
    }

    void SetComponent<T>(IItem newItem, int idx) where T : IItem
    {
        T item = Manager.Game.Inventory.FindExiItem<T>(newItem);
        bool isitem = true;
        if (item == null)
        {
            item = (T)newItem;
        }
        else
        {
            item = Manager.Game.Inventory.ItemNextLevel<T>(newItem);
            isitem = false;
        }

        _itemList[idx] = item;

        int nextComponent = 4;
        GetText(idx).text = item.Name;
        GetText(idx + nextComponent).text = item.Explanation;
        GetImage(idx).sprite = Manager.Asset.LoadSprite(item.Type.ToString());
        GetImage(idx + nextComponent).sprite = Manager.Asset.LoadSprite(item.Name);
        GetObject(idx + nextComponent).SetActive(isitem);
    }

    protected override void OnEnterButton(PointerEventData data)
    {
        Buttons button = Enum.Parse<Buttons>(data.pointerClick.name);
        int buttonIndex = (int)button;
        GetObject((int)Objects.Pointer).transform.position = _position[buttonIndex].transform.position;
        
        base.OnEnterButton(data);
    }

    protected override void OnClickButton(PointerEventData data) 
    {
        Buttons button = Enum.Parse<Buttons>(data.pointerClick.name);
        Manager.Game.Inventory.SetItem<IItem>(_itemList[(int)button]);
        base.OnClickButton(data);
    }
}
