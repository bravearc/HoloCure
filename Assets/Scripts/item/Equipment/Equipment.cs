using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public abstract class Equipment : MonoBehaviour
{
    public string Name { get; private set; }
    public ReactiveProperty<int> Level { get; private set; } = new();
    public EquipmentData EquipmentData { get; private set; }
    public ItemID ID;
    public virtual void Init()
    {

    }

    public virtual void LevelUp()
    {
        ++Level.Value;
    }
}
