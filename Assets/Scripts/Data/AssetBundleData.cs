using UnityEngine;
public enum AssetBuldleID
{
    None,
    Character,
    monster,
    Item,
    Sprite,
    Sound,
    UI
}

public class AssetBundleData
{
    public AssetBuldleID ID { get; set; }
    public AssetBundle Asset { get; set; }

}
