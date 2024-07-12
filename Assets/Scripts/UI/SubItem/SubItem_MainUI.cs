using System;

public class SubItem_MainUI : UI_SubItem
{
    #region enum
    protected enum Images
    {
        Weapon0 = 0,
        Weapon1,
        Weapon2,
        Weapon3,
        Weapon4,
        Weapon5,
        WeaponLevel0 = 6, 
        WeaponLevel1,
        WeaponLevel2,
        WeaponLevel3,
        WeaponLevel4,
        WeaponLevel5,
        Equipment0 = 12,
        Equipment1,
        Equipment2,
        Equipment3,
        Equipment4,
        Equipment5,
        EquipmentLevel0 = 18,
        EquipmentLevel1,
        EquipmentLevel2,
        EquipmentLevel3,
        EquipmentLevel4,
        EquipmentLevel5,
        CharacterImage,
        SpecialImage,
        ExperiencePoints
    }
    protected enum Texts
    {
        Timer,
        GoldText,
        EnemyCountText,
        HpText
    }
    protected enum Sliders
    {
        HpSlider,
        SpecialSlider
    }
    #endregion

    private Images[] _weapons = new Images[6];
    private Images[] _weaponLevels = new Images[6];
    private Images[] _equipments = new Images[6];
    private Images[] _equipmentLevels = new Images[6];
    private Inventory _inventory;
    private Character _character;
    private void Start()
    {
        Init();
    }
    protected override void Init()
    {
        base.Init();
        BindSlider(typeof(Sliders));
        BindText(typeof(Texts));
        BindImage(typeof(Images));
        SetArray(_weapons, 0);
        SetArray(_weaponLevels, 6);
        SetArray(_equipments, 12);
        SetArray(_equipmentLevels, 18);

        GetImage((int)Images.CharacterImage).sprite = 
            Manager.Asset.LoadSprite(Manager.Game.GetCharacterData().Sprite_Icon);
        GetImage((int)Images.SpecialImage).sprite =
            Manager.Asset.LoadSprite(Manager.Game.GetCharacterData().Sprite_Special);

        _inventory = Manager.Game.Inventory;
        _character = Manager.Game.Character;

        BindModelEvent(Manager.Game.ElapsedTime, UpdateTimerUI, this);
        BindModelEvent(_inventory.WeaponCount, UpdateInventoryWeaponUI, this);
        BindModelEvent(_inventory.EquipmentCount, UpdateInventoryEquimentsUI, this);
        BindModelEvent(_character.CurrentExp, UpdateUI, this);
        BindModelEvent(Manager.Game.SpesialTimer, UpdateSpesialUI, this);
    }
    
    protected void SetArray(Images[] array, int idx)
    {
        for(int i = 0; i < array.Length; ++i)
        {
            array[i] = (Images)idx + i;
        }
    }

    //GameManager를 관찰
    private void UpdateSpesialUI(float value)
    {
        GetSlider((int)Sliders.SpecialSlider).value += value;
    }

    /// <summary>
    /// 경험치 이미지
    /// </summary>
    /// <param name="exPoint"></param>
    private void UpdateUI(float exPoint)
    {
        int number = (int)exPoint / 2;
        GetImage((int)Images.ExperiencePoints).sprite = Manager.Asset.LoadSprite($"Point_{number}");
    }

    /// <summary>
    /// 체력과 필살기 슬라이드
    /// 홀로코인과 처치 수 텍스트
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    public void UpdateUI(Define.MainUI_Type type, int count)
    {
        switch(type)
        {
            case Define.MainUI_Type.Hp:
                GetSlider((int)Sliders.HpSlider).value = count;
                break;
            case Define.MainUI_Type.Special:
                GetSlider((int)Sliders.SpecialSlider).value = count;
                break;
            case Define.MainUI_Type.Gold:
                GetText((int)Texts.GoldText).text = count.ToString();
                break;
            case Define.MainUI_Type.EnemyCount:
                GetText((int)Texts.EnemyCountText).text = count.ToString();
                break;
        }
    }
    /// <summary>
    /// 타이머
    /// </summary>
    /// <param name="time"></param>
    public void UpdateTimerUI(TimeSpan time)
    {
        string timer = string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);

        GetText((int)Texts.Timer).text = timer;
    }
    /// <summary>
    /// 인벤토리 슬롯 이미지
    /// </summary>
    /// <param name="type"></param>
    /// <param name="idx"></param>
    /// <param name="sprite"></param>
    /// <param name="level"></param>
    public void UpdateInventoryWeaponUI(int idx)
    {
        Weapon weapon = _inventory.Weapons[idx];
        GetImage((int)_weapons[idx]).sprite = Manager.Asset.LoadSprite(weapon.WeaponData.Name);
        GetImage((int)_weaponLevels[idx]).sprite = Manager.Asset.LoadSprite($"Lv_{weapon.Level}");
    }

    private void UpdateInventoryEquimentsUI(int idx)
    {
        Equipment equipment = _inventory.Equipments[idx];
        GetImage((int)_equipments[idx]).sprite = Manager.Asset.LoadSprite(equipment.EquipmentData.Name);
        GetImage((int)_equipmentLevels[idx]).sprite = Manager.Asset.LoadSprite($"Lv_{equipment.Level}");
    }
}
