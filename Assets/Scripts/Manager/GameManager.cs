using System;
using UnityEngine;
using UniRx;
using System.ComponentModel;

public class GameManager : MonoBehaviour
{
    public Inventory Inventory;
    public Character Character;
    public ReactiveProperty<TimeSpan> ElapsedTime = new();
    public ReactiveProperty<float> SpesialTimer = new();
    public CharacterID CharacterID;
    private CharacterData _characterData;
    private DateTime _startTime;

    bool _isPlaying;
    public void Init()
    {
        Inventory = Utils.GetOrAddComponent<Inventory>(gameObject);
    }

    private void Update()
    {
        if (_isPlaying) 
        { 
            TimeSystem();
        }
    }

    public CharacterData GetCharacterData()
    {
        return _characterData;
    }

    public void GameStart(CharacterID id)
    {
        CharacterID = id;
        Character = Manager.Asset.Instantiate("Character").GetComponent<Character>();
        _characterData = Manager.Data.Character[id];
        Character.Init();
        Inventory.Init();
        Manager.UI.MakeSubItem<SubItem_Map>();
        Manager.UI.MakeSubItem<SubItem_MainUI>();
        _isPlaying = true;
    }
    public void GameOver()
    {
        _isPlaying = false;
        Inventory.Claer();
        Manager.UI.Clear();
        Manager.UI.ShowPopup<Popup_Title>();
    }
    //Time
    private void TimeSystem()
    {
        TimeSpan elapsed = DateTime.Now - _startTime;
        ElapsedTime.SetValueAndForceNotify(elapsed);

        SpesialTimer.Value += Time.fixedDeltaTime * 1;
    }
}
