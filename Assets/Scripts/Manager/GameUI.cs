using System;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : UIManager
{
    public CharacterData characterData;
    private IStats istats;

    public GameObject PlayUI;

    public GameObject MainUI;
    public GameObject Time;
    public GameObject Paused;

    private Text _time;

    private Text _nameText;
    private Text _hpText;
    private Text _atkText;
    private Text _spdText;
    private Text _crtText;
    private Text _pickupText;
    private Text _hasteText;

    private Text _holoCoinText;
    private Text _enemyCountText;

    private Image[] _weaponImage = new Image[6];
    private Image[] _equipmentImage = new Image[6];
    private Image[] _stempImage = new Image[3];

    private Image[] _weaponLevelImage = new Image[6];
    private Image[] _equipmentLevelImage = new Image[6];

    private Image _experiencePoints;
    private DateTime _startTime;

    public void Init(CharacterData id)
    {
        characterData = id;
        PlayUI = Manager.Asset.LoadObject("PlayUI");
        MainUI = transform.Find("MainUI").gameObject;
        Time = transform.Find("Time").gameObject;
        Paused = transform.Find("Paused").gameObject;

        _time = Time.transform.Find("Timer").GetComponent<Text>();

        Transform tr = Paused.transform.Find("Stat").Find("Texts");
        _nameText = tr.Find("Name").GetComponent<Text>();
        _hpText = tr.Find("HP").GetComponent<Text>();
        _atkText = tr.Find("ATK").GetComponent<Text>();
        _spdText = tr.Find("SPD").GetComponent<Text>();
        _crtText = tr.Find("CRT").GetComponent<Text>();
        _pickupText = tr.Find("Pickup").GetComponent<Text>();
        _hasteText = tr.Find("Haste").GetComponent<Text>();

        tr = transform.Find("Count").Find("Texts");
        _holoCoinText = tr.Find("Gold").GetComponent<Text>();
        _enemyCountText = tr.Find("EnemyCount").GetComponent<Text>();

        _startTime = DateTime.Now;

        tr = MainUI.transform.Find("Weapon");
        SetObject(_weaponImage, tr);
        tr = MainUI.transform.Find("Equipment");
        SetObject(_equipmentImage, tr);
        tr = MainUI.transform.Find("WeaponLevel");
        SetObject(_weaponLevelImage, tr);
        tr = MainUI.transform.Find("EquipmentLevel");
        SetObject(_equipmentLevelImage, tr);

        _experiencePoints = transform.Find("ExperiencePoints")
            .GetChild(0).GetComponent<Image>();
    }


    /// <summary>
    /// 경험치 이미지, 경험치 몇% 찼는지 전달하면됨.
    /// </summary>
    /// <param name="i"></param>
    public void ExperiencePointsUp(int i)
    {
        int image = i / 2;
        _experiencePoints.sprite = Manager.Asset.LoadSprite($"Point_{image}");
    }

    //스탯 세팅 및 스탯 업데이트
    //오버라이딩
    public void SetStats(string name, float hp, float at, float spd, float crt, float pic, float has)
    {
        _nameText.text = name;
        _hpText.text = StringStats(hp);
        _atkText.text = StringStats(at);
        _spdText.text = StringStats(spd);
        _crtText.text = StringStats(crt);
        _pickupText.text = StringStats(pic);
        _hasteText.text = StringStats(has);
    }
    private string StringStats(float fl)
    {
        string str = Mathf.FloorToInt(fl).ToString();
        return str + "%";
    }

    //자식오브젝트 세팅
    private void SetObject<T>(T[] array, Transform t)
    {
        for(int i = 0; i < array.Length; i++) 
        {
            array[i] = t.GetChild(i).GetComponent<T>();
        }
    }

    //게임 플레이 시간 표기
    public void TimeUpdate(string timer)
    {
        _time.text = timer;
    }

    //획득한 Item Image 교체
    public void ItemImageUpdate<T>(T t, string itemName, int count)
    {
        Image[] images = t switch
        {
            Weapon => _weaponImage,
            Equipment => _equipmentImage,
            Stemp => _stempImage,
            _ => throw new ArgumentException("Unsupported type", nameof(t))
        };

        string normalSprite = images.ToString() switch
        {
            "_weaponImage" => "Waepon",
            "_equipmentImage" => "Equipment",
            "_stempImage" => "Stemp",
            _ => throw new ArgumentException("string type", nameof(images))
        };


        images[count].sprite = Manager.Asset.LoadSprite(itemName);
        RectTransform RT = images[count].GetComponent<RectTransform>();
        RT.sizeDelta = new Vector2(80, 80);
    }

    public void LevelImageUpdate(Image image, int i)
    {
        image.sprite = Manager.Asset.LoadSprite($"Lv_{i}");
    }

    //Enemy 죽인 수 표기
    public void EnemyCountUpdate(int count)
    {
        _enemyCountText.text = count.ToString();
    }

    //HoloCoin 획득 수 표기
    public void GoldUpdate(int coin)
    {
        _holoCoinText.text = coin.ToString();
    }
}
