using System;
using System.Collections.Generic;
using UniRx;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;
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
    private ItemID[] _itemList = new ItemID[4];
    private GameObject[] _position = new GameObject[4];
    private Inventory _inventory;
    void Start() => Init();

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
        _inventory = Manager.Game.Inventory;
        for (int idx = 0; idx < 4; idx++)
        {
            _position[idx] = GetObject(idx);
            BindEvent(GetButton(idx), OnEnterButton, Define.UIEvent.Enter, this);
            BindEvent(GetButton(idx), OnClickButton, Define.UIEvent.Click, this);
        }
        SetupItem();
    }

    void SetupItem()
    {
        for (int idx = 0; idx < 4; idx++)
        {
            ItemID id = Manager.Spawn.GetRandomItem();
            string type = Utils.GetItemType(id);

            switch(type)
            {
                case "Weapon": SetupWeapon(id, idx); break;
                case "Equipment": SetupEquipment(id, idx); break;
                case "Stats": SetupStats(id, idx); break;
            }
            _itemList[idx] = id;
        }
    }

    void ShowItems(string name, string Exp, string type, string icon, bool active, int idx)
    {
        GetText(idx + (int)Texts.NameText0).text = name;
        GetText(idx + (int)Texts.DescriptionText0).text = Exp;
        GetImage(idx + (int)Images.TypeImage0).sprite = Manager.Asset.LoadSprite(type);
        GetImage(idx + (int)Images.ItemImage0).sprite = Manager.Asset.LoadSprite(icon);
        GetObject(idx + (int)Objects.NewText0).SetActive(active);
    }

    void SetupWeapon(ItemID id, int idx)
    {
        List<Weapon> list = _inventory.Weapons;
        Weapon item = list.Find(weapon => weapon.ID == id);

        int nextLevel = item == null ? 0 : item.Level.Value + 1;

        WeaponData data = Manager.Data.Weapon[id][nextLevel];
        string name = data.Name;
        string Exp = data.Explanation;
        string type = data.Type.ToString();
        string icon = data.Name;
        bool active = item != null;
        ShowItems(name, Exp, type, icon, active, idx);  
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
        ShowItems(name, Exp, type, icon, active, idx);
    }
    void SetupStats(ItemID id, int idx)
    {
        StatsData data = Manager.Data.Stats[id];
        string name = data.Name;
        string Exp = data.Explanation;
        string type = null;
        string icon = data.Name;
        bool active = false;
        ShowItems(name, Exp, type, icon, active, idx);
    }
    protected override void OnEnterButton(PointerEventData data)
    {
        Buttons button = Enum.Parse<Buttons>(data.pointerEnter.name);
        int buttonIndex = (int)button;
        GetObject((int)Objects.Pointer).transform.position = _position[buttonIndex].transform.position;
        
        base.OnEnterButton(data);
    }

    protected override void OnClickButton(PointerEventData data) 
    {
        Buttons button = Enum.Parse<Buttons>(data.pointerClick.name);
        int buttonIndex = (int)button;
        _inventory.GetItem(_itemList[buttonIndex]);
        base.OnClickButton(data);
    }
}
