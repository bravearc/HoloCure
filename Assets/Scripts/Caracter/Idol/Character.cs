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

    Rigidbody2D _rigidbody;

    public void Init()
    {
        Utils.GetOrAddComponent<IdolMove>(gameObject);
        Utils.GetOrAddComponent<CursorControl>(gameObject);
        Utils.GetOrAddComponent<IdolAnim>(gameObject);

        SetStats();
    }
    void SetStats()
    {
        CharacterData data = Manager.Game.GetCharacterData();
        MaxHp.Value = data.Hp;
        Hp.Value = data.Hp;
        Attack.Value = data.Attack;
        Speed.Value = data.Speed;
        Criticial.Value = data.Criticial;
        Pickup.Value = data.Pickup;
        Haste.Value = data.Haste;
        Level.Value = 1;
        MaxExp.Value = Manager.Data.Exp[Level.Value].Exp;

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
        //획득 사운드
        CurrentExp.Value += value;
        if(CurrentExp.Value > MaxExp.Value)
        {
            LevelUp();
        }
    }
    public void Update_Hp(int damage)
    {
        if (damage > 0) { }
        //회복 사운드
        else {}
        //피격 사운드

        Hp.Value += damage;
        if(Hp.Value >= MaxHp.Value)
            Hp.Value = MaxHp.Value;

        if (Hp.Value <= 0)
            Manager.Game.GameOver();
    }
    private void LevelUp()
    {
        MaxExp.Value = Manager.Data.Exp[Level.Value].Exp;
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
