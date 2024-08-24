using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Type = System.Type;
using System.Security.Cryptography;

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
        Button0,
        Button1,
        Button2,
        Button3,
        TypeImage0 = 4,
        TypeImage1,
        TypeImage2,
        TypeImage3,
        ItemImage0 = 8,
        ItemImage1,
        ItemImage2,
        ItemImage3,
        ItemFrameImage0 = 12,
        ItemFrameImage1,
        ItemFrameImage2,
        ItemFrameImage3,
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
        Manager.Sound.Play(Define.SoundType.Effect, "LevelUp");
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));
        BindObject(typeof(Objects));
        _pointer = Utils.GetOrAddComponent<RectTransform>(GetObject((int)Objects.Pointer));
        for (int idx = 0; idx < LEVELUP_ITEM_MAX_COUNT; idx++)
        {
            Button button = GetButton(idx);
            button.BindEvent(OnEnterButton, Define.UIEvent.Enter, this);
            button.BindEvent(OnClickButton, Define.UIEvent.Click, this);
        }
        
        Manager.UI.MakeSubItem<SubItem_Stats>(transform);
        Manager.Game.IsPlaying.Value = false;
        _inventory = Manager.Game.Inventory;
        Time.timeScale = 0f;
        SetupItem();
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
                case ItemType.StartingWeapon: SetupWeapon(id, idx); break;
                case ItemType.Equipment: SetupEquipment(id, idx); break;
                case ItemType.Stat: SetupStats(id, idx); break;

            }
            _itemList[idx] = id;
        }
    }

    void ShowItems(string name, string Exp, string typeImage, string icon, string type, bool newText, string typeText, int idx)
    {
        GetText(idx + (int)Texts.NameText0).text = name;
        GetText(idx + (int)Texts.DescriptionText0).text = Exp;
        GetImage(idx + (int)Images.TypeImage0).sprite = Manager.Asset.LoadSprite(typeImage);
        GetImage(idx + (int)Images.ItemImage0).sprite = Manager.Asset.LoadSprite(icon);
        GetImage(idx + (int)Images.ItemFrameImage0).sprite = Manager.Asset.LoadSprite($"spr_option{type}Icon_0");
        GetText(idx + (int)Texts.NewText0).text = newText ? "New!" : "";
        GetText(idx + (int)Texts.TypeText0).text = typeText;
    }
    void SetupWeapon(ItemID id, int idx)
    {
        List<Weapon> list = _inventory.Weapons;
        Weapon item = list.Find(weapon => weapon.ID == id);

        int nextLevel = item == null ? 1 : item.Level.Value + 1;

        WeaponData data = Manager.Data.Weapon[id][nextLevel];
        string name = data.Name;
        string Exp = data.Explanation;
        string type = data.WeaponType.ToString();
        string icon = Manager.Data.Item[id].IconImage;
        bool active = item == null;
        string typeText = data.WeaponType.ToString();

        ShowItems(name, Exp, type, icon, "weapon", active, typeText, idx);  
    }
    void SetupEquipment(ItemID id, int idx)
    {
        List<Equipment> list = _inventory.Equipments;
        Equipment item = list.Find(equipment => equipment.ID == id);

        int nextLevel = item == null ? 1 : item.Level.Value + 1;

        EquipmentData data = Manager.Data.Equipment[id][nextLevel];
        string name = data.Name;
        string Exp = data.Explanation;
        string type = null;
        string icon = data.Name;
        bool active = item == null;
        string typeText = Manager.Data.Item[id].Type.ToString();
        ShowItems(name, Exp, type, icon, "Equipment", active, typeText, idx);
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
        ShowItems(name, Exp, type, icon, "Stat", active, typeText, idx);
    }
    protected override void OnEnterButton(PointerEventData data)
    {
        Buttons buttonIdx = Enum.Parse<Buttons>(data.pointerEnter.name);
        CurrentButton = buttonIdx;
    }

    protected override void OnClickButton(PointerEventData data) 
    {
        Manager.Sound.Play(Define.SoundType.Effect, "ButtonClick");
        Buttons buttonIdx = Enum.Parse<Buttons>(data.pointerClick.name);
        _inventory.GetItem(_itemList[(int)buttonIdx]);
        Manager.Game.IsPlaying.Value = true;
        Time.timeScale = 1f;
        base.ClosePopup();
    }

    protected void SetButtonNormal(Buttons button)
    {
        GetImage((int)button).sprite = 
            Manager.Asset.LoadSprite("ui_menu_upgrade_window_0");

    }
    protected void SetButtonHighlighted(Buttons button)
    {
        Manager.Sound.Play(Define.SoundType.Effect, "ButtonMove");
        GetImage((int)button).sprite =
            Manager.Asset.LoadSprite("ui_menu_upgrade_window_selected_0");
        float resize = -600;
        float pointerPosition = GetImage((int)button).rectTransform.position.y;
            _pointer.anchoredPosition = new Vector2(-142, pointerPosition + resize);
    }
}
