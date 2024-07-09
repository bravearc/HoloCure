public enum MainUI_Type
{ 
    Hp,
    Special,
    Gold,
    EnemyCount
}
public enum MainUI_Item
{
    Weapon,
    Equipment
}

public class Popup_MainUI : UI_Popup
{
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
        EnemyCountText
    }
    protected enum Sliders
    {
        HpSlider,
        SpecialSlider
    }

    private Images[] _weapons = new Images[6];
    private Images[] _weaponLevels = new Images[6];
    private Images[] _equipments = new Images[6];
    private Images[] _equipmentLevels = new Images[6];
    private void Start()
    {
        Init();
    }
    protected override void Init()
    {
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
    }
    
    protected void SetArray(Images[] array, int idx)
    {
        for(int i = 0; i < array.Length; ++i)
        {
            array[i] = (Images)idx + i;
        }
    }

    /// <summary>
    /// 경험치 이미지
    /// </summary>
    /// <param name="exPoint"></param>
    public void UpdateUI(int exPoint)
    {
        int number = exPoint / 2;
        GetImage((int)Images.ExperiencePoints).sprite = Manager.Asset.LoadSprite($"Point_{number}");
    }

    /// <summary>
    /// 체력과 필살기 슬라이드
    /// 홀로코인과 처치 수 텍스트
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    public void UpdateUI(MainUI_Type type, int count)
    {
        switch(type)
        {
            case MainUI_Type.Hp:
                GetSlider((int)Sliders.HpSlider).value = count;
                break;
            case MainUI_Type.Special:
                GetSlider((int)Sliders.SpecialSlider).value = count;
                break;
            case MainUI_Type.Gold:
                GetText((int)Texts.GoldText).text = count.ToString();
                break;
            case MainUI_Type.EnemyCount:
                GetText((int)Texts.EnemyCountText).text = count.ToString();
                break;
        }
    }
    /// <summary>
    /// 타이머
    /// </summary>
    /// <param name="time"></param>
    public void UpdateUI(string time)
    {
        GetText((int)Texts.Timer).text = time;
    }
    /// <summary>
    /// 인벤토리 슬롯 이미지
    /// </summary>
    /// <param name="type"></param>
    /// <param name="idx"></param>
    /// <param name="sprite"></param>
    /// <param name="level"></param>
    public void UpdateUI(MainUI_Item type, int idx, UnityEngine.Sprite sprite, int level = 1)
    {
        MainUI_Item item = type;
        
        switch (item) 
        {
            case MainUI_Item.Weapon:
                GetImage((int)_weapons[idx]).sprite = sprite;
                GetImage((int)_weaponLevels[idx]).sprite = Manager.Asset.LoadSprite($"Lv_{level}");
                break;
            case MainUI_Item.Equipment:
                GetImage((int)_equipments[idx]).sprite = sprite;
                GetImage((int)_equipmentLevels[idx]).sprite = Manager.Asset.LoadSprite($"Lv_{level}");
                break;
        }
    }
}
