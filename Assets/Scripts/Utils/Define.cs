
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
        public const string Ani = "Animation/";
    }
    public enum ItemNumber
    {
        StartingWeaopn_Start = 1,
        StartingWeaopn_End,
        Weapon_Start = 1001,
        Weapon_End = 1129,
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
    public enum Mode_Type
    {
        StageMode,
        EndlessMode
    }
    public class KeyCode
    {
        public const string VERTICAL = "Vertical";
        public const string CONFIRM = "Confirm";
        public const string CANCEL = "Cancel";
    }

    public class Anim
    {
        public const string PointerMove = "PointerMove";

        public const string Idol_UI_Run = "Idol_UI_Run";
        public const string Idol_Play_Run = "Idol_Play_Run";
        
        public const string Anim_Idol = "Ani_Idle";
        public const string Anim_run = "Ani_run";
        public const string Anim_TakeDamage = "Ani_TakeDamage";
        public const string Anim_Attack = "Ani_Attack";
    }
    public class Tag
    {
        public const string IDOL = "Idol";
        public const string EXP = "Exp";
        public const string HASTE = "Haste";
        public const string AREA = "Area";
        public const string ENEMY = "Enemy";
        public const string ITEM_BOX = "Item_box";
    }
}
