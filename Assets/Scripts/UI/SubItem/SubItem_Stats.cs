public class SubItem_Stats : UI_SubItem
{
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
    private void Start() => Init();

    protected override void Init()
    {
        BindText(typeof(Texts));
        BindImage(typeof(Images));
        StatSet();
    }

    private void StatSet()
    {
        CharacterData data = Manager.Game.GetCharacterData();
        GetText((int)Texts.HP).text = Manager.Game._hp.ToString() +
            " / " + Manager.Game._maxhp.ToString();
        GetText((int)Texts.Name).text = Manager.Game._name;
        GetText((int)Texts.ATK).text = Manager.Game._attack.ToString();
        GetText((int)Texts.SPD).text = Manager.Game._speed.ToString();
        GetText((int)Texts.Haste).text = Manager.Game._haste.ToString();
        GetText((int)Texts.Pickup).text = Manager.Game._pickup.ToString();
        GetImage((int)Images.Character).sprite =
            Manager.Asset.LoadSprite(Manager.Game._name);
    }
}
