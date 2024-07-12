using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System.Collections;
using System;
using System.Data.Common;
using UnityEngine.EventSystems;

public class Popup_ItemBox : UI_Popup
{
    #region enum
    protected enum Buttons
    {
        TakeButton,
        CancelButton
    }
    protected enum Texts
    {
        NameText,
        ExplanationText,
        TypeText
    }
    protected enum Images
    {
        GetItemImage0,
        GetItemImage1,
        TypeImage,
        ItemFrameImage
    }
    protected enum Objects
    {
        Position0,
        Position1,
        GetBox,
        OpenBox,
        CloseBox,
        New,
        Pointer
    }
    protected enum Anis
    {
        OpenBox
    }
    #endregion
    IDisposable _updateDisposable;
    Inventory _inventory;
    ItemID _itemID;
    void Start()
    {
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindObject(typeof(Objects));
        BindImage(typeof(Images));

        _updateDisposable = 
            this.UpdateAsObservable().Subscribe(_ => KeyCheck());
        
        Init();
    }

    protected override void Init()
    {
        base.Init();
    }

    protected void KeyCheck()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(OpenBox());
            _updateDisposable.Dispose();
        }
    }
    void SetupItem()
    {
        _inventory = Manager.Game.Inventory;
        _itemID = Manager.Spawn.GetRandomItem();
        if (Utils.IsWeapon(_itemID))
        {
            WeaponData data;
            Weapon weapon = _inventory.Weapons.Find(weapon => weapon.ID == _itemID);
            if (weapon != null) 
            {
                int level = weapon.Level.Value;
                data = Manager.Data.Weapon[_itemID][level + 1];
                GetObject((int)Objects.New).SetActive(false);
            }
            else
            {
                data = Manager.Data.Weapon[_itemID][0]; ;
            }
            GetText((int)Texts.NameText).text = data.Name;
            GetText((int)Texts.TypeText).text = ">> Weapon";
            GetImage((int)Images.GetItemImage0).sprite = Manager.Asset.LoadSprite(data.Name);
            GetImage((int)Images.GetItemImage1).sprite = Manager.Asset.LoadSprite(data.Name);
            GetImage((int)Images.TypeImage).sprite = Manager.Asset.LoadSprite(data.Type.ToString());
            GetImage((int)Images.ItemFrameImage).sprite = Manager.Asset.LoadSprite("Weapon_Frame");
        }
        else
        {
            EquipmentData data;
            Equipment equipment = _inventory.Equipments.Find(weapon => weapon.ID == _itemID);
            if (equipment != null)
            {
                int level = equipment.Level.Value;
                data = Manager.Data.Equipment[_itemID][level + 1];
                GetObject((int)Objects.New).SetActive(false);
            }
            else
            {
                data = Manager.Data.Equipment[_itemID][0]; ;
            }
            GetText((int)Texts.NameText).text = data.Name;
            GetText((int)Texts.TypeText).text = ">> Equipment";
            GetImage((int)Images.GetItemImage0).sprite = Manager.Asset.LoadSprite(data.Name);
            GetImage((int)Images.GetItemImage1).sprite = Manager.Asset.LoadSprite(data.Name);
            GetImage((int)Images.TypeImage).sprite = null;
            GetImage((int)Images.ItemFrameImage).sprite = Manager.Asset.LoadSprite("Equipment_Frame");
        }

    }

    IEnumerator OpenBox()
    {
        GetObject((int)Objects.OpenBox).SetActive(true);
        GetObject((int)Objects.GetBox).SetActive(false);

        while (GetAni((int)Anis.OpenBox).GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
        GetObject((int)Objects.CloseBox).SetActive(true);
        GetObject((int)Objects.OpenBox).SetActive(false);
        Manager.UI.MakeSubItem<SubItem_Stats>(transform);
        SetupItem();
    }

    protected override void OnEnterButton(PointerEventData data)
    {
        Buttons button = Enum.Parse<Buttons>(data.pointerEnter.name);
        int buttonIndex = (int)button;
        GetObject((int)Objects.Pointer).transform.position = GetObject(buttonIndex).transform.position;
    }

    protected override void OnClickButton(PointerEventData data)
    {
        Buttons button = Enum.Parse<Buttons>(data.pointerEnter.name);
        switch(button)
        {
            case Buttons.TakeButton:
                _inventory.GetItem(_itemID);
                break;
            case Buttons.CancelButton: break;
            default: break;
        }

        base.OnClickButton(data);
    }
    bool IsFever()
    {
        return UnityEngine.Random.Range(1, 101) < 30 ?  true : false;
    }
}
