using System;
using UnityEngine;
using UniRx;
using System.ComponentModel;
using UniRx.Triggers;

public class GameManager : MonoBehaviour
{
    public Inventory Inventory;
    public Character Character;
    public ReactiveProperty<TimeSpan> ElapsedTime = new();
    public ReactiveProperty<int> EnemyCount = new();
    public ReactiveProperty<int> GoldCount = new();
    public ReactiveProperty<float> SpesialTimer = new();
    public ReactiveProperty<float> ExperiencePoints = new();
    public CharacterID CharacterID;
    private CharacterData _characterData;
    private DateTime _startTime;
    private IDisposable _disposable;

    private bool _isPlaying;
    private bool _isStage = true;
    public void Init()
    {
        Inventory = Utils.GetOrAddComponent<Inventory>(gameObject);
        _disposable = this.UpdateAsObservable().Subscribe(_ => TimeSystem());
    }

    public bool IsStage()
    {
        return _isStage;
    }
    public void SetStageMode(bool mode)
    {
        _isStage = mode;
    }
    public CharacterData GetCharacterData()
    {
        return _characterData;
    }

    public void GameStart()
    {
        Character = Manager.Asset.Instantiate("Character").GetComponent<Character>();
        _characterData = Manager.Data.Character[CharacterID];
        Character.Init();
        Inventory.Init();
        Manager.UI.MakeSubItem<SubItem_Map>();
        Manager.UI.MakeSubItem<SubItem_MainUI>();
        _isPlaying = true;
    }

    public void SetCharacterID(CharacterID id)
    {
        CharacterID = id;
    }
    public void GameOver()
    {
        _isPlaying = false;
        Inventory.Claer();
        Manager.UI.Clear();

    }
    private void TimeSystem()
    {
        if (_isPlaying)
        {
            TimeSpan elapsed = DateTime.Now - _startTime;
            ElapsedTime.SetValueAndForceNotify(elapsed);

            SpesialTimer.Value += Time.fixedDeltaTime * 1;
        }
    }

    private void OnDisable()
    {
        _disposable?.Dispose();
    }
}
