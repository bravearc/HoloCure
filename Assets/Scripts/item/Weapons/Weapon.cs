using UniRx;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public ReactiveProperty<int> Level { get; private set; } = new();
    public ItemID ID;
    public WeaponData WeaponData { get; private set; }
    public virtual void Init()
    {

    }

    public virtual void LevelUp()
    {
        ++Level.Value;
    }


}
