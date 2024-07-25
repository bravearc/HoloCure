public class SubItem_Stats : UI_SubItem
{
    #region enum
    protected enum Texts
    {
        NameText,
        HPText,
        ATKText,
        SPDText,
        CRTText,
        PickupText,
        HasteText
    }
    protected enum Images
    {
        Character
    }
    #endregion

    protected override void Init()
    {
        BindText(typeof(Texts));
        BindImage(typeof(Images));
        StatSet();
    }

    private void StatSet()
    {
        Character character = Manager.Game.Character;
        CharacterData data = Manager.Game.GetCharacterData();
        GetText((int)Texts.HPText).text = character.Hp.ToString() +
            " / " + character.MaxHp.ToString();
        GetText((int)Texts.NameText).text = data.Name;
        GetText((int)Texts.ATKText).text = character.Attack.ToString();
        GetText((int)Texts.SPDText).text = character.Speed.ToString();
        GetText((int)Texts.HasteText).text = character.Haste.ToString();
        GetText((int)Texts.PickupText).text = character.Pickup.ToString();
        GetImage((int)Images.Character).sprite =
        Manager.Asset.LoadSprite($"spr_Title_{data.Sprite}_0");
    }
}
