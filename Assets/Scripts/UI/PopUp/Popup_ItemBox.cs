using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Collections;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        TakeText,
        CancelText,
        NameText,
        ExplanationText,
        TypeText,
        NewText
    }
    protected enum Images
    {
        TakeButton,
        CancelButton,
        GetItemImage0,
        GetItemImage1,
        TypeImage,
        ItemFrameImage
    }
    protected enum Objects
    {
        GetBox,
        OpenBox,
        CloseBox,
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
    public RectTransform _pointer;

    Buttons _currentButon;
    Buttons CurrentButton 
    { 
        get
        {
            return _currentButon;
        } 
        set
        {
            SetButtonNormal(_currentButon);
            SetButtonsHighlight(value);
            _currentButon = value;
        }
    }
    protected override void Init()
    {
        base.Init();
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindObject(typeof(Objects));
        BindImage(typeof(Images));
        BindAnimator(typeof(Anis));

        _pointer = Utils.GetOrAddComponent<RectTransform>(GetObject((int)Objects.Pointer));

        GetObject((int)Objects.OpenBox).SetActive(false);
        GetObject((int)Objects.CloseBox).SetActive(false);

        _updateDisposable =
            this.UpdateAsObservable().Subscribe(_ => KeyCheck());

        CurrentButton = _currentButon;

        for (int idx = 0; idx < Enum.GetValues(typeof(Buttons)).Length; idx++) 
        { 
            Button button = GetButton(idx);
            button.BindEvent(OnEnterButton, Define.UIEvent.Enter, this);
            button.BindEvent(OnClickButton, Define.UIEvent.Click, this);
        }

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
            string newtext = "";
            Weapon weapon = _inventory.Weapons.Find(weapon => weapon.ID == _itemID);
            if (weapon != null) 
            {
                int level = weapon.Level.Value;
                data = Manager.Data.Weapon[_itemID][level + 1];
                newtext = "New!";
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
            GetText((int)Texts.NewText).text = newtext;
        }
        else
        {
            EquipmentData data;
            string newtext = "";
            Equipment equipment = _inventory.Equipments.Find(weapon => weapon.ID == _itemID);
            if (equipment != null)
            {
                int level = equipment.Level.Value;
                data = Manager.Data.Equipment[_itemID][level + 1];
                newtext = "New!";
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
            GetText((int)Texts.NewText).text = newtext;
        }

    }

    IEnumerator OpenBox()
    {
        GetObject((int)Objects.OpenBox).SetActive(true);
        GetObject((int)Objects.GetBox).SetActive(false);

        while (GetAnimator((int)Anis.OpenBox).GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
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
        CurrentButton = button;
        //버튼사운드
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

    void SetButtonNormal(Buttons button)
    {
        GetImage((int)button).sprite = Manager.Asset.LoadSprite("hud_Button_0");
        GetText((int)button).color = Color.white;
    }

    void SetButtonsHighlight(Buttons button) 
    {
        GetImage((int)button).sprite = Manager.Asset.LoadSprite("hud_Button_1");
        GetText((int)button).color = Color.black;
        float resize = -586;
        float pointerY = GetImage((int)button).rectTransform.position.y + resize;
        _pointer.anchoredPosition = new Vector2(455, pointerY);
    }

    bool IsFever()
    {
        return UnityEngine.Random.Range(1, 101) < 30 ?  true : false;
    }
}
