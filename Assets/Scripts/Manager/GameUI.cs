using System;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : UIManager
{
    public GameObject MainUI;
    public GameObject Time;
    public GameObject Event;

    public GameObject Stat;
    public GameObject Paused;
    public GameObject LevelUp;
    public GameObject ItemBox;
    public GameObject Hammer;

    //Main UI
    private Image _characterImage;
    private Slider _hpSlider;
    private Image _specialImage;
    private Slider _specialTimerSlider;

    //타이머
    private Text _time;

    //스탯
    private Text _nameText;
    private Text _hpText;
    private Text _atkText;
    private Text _spdText;
    private Text _crtText;
    private Text _pickupText;
    private Text _hasteText;

    //홀로코인, 클리어 수
    private Text _holoCoinText;
    private Text _enemyCountText;

    //인벤토리
    private Image[] _weaponImage = new Image[6];
    private Image[] _equipmentImage = new Image[6];
    private Image[] _stempImage = new Image[3];

    //아이템 레벨
    private Image[] _weaponLevelImage = new Image[6];
    private Image[] _equipmentLevelImage = new Image[6];

    //경험치
    private Image _experiencePoints;

    //일시 정지
    private Button _skill;
    private Button _questionMark;
    private Button _resume;
    private Button _setting;
    private Button _quit;

    //Level Up
    private Transform _levelUpPointer;
    private Transform[] _levelUpPointerPositions = new Transform[4];
    private Button[] _levelUpButtons = new Button[4];
    private Text[] _levelUpNameTexts = new Text[4];
    private Text[] _levelUpDescriptionsText = new Text[4];
    private Image[] _levelUpTypeImage = new Image[4];
    private Image[] _levelUpItemImage = new Image[4];

    //ItemBox
    private GameObject _boxing;
    private Button _boxingOpenButton;
    private Transform _unBoxingPointer;
    private Transform[] _unBoxingPointerPositions = new Transform[2];
    private Button[] _unBoxingButtons = new Button[2];
    private GameObject _unBoxing;
    private Text _unboxingNameText;
    private Text _unboxingDescriptionText;
    private Image _unboxingItemImage;
    private Image _unBoxingtypeImage;

    //Hammer
    private Button[] _hammerWeaponButtons = new Button[6];
    private Image[] _hammerWeaponImages = new Image[6];
    private Button[] _hammerEquipmentButtons = new Button[6];
    private Image[] _hammerEquipmentImages = new Image[6];
    private Text _hammerNameText;
    private Text _hammerDescriptionText;
    private Image _hammerItemImage;
    private Image _hammerTypeImage;

    public void Init()
    {
        MainUI = FindObject("MainUI");
        Time = FindObject("Time");
        Event = FindObject("Event");

        _time = Time.transform.Find("Timer").GetComponent<Text>();

        #region 초기화
        #region Main UI
        _characterImage = FindComponent<Image>("Character", MainUI.transform);
        _hpSlider = FindComponent<Slider>("Hp", MainUI.transform);
        _specialImage = FindComponent<Image>("SpecialImage", MainUI.transform);
        _specialTimerSlider = FindComponent<Slider>("SpecialSlider", MainUI.transform);
        #endregion

        #region 스탯
        Stat = FindObject("Stat", Event.transform).transform.Find("Texts").gameObject;
        _nameText = FindComponent<Text>("Name", Stat.transform);
        _hpText = FindComponent<Text>("HP", Stat.transform);
        _atkText = FindComponent<Text>("ATK", Stat.transform);
        _spdText = FindComponent<Text>("SPD", Stat.transform);
        _crtText = FindComponent<Text>("CRT", Stat.transform);
        _pickupText = FindComponent<Text>("Pickup", Stat.transform);
        _hasteText = FindComponent<Text>("Haste", Stat.transform);
        #endregion

        #region 코인, 클리어 수
        Transform countTexts = FindObject("Count").transform.Find("Texts");
        _holoCoinText = FindComponent<Text>("Gold", countTexts);
        _enemyCountText = FindComponent<Text>("EnemyCount", countTexts);
        #endregion

        #region 인벤토리
        SetObjectArray(_weaponImage, "Weapon");
        SetObjectArray(_equipmentImage, "Equipment");
        SetObjectArray(_weaponLevelImage, "WeaponLevel");
        SetObjectArray(_equipmentLevelImage, "EquipmentLevel");
        #endregion

        #region 일시 정지
        Paused = FindObject("Paused", Event.transform).gameObject;
        Transform pausedButtons = Paused.transform.Find("Buttons");
        _skill = FindComponent<Button>("Skill", pausedButtons);
        _questionMark = FindComponent<Button>("????", pausedButtons);
        _resume = FindComponent<Button>("Resume", pausedButtons);
        _setting = FindComponent<Button>("Setting", pausedButtons);
        _quit = FindComponent<Button>("Quit", pausedButtons);
        #endregion

        #region 레벨 업
        LevelUp = FindObject("LevelUp", Event.transform).gameObject;
        Transform levelUpTransform = LevelUp.transform;
        _levelUpPointer = levelUpTransform.Find("Pointer");
        SetObjectArray(_levelUpPointerPositions, "PointerPosition", levelUpTransform);
        for (int i = 0; i < 4; i++)
        {
            _levelUpButtons[i] = FindComponent<Button>($"Buttons/LevelUpButton_{i}", levelUpTransform);
            _levelUpNameTexts[i] = FindComponent<Text>("NameText", _levelUpButtons[i].transform);
            _levelUpDescriptionsText[i] = FindComponent<Text>("Description", _levelUpButtons[i].transform);
            _levelUpTypeImage[i] = FindComponent<Image>("TypeImage", _levelUpButtons[i].transform);
            _levelUpItemImage[i] = FindComponent<Image>("ItemImage", _levelUpButtons[i].transform);
        }
        #endregion

        #region 아이템 박스
        ItemBox = FindObject("ItemBox", Event.transform).gameObject;
        _boxing = FindObject("Boxing", ItemBox.transform).gameObject;
        _boxingOpenButton = FindComponent<Button>("Button", _boxing.transform);

        _unBoxing = FindObject("UnBoxing", ItemBox.transform).gameObject;
        _unBoxingPointer = _unBoxing.transform.Find("Pointer");
        SetObjectArray(_unBoxingPointerPositions, "PointerPosition", _unBoxing.transform);
        SetObjectArray(_unBoxingButtons, "Buttons", _unBoxing.transform);

        Transform unBoxingImage = _unBoxing.transform.Find("Image");
        _unboxingNameText = FindComponent<Text>("NameText", unBoxingImage);
        _unboxingDescriptionText = FindComponent<Text>("Description", unBoxingImage);
        _unboxingItemImage = FindComponent<Image>("TypeImage", unBoxingImage);
        _unBoxingtypeImage = FindComponent<Image>("ItemImage", unBoxingImage);
        #endregion

        #region 모루
        Hammer = FindObject("Hammer", Event.transform).gameObject;
        SetObjectArray(_hammerWeaponButtons, "Weapons");
        SetObjectArray(_hammerEquipmentButtons, "Equipment");
        for (int i = 0; i < 6; i++)
        {
            _hammerWeaponImages[i] = _hammerWeaponButtons[i].transform.GetChild(0).GetComponent<Image>();
            _hammerEquipmentImages[i] = _hammerEquipmentButtons[i].transform.GetChild(0).GetComponent<Image>();
        }

        Transform hammerImage = Hammer.transform.Find("Image");
        _hammerNameText = FindComponent<Text>("NameText", hammerImage);
        _hammerDescriptionText = FindComponent<Text>("Description", hammerImage);
        _hammerItemImage = FindComponent<Image>("TypeImage", hammerImage);
        _hammerTypeImage = FindComponent<Image>("ItemImage", hammerImage);
        #endregion

        _experiencePoints = FindObject("ExperiencePoints").transform.GetChild(0).GetComponent<Image>();
        #endregion
    }

    private GameObject FindObject(string name, Transform parent = null)
    {
        Transform target = parent == null ? transform.Find(name) : parent.Find(name);
        if (target == null) throw new Exception($"{name} not found in {parent?.name ?? "root"}");
        return target.gameObject;
    }

    private T FindComponent<T>(string name, Transform parent)
    {
        Transform target = parent.Find(name);
        if (target == null) throw new Exception($"{name} component not found in {parent.name}");
        return target.GetComponent<T>();
    }

    private void SetObjectArray<T>(T[] array, string parentName, Transform parent = null)
    {
        Transform parentTransform = parent == null ? MainUI.transform.Find(parentName) : parent.Find(parentName);
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = parentTransform.GetChild(i).GetComponent<T>();
        }
    }

    //경험치 표기
    public void ExperiencePointsUp(int i)
    {
        int imageIndex = i / 2;
        _experiencePoints.sprite = Manager.Asset.LoadSprite($"Point_{imageIndex}");
    }

    //스탯 세팅
    public void SetStats(string name, float hp, float maxhp, float atk, float spd, float crt, float pickup, float haste)
    {
        _nameText.text = name;
        _hpText.text = StringStats(hp) + " / " + StringStats(maxhp);
        _atkText.text = StringStats(atk);
        _spdText.text = StringStats(spd);
        _crtText.text = StringStats(crt);
        _pickupText.text = StringStats(pickup);
        _hasteText.text = StringStats(haste);
    }

    private string StringStats(float value)
    {
        return $"{Mathf.FloorToInt(value)}%";
    }

    public void TimeUpdate(string timer)
    {
        _time.text = timer;
    }


    public void EnemyCountUpdate(int count)
    {
        _enemyCountText.text = count.ToString();
    }

    public void GoldUpdate(int coin)
    {
        _holoCoinText.text = coin.ToString();
    }
}
