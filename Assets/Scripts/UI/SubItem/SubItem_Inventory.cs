using System;
using System.Collections.Generic;
public class SubItem_Inventory : UI_SubItem
{
    #region enum
    protected enum Images
    {
        WeaponImage0,
        WeaponImage1,
        WeaponImage2,
        WeaponImage3,
        WeaponImage4,
        WeaponImage5,

        EquipmentImage0,
        EquipmentImage1,
        EquipmentImage2,
        EquipmentImage3,
        EquipmentImage4,
        EquipmentImage5,

    }

    protected enum Texts
    {
        WeaponLevelText0,
        WeaponLevelText1,
        WeaponLevelText2,
        WeaponLevelText3,
        WeaponLevelText4,
        WeaponLevelText5,

        EquipmentLevelText0,
        EquipmentLevelText1,
        EquipmentLevelText2,
        EquipmentLevelText3,
        EquipmentLevelText4,
        EquipmentLevelText5
    }
    #endregion

    protected override void Init()
    {
        base.Init();

        BindImage(typeof(Images));
        BindText(typeof(Texts));
        BindModelEvent(Manager.Game.Inventory.WeaponCount, SetWeapon, this);
        BindModelEvent(Manager.Game.Inventory.EquipmentCount, SetEquipment, this);
    }

    private void SetWeapon(int slot)
    {
        if (slot < 0) { return; }

        Weapon weapon = Manager.Game.Inventory.Weapons[slot];
        UpdateUI_Image(slot, weapon.WeaponData.Name, true);
        weapon.Level.BindModelEvent(level => UpdateUI_Text(level, slot, true), this);
    }

    private void SetEquipment(int slot)
    {
        if (slot < 0) { return; }

        Equipment equipment = Manager.Game.Inventory.Equipments[slot];
        UpdateUI_Image(slot, equipment.EquipmentData.Name, false);
        equipment.Level.BindModelEvent(level => UpdateUI_Text(level, slot, false), this);
    }
    private void UpdateUI_Image(int slot, string imageName, bool isWeapon)
    {
        int baseIndex = isWeapon ? (int)Images.WeaponImage0 : (int)Images.EquipmentImage0;
        GetImage(slot + baseIndex).sprite = Manager.Asset.LoadSprite(imageName);
    }

    private void UpdateUI_Text(int level, int slot, bool isWeapon)
    {
        int baseIndex = isWeapon ? (int)Texts.WeaponLevelText0 : (int)Texts.EquipmentLevelText0;
        GetText(slot + baseIndex).text = $"Lv_{level}";
    }
}