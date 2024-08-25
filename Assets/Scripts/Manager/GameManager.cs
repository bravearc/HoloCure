using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GameManager : MonoBehaviour
{
    public Inventory Inventory;
    public Character Character;
    public ReactiveProperty<int> EnemyCount = new();
    public ReactiveProperty<int> GoldCount = new();
    public ReactiveProperty<int> PlayTimeSecond = new();
    public ReactiveProperty<int> PlayTimeMinute = new();
    public ReactiveProperty<float> SpecialTimer = new();
    public ReactiveProperty<bool> IsPlaying = new();
    private StageData stageData;
    public CharacterID CharacterID;
    private CharacterData _characterData;
    private EnemyController _enemyController;
    private IDisposable _timerDisposable;

    private bool _isStage = true;
    public  bool IsGameClear = false;
    public void Init()
    {
        Inventory = Utils.GetOrAddComponent<Inventory>(gameObject);
        Manager.UI.ShowPopup<Popup_Title>();

        IsPlaying.Subscribe(TimeSystem).AddTo(this);
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
    public void SetCharacterID(CharacterID id)
    {
        CharacterID = id;
    }

    public void GameStart()
    {
        _characterData = Manager.Data.Character[CharacterID];
        Character = Utils.GetOrAddComponent<Character>(Manager.Asset.InstantiateObject(nameof(Character)));
        _enemyController = Utils.GetOrAddComponent<EnemyController>(Manager.Asset.InstantiateObject(nameof(EnemyController)));

        Manager.Spawn.SetSpawn();
        Inventory.Init();
        Character.SetStats();
        IsPlaying.Value = true;
    }
    public void GameReStart()
    {
        IsPlaying.Value = false;
        Inventory.Clear();
        Inventory.Init();
        Character.SetStats();
        Manager.Sound.Play(Define.SoundType.BGM, "StageOneBGM");
        Manager.Spawn.PoolReset();
        TimeReset();
        IsPlaying.Value = true;
        Time.timeScale = 1.0f;
    }

    public void GameOver()
    {
        Manager.UI.Clear();
        Inventory.Clear();
        Manager.Asset.Destroy(Character.gameObject);
        Manager.Asset.Destroy(_enemyController.gameObject);
        Manager.Spawn.PoolReset();
        TimeReset();
        Manager.UI.ShowPopup<Popup_Title>();
        IsPlaying.Value = false;

    }
    private void TimeSystem(bool isPlay)
    {
        if (isPlay)
        {
          
            _timerDisposable = Observable.Interval(TimeSpan.FromSeconds(1))
                .Where(_ => IsPlaying.Value)
                .Subscribe(_ =>
                {
                    PlayTimeSecond.Value += 1;
                    if (PlayTimeSecond.Value >= 60)
                    {
                        PlayTimeMinute.Value += 10;
                        PlayTimeSecond.Value = 0;
                    }
                });
        }
        else if (isPlay == false && _timerDisposable != null)
        {
            _timerDisposable?.Dispose();
            _timerDisposable = null;
        }
    }

    void TimeReset()
    {
        PlayTimeMinute.Value = 0;
        PlayTimeSecond.Value = 0;
    }
}
