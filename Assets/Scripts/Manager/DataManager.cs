using UnityEngine;
using CsvHelper;
using System.Collections.Generic;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Globalization;
using System.Linq;

public class DataManager : MonoBehaviour
{
    public Dictionary<CharaterID, CharacterData> Character { get; private set; }
    public Dictionary<ItemID, ItemData> Item { get; private set; }
    public Dictionary<SoundID, SoundData> Sound { get; private set; }
    public Dictionary<AssetBuldleID, AssetBundleData> Asset { get; private set; }
    public void Init()
    {
        //Character = 
    }

    private Dictionary<TKey, TValue> ParseToDict<TValue, TKey>([NotNull] string path, Func<TValue, TKey> keySelector)
    {
        string fullPath = path;
#if UNITY_EDITOR
        using (var reader = new StreamReader(fullPath))
#else
        using (var reader2 = new StringReader(fullPath))
#endif
            using ( var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<TValue>().ToList();
            return records.ToDictionary(keySelector);
        }
    }


}

public enum CharaterID
{

}
public enum CharacterType
{

}
public struct CharacterData
{
    public CharacterType Type { get; set; }
    public CharaterID ID { get; set; }
    public string Name { get; set; }
    public int HP {  get; set; }
}
public enum ItemID
{

}
public enum ItemType
{

}
public struct ItemData
{
    ItemType ID { get; set; }
    string Name { get; set; }
    ItemType ItemType { get; set; }
}
public enum SoundID
{
    None,
    Character,
    Monster,
    UI,
    BGM,
    Effect
}
public struct SoundData
{
    public SoundID ID { get; set; }
    public string Name { get; set; }
    public int Volume { get; set; }

}
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
public struct AssetBundleData
{
    public AssetBuldleID ID { get; set; }
    public AssetBundle Asset { get; set; }
}
