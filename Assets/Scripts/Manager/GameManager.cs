using System;
using UnityEngine;
using UniRx;
using System.ComponentModel;
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
    private bool _isPlaying = false;
    public void Init()
    {
        Inventory = Utils.GetOrAddComponent<Inventory>(gameObject);
        Manager.UI.ShowPopup<Popup_Title>();
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
        _characterData = Manager.Data.Character[CharacterID];
        Character = Utils.GetOrAddComponent<Character>(Manager.Asset.InstantiateObject(nameof(Character)));
        _enemyController = Utils.GetOrAddComponent<EnemyController>(Manager.Asset.InstantiateObject(nameof(EnemyController))); 
        Manager.UI.CloseALLPopupUI();
        Manager.UI.ShowPopup<Popup_PlayUI>();
        Manager.Spawn.GameStartInit();
        Inventory.Init();
        Character.SetStats();
        IsPlaying.Value = true;
        TimeSystem(true);
    }

    public void SetCharacterID(CharacterID id)
    {
        CharacterID = id;
    }
    public void GameOver()
    {
        Inventory.Claer();
        Manager.UI.Clear();
        Manager.UI.ShowPopup<Popup_Title>();
        Manager.Asset.Destroy(Character.gameObject);
        Manager.Asset.Destroy(_enemyController.gameObject);
        Manager.Spawn.PoolReset();
        Time.timeScale = 1.0f;
        TimeSystem(false);
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
                    SpecialTimer.Value += 1;

                    if (PlayTimeSecond.Value >= 60)
                    {
                        PlayTimeMinute.Value += 1;
                        PlayTimeSecond.Value = 0;
                    }
                });
        }
        else
        {
            _timerDisposable.Dispose();
        }
    }
}
