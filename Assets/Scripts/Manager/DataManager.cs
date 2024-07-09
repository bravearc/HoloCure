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
    public Dictionary<ItemID, ItemData> Item { get; private set; }
    public Dictionary<ItemID, List<WeaponData>> Weapon { get; private set; }
    public Dictionary<ItemID, List<EquipmentData>> Equipment { get; private set; }
    public Dictionary<SoundID, SoundData> Sound { get; private set; }
    public Dictionary<AssetBuldleID, AssetBundleData> Asset { get; private set; }

    public void Init()
    {
        string path = "Assets/Resources/Data/";
        Character = ParseToDick<CharacterID, CharacterData>(path + "Character.csv", data => data.ID);
        Item = ParseToDick<ItemID, ItemData>(path + "Weapon.csv", data => data.ID);
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


public enum SoundID
{
    None
}
public struct SoundData
{
    public SoundID ID { get; set; }
    public string Name { get; set; }
    public string AudioClip { get; set; }
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
public enum EnemyID
{
    None,
    Nomal = 1000,
    Boss = 2000
}
public struct EnemyData
{
    public EnemyID ID { get; set; }
    public float Hp { get; set; }
}
