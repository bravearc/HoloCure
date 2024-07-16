public class SubItem_Stats : UI_SubItem
{
    #region enum
    protected enum Texts
    {
        Name,
        HP,
        ATK,
        SPD,
        CRT,
        Pickup,
        Haste
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
        GetText((int)Texts.HP).text = character.Hp.ToString() +
            " / " + character.MaxHp.ToString();
        GetText((int)Texts.Name).text = data.Name;
        GetText((int)Texts.ATK).text = character.Attack.ToString();
        GetText((int)Texts.SPD).text = character.Speed.ToString();
        GetText((int)Texts.Haste).text = character.Haste.ToString();
        GetText((int)Texts.Pickup).text = character.Pickup.ToString();
        GetImage((int)Images.Character).sprite =
        Manager.Asset.LoadSprite(data.Name);
    }
}
