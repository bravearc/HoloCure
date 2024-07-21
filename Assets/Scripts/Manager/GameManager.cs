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
    public ReactiveProperty<bool> IsPlaying = new();
    private StageData stageData;
    public CharacterID CharacterID;
    private CharacterData _characterData;
    private DateTime _startTime;
    private IDisposable _disposable;

    private bool _isStage = true;
    public void Init()
    {
        Inventory = Utils.GetOrAddComponent<Inventory>(gameObject);
        _disposable = this.UpdateAsObservable().Subscribe(_ => TimeSystem());
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
        Character = Utils.GetOrAddComponent<Character>(Manager.Asset.LoadObject("Character"));
        Manager.UI.CloseALLPopupUI();
        Manager.UI.ShowPopup<Popup_PlayUI>();
        _characterData = Manager.Data.Character[CharacterID];
        Character.Init();
        Inventory.Init();
        _startTime = DateTime.Now;
        IsPlaying.Value = true;
    }

    public void SetCharacterID(CharacterID id)
    {
        CharacterID = id;
    }
    public void GameOver()
    {
        IsPlaying.Value = false;
        Inventory.Claer();
        Manager.UI.Clear();
        Manager.UI.ShowPopup<Popup_Title>();
        Manager.Asset.Destroy(Character.gameObject);
        Time.timeScale = 1.0f;
    }
    private void TimeSystem()
    {
        if (IsPlaying.Value)
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
