using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameUI gameUI;
    public Inventory Inventory;

    private DateTime _startTime;

    public string _name;
    public float _hp;
    public float _attack;
    public float _speed;
    public float _critical;
    public float _pickup = 30;
    public float _haste = 12;

    public void Init()
    {
        gameUI = Manager.Asset.LoadObject("GameUI").GetComponent<GameUI>();
        Inventory = gameUI.transform.GetComponent<Inventory>();
    }

    private void Update()
    {
        TimeSystem();
    }
    public void SetCharacter(CharacterData data)
    {
        _name = data.Name;
        _hp = data.HP;
        _attack = data.Attack;
        _speed = data.Speed;
        _critical = data.Criticial;
    }

    //gameUI - stats 전달
    public void StatGain()
    {
        gameUI.SetStats(_name, _hp, _attack, _speed, _critical, _pickup, _haste);
    }

    //timer
    private void TimeSystem()
    {
        TimeSpan elapsedTime = DateTime.Now - _startTime;
        string timer = string.Format("{0:D2} : {1:D2}", elapsedTime.Minutes, elapsedTime.Seconds);
        gameUI.TimeUpdate(timer);
        if (elapsedTime.Minutes % 3 == 0)
        {
            //스폰되는 Enemy Update
        }
        else if (elapsedTime.Seconds % 5 == 0)
        {
            //보스 스폰
        }
    }
}
