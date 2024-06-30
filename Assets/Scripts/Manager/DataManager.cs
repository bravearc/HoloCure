using UnityEngine;
using CsvHelper;
using System.Collections.Generic;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Globalization;
using System.Linq;

public class DataManager
{
    public Dictionary<CharacterID, CharacterData> Character { get; private set; }
    public Dictionary<WeaponID, WeaponData> Weapon { get; private set; }
    public Dictionary<SoundID, SoundData> Sound { get; private set; }
    public Dictionary<AssetBuldleID, AssetBundleData> Asset { get; private set; }
    public void Init()
    {
        string path = "Assets/Resources/Data/";
        Character = ParseToDick<CharacterID, CharacterData>(path + "Character.csv", data => data.ID);
        Weapon = ParseToDick<WeaponID, WeaponData>(path + "Item.csv", data => data.ID);
        Sound = ParseToDick<SoundID, SoundData>(path + "Sound.csv", data => data.ID);
    }

    private Dictionary<TKey, TValue> ParseToDick<TKey, TValue>([NotNull] string path, Func<TValue, TKey> keySelector)
    {
        string fullPath = path;
#if UNITY_EDITOR
        using (var reader = new StreamReader(fullPath))
#else
        using (var reader = new StringReader(fullPath))
#endif
        using ( var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<TValue>().ToList();
            return records.ToDictionary(keySelector);
        }
    }
}

public enum CharacterID
{
    None
}
public enum CharacterType
{

}
public struct CharacterData
{
    public CharacterID ID { get; set; }
    public string Name { get; set; }
    public float HP { get; set; }
    public float Attack { get; set; }
    public float Speed { get; set; }
    public float Criticial {  get; set; }
    public CharacterType Type { get; set; }
    public int Unlock {  get; set; }

}
public enum WeaponID
{
    None
}
public enum WeaponType
{
    None,
    Melee = 1,
    Ranged = 2,
    Multishot = 3
}

public struct WeaponData
{
    public WeaponID ID { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public float Attack { get; set; }
    public int Quantity { get; set; }
    public float Speed { get; set; }
    public float AttackRange { get; set; }
    public float AttackCycle { get; set; }
    public float Size { get; set; }
    public int Knockback { get; set; }
    public int MAX_Level { get; set; }
    public WeaponType type { get; set; }
    public string KORNAME { get; set; }
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
