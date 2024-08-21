using UnityEngine;
using UniRx;

public class Character : MonoBehaviour
{
    public ReactiveProperty<int> Hp = new();
    public ReactiveProperty<int> MaxHp = new();
    public ReactiveProperty<float> Attack = new();
    public ReactiveProperty<float> Speed = new();
    public ReactiveProperty<float> Criticial = new();
    public ReactiveProperty<float> Pickup = new();
    public ReactiveProperty<float> Haste = new();
    public ReactiveProperty<int> Level = new();
    public ReactiveProperty<float> CurrentExp = new();
    public ReactiveProperty<float> MaxExp = new();
    public CharacterID CharacterID;
    public Transform Cursor;

    float _maxExp = 50;
    private void Awake() => Init();
    public void Init()
    {
        Utils.GetOrAddComponent<IdolMove>(gameObject);
        Utils.GetOrAddComponent<CursorControl>(gameObject);
        Utils.GetOrAddComponent<IdolAnim>(gameObject);

        Cursor = transform.Find("Cursor");

        
    }
    public void SetStats()
    {
        CharacterData data = Manager.Game.GetCharacterData();
        MaxHp.Value = data.Hp;
        Attack.Value = data.Attack;
        Speed.Value = data.Speed;
        Criticial.Value = data.Criticial;
        Pickup.Value = data.Pickup;
        Haste.Value = data.Haste;
        Level.Value = 1;
        MaxExp.Value = 1;
        Hp.Value = data.Hp;

    }
    public void GetStats(ItemID id, int value)
    {
        switch(id)
        {
            case ItemID.MaxHPUp: GetMaxHP(value); break;
            case ItemID.ATKUp: GetATK(value); break;
            case ItemID.SPDUp: GetSPD(value); break;
            case ItemID.CRTUp: GetCRT(value); break;
            case ItemID.PickUpRangeUp: GetPickUpRange(value); break;
            case ItemID.HasteUp: GetHasteup(value); break;
            default: Debug.Log($"Erorr ItemID: {id}"); break;
        }
    }
    public void GetExp(float value)
    {
        //È¹µæ »ç¿îµå
        float addExp = value / _maxExp;
        CurrentExp.Value += addExp;
        if(CurrentExp.Value > MaxExp.Value)
        {
            LevelUp();
        }
    }
    public void GetHp(int damage)
    {

        if (damage > 0)
            Manager.Sound.Play(Define.SoundType.Effect, "GetExp");

        else
            Manager.Sound.Play(Define.SoundType.Effect, "PlayerDamaged");

        Hp.Value += damage;
        if(Hp.Value >= MaxHp.Value)
            Hp.Value = MaxHp.Value;

        if (Hp.Value <= 0)
            Manager.UI.ShowPopup<Popup_GameOver>();
    }
    private void LevelUp()
    {
        _maxExp *= 2f;
        CurrentExp.Value = 0;
        Manager.UI.ShowPopup<Popup_LevelUp>();
    }

    private void GetMaxHP(int value)
    {
        MaxHp.Value += value;
    }
    private void GetATK(float value)
    {
        Attack.Value += value;
    }
    private void GetSPD(float value)
    {
        Speed.Value += value;
    }
    private void GetCRT(float value)
    {
        Criticial.Value += value;
    }
    private void GetPickUpRange(float value)
    {
        Pickup.Value += value;
    }
    private void GetHasteup(float value)
    {
        Haste.Value += value;
    }

}
