using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Type = System.Type;

public class Popup_LevelUp : UI_Popup
{
    #region enum
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
        DescriptionText3,
        NewText0 = 8,
        NewText1,
        NewText2,
        NewText3,
        TypeText0 = 12,
        TypeText1, 
        TypeText2, 
        TypeText3
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
        ItemImage3,
        ItemFrameImage0 = 8,
        ItemFrameImage1,
        ItemFrameImage2,
        ItemFrameImage3,
        Button0,
        Button1,
        Button2,
        Button3
    }
    protected enum Objects
    {
        Pointer
    }
    #endregion

    private Buttons _currentButton;
    private Buttons CurrentButton
    {
        get
        {
            return _currentButton;
        }
        set
        {
            SetButtonNormal(_currentButton);
            SetButtonHighlighted(value);
            _currentButton = value;
        }
    }
    private const int LEVELUP_ITEM_MAX_COUNT = 4;
    private ItemID[] _itemList = new ItemID[4];
    private Inventory _inventory;
    private RectTransform _pointer;

    protected override void Init()
    {
        base.Init();
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));
        BindObject(typeof(Objects));
        _pointer = Utils.GetOrAddComponent<RectTransform>(GetObject((int)Objects.Pointer));
        for (int idx = 0; idx < LEVELUP_ITEM_MAX_COUNT; idx++)
        {
            Button button = GetButton(idx);
            button.BindEvent(OnEnterButton, Define.UIEvent.Enter, this);
            button.BindEvent(OnEnterButton, Define.UIEvent.Click, this);
        }
        
        Manager.UI.MakeSubItem<SubItem_Stats>(transform);
        
        _inventory = Manager.Game.Inventory;
        //SetupItem();
    }

    void SetupItem()
    {
        for (int idx = 0; idx < 4; idx++)
        {
            ItemID id = Manager.Spawn.GetRandomItem();
            ItemType type = Manager.Data.Item[id].Type;
            switch(type)
            {
                case ItemType.Weapon: SetupWeapon(id, idx); break;
                case ItemType.Equipment: SetupEquipment(id, idx); break;
                case ItemType.Stat: SetupStats(id, idx); break;
            }
            _itemList[idx] = id;
        }
    }

    void ShowItems(string name, string Exp, string typeImage, string icon, bool newText, string typeText, int idx)
    {
        GetText(idx + (int)Texts.NameText0).text = name;
        GetText(idx + (int)Texts.DescriptionText0).text = Exp;
        GetImage(idx + (int)Images.TypeImage0).sprite = Manager.Asset.LoadSprite(typeImage);
        GetImage(idx + (int)Images.ItemImage0).sprite = Manager.Asset.LoadSprite(icon);
        GetText(idx + (int)Texts.NewText0).text = newText ? "New!" : "";
        GetText(idx + (int)Texts.TypeText0).text = typeText;
    }
    void SetupWeapon(ItemID id, int idx)
    {
        List<Weapon> list = _inventory.Weapons;
        Weapon item = list.Find(weapon => weapon.ID == id);

        int nextLevel = item == null ? 0 : item.Level.Value + 1;

        WeaponData data = Manager.Data.Weapon[id][nextLevel];
        string name = data.Name;
        string Exp = data.Explanation;
        string type = data.WeaponType.ToString();
        string icon = data.Name;
        bool active = item != null;
        string typeText = Manager.Data.Item[id].Type.ToString();
        ShowItems(name, Exp, type, icon, active, typeText, idx);  
    }
    void SetupEquipment(ItemID id, int idx)
    {
        List<Equipment> list = _inventory.Equipments;
        Equipment item = list.Find(equipment => equipment.ID == id);

        int nextLevel = item == null ? 0 : item.Level.Value + 1;

        EquipmentData data = Manager.Data.Equipment[id][nextLevel];
        string name = data.Name;
        string Exp = data.Explanation;
        string type = null;
        string icon = data.Name;
        bool active = item != null;
        string typeText = Manager.Data.Item[id].Type.ToString();
        ShowItems(name, Exp, type, icon, active, typeText, idx);
    }
    void SetupStats(ItemID id, int idx)
    {
        StatsData data = Manager.Data.Stats[id];
        string name = data.Name;
        string Exp = data.Explanation;
        string type = null;
        string icon = data.Name;
        bool active = false;
        string typeText = "";
        ShowItems(name, Exp, type, icon, active, typeText, idx);
    }
    protected override void OnEnterButton(PointerEventData data)
    {
        Buttons buttonIdx = Enum.Parse<Buttons>(data.pointerEnter.name);
        CurrentButton = buttonIdx;
    }

    protected override void OnClickButton(PointerEventData data) 
    {
        Buttons buttonIdx = Enum.Parse<Buttons>(data.pointerClick.name);
        base.ClosePopup();
        _inventory.GetItem(_itemList[(int)buttonIdx]);
    }

    protected void SetButtonNormal(Buttons button)
    {
        GetImage((int)button).sprite = 
            Manager.Asset.LoadSprite("ui_menu_upgrade_window_0");

    }
    protected void SetButtonHighlighted(Buttons currentButton)
    {

        GetImage((int)currentButton).sprite =
            Manager.Asset.LoadSprite("ui_menu_upgrade_window_selected_0");
        float resize = -600;
        float pointerPosition = GetImage((int)currentButton).rectTransform.position.y;
            _pointer.anchoredPosition = new Vector2(-142, pointerPosition + resize);
    }
}
