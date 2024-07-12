
public static class Define

{

    public enum UIEvent

    {

        Click,

        Enter

    }



    public enum Sound

    {

        BGM,

        Effect,

        Collision,

        Max

    }

    public class Path

    {

        public const string Enemy = "Enemy/";

        public const string CSV = "Data/";

        public const string Character = "Character/";

        public const string Audio = "Audio/";

        public const string Sprite = "Sprite/";

        public const string Item = "Item/";

        public const string Text = "Text/";

        public const string Object = "Object/";

    }



    public enum ItemNumber
    {
        StartingWeaopn_Start = 1,
        StartingWeaopn_End,
        Weapon_Start = 1001,
        Weapon_End = 1018,
        Equipment_Start = 2001,
        Equipment_End = 2021,
        Stats_Start = 3501,
        Stats_End = 3508
    }
    public enum MainUI_Type
    {
        Hp,
        Special,
        Gold,
        EnemyCount
    }

}
