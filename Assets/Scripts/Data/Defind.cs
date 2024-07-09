using System.IO;

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
        public const string Enemy = "/Enemy";
        public const string CSV = "/Data";
        public const string Character = "/Character";
        public const string Audio = "/Audio";
        public const string Sprite = "/Sprite";
        public const string Item = "/Item";
        public const string Text = "/Text";
        public const string Object = "/Object";
    }
}
